using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Hadoop.Avro;
using Microsoft.Hadoop.Avro.Container;
using Microsoft.Hadoop.Avro.Schema;
using Timer = System.Windows.Forms.Timer;
using MsCommon.ClickOnce;
using Newtonsoft.Json;

namespace AvroViewerGui
{
    public partial class MainForm : AppForm
    {
        private static readonly string Version = "v" + AppVersion.GetVersion();

        private string[] _loadedColumns = null;
        private string[][] _loadedRows = null;
        private string[][] _filteredRows = null;
        private string _initialFilename = null;

        private bool _abortClicked = false;

        private Timer delayedFilterTextChangedTimer;

        public MainForm(string filename = null)
        {
            InitializeComponent();
            Text = AppVersion.AppName + " " + Version;
            dataGridView1.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableWithAutoHeaderText;
            dataGridView1.MultiSelect = true;
            dataGridView1.CellDoubleClick += HandleCellDoubleClick;
            typeof(DataGridView).InvokeMember(
               "DoubleBuffered",
               BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty,
               null,
               dataGridView1,
               new object[] { true });

            // Filter textbox
            filterTextBox.GotFocus += HandleFilterGotFocus;
            filterTextBox.LostFocus += HandleFilterLostFocus;
            filterTextBox.KeyUp += HandleFilterKeyup;
            HandleFilterLostFocus(this, EventArgs.Empty);

            delayedFilterTextChangedTimer = new Timer();
            delayedFilterTextChangedTimer.Interval = 500;
            delayedFilterTextChangedTimer.Tick += DelayedFilterTextChangedTimerTick;

            // New version check
            AppVersion.CheckForUpdateAsync();

            // File?
            _initialFilename = filename;
        }

        #region Filter textbox

        private static string FilterText = "type to filter";

        void HandleFilterGotFocus(object sender, EventArgs e)
        {
            if (filterTextBox.Text == FilterText)
            {
                filterTextBox.Text = "";
                //filterTextBox.Font = new Font(TextBox.DefaultFont, FontStyle.Regular);
                filterTextBox.ForeColor = TextBox.DefaultForeColor;
            }
        }

        void HandleFilterLostFocus(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(filterTextBox.Text))
            {
                filterTextBox.Text = FilterText;
                //filterTextBox.Font = new Font(TextBox.DefaultFont, FontStyle.Regular);
                filterTextBox.ForeColor = Color.Gray;
            }
        }

        string[] GetFilterTextSearchTerms()
        {
            if (string.IsNullOrEmpty(filterTextBox.Text) || filterTextBox.Text.Equals(FilterText))
                return null;

            return filterTextBox.Text.ToLower().Split(new[] { "&&" }, StringSplitOptions.RemoveEmptyEntries);
        }

        void HandleFilterKeyup(object sender, EventArgs e)
        {
            delayedFilterTextChangedTimer.Stop();
            delayedFilterTextChangedTimer.Start();
        }

        private void DelayedFilterTextChangedTimerTick(object sender, EventArgs e)
        {
            delayedFilterTextChangedTimer.Stop();
            HandleFilter();
        }

        private void HandleFilter()
        {
            if (dataGridView1 == null || dataGridView1.RowCount == 0)
                return;

            // Get the selected filter text
            string[] searchterms = GetFilterTextSearchTerms();

            // Determine the filteredrows
            if (searchterms == null)
            {
                _filteredRows = _loadedRows;
            }
            else
            {
                searchterms = searchterms.Where(s => !s.StartsWith("#")).ToArray(); // niet filteren op highlight terms
                _filteredRows = (from row in _loadedRows
                    where searchterms.Where(s => !s.StartsWith("!")).All(s => row.Any(r => r.ToLower().Contains(s))) &&
                          searchterms.Where(s => s.StartsWith("!")).All(s => row.All(r => !r.ToLower().Contains(s.TrimStart('!'))))
                    select row).ToArray();
            }

            // Always show at least one record. When there are no results, add a dummy. If we don't do this, the
            // grid won't be usable anymore.
            if (_filteredRows.Length == 0)
                _filteredRows = new string[][] {Enumerable.Repeat("No result", _loadedColumns.Length).ToArray()};

            // Only update the rowcount if it changes, as this is a fairly expensive operation
            if (_filteredRows.Length != dataGridView1.RowCount)
            {
                dataGridView1.Rows.Clear();
                dataGridView1.RowCount = _filteredRows.Length;
            }
            dataGridView1.Invalidate();
        }

        bool FilterContains(string[] items, string[] searchterms)
        {
            // Keep a list of searchterms to find
            List<string> searchtermsRemaining = new List<string>(searchterms);

            // Loop over each item (i.e. column values)
            foreach (var item in items)
            {
                // Loop over each remaining searchterm
                foreach (var searchterm in searchtermsRemaining)
                {
                    // If a searchterm is found, remove it from the 'remaining' list
                    if (item.ToLower().Contains(searchterm))
                    {
                        searchtermsRemaining = new List<string>(searchtermsRemaining);
                        searchtermsRemaining.Remove(searchterm);
                    }
                }

                // If all searchterms are found, return early (for performance)
                if (searchtermsRemaining.Count == 0)
                    return true;
            }

            // All search terms found?
            return searchtermsRemaining.Count == 0;
        }

        #endregion

        private void HandleCellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Find the correct row
            var row = dataGridView1.SelectedRows.Cast<DataGridViewRow>().FirstOrDefault();
            if (row == null)
                return;

            // Find the cell
            var cell = row.Cells[e.ColumnIndex];
            if (cell == null || cell.Value == null)
                return;

            // Get the data from the cell
            var content = cell.Value.ToString().Replace("\\r", "").Replace("\\n", Environment.NewLine);

            // Might be json? Format if so.
            if (content.StartsWith("{") || content.StartsWith("["))
            {
                try
                {
                    content = JsonConvert.SerializeObject(JsonConvert.DeserializeObject(content), Formatting.Indented);
                }
                catch (Exception exception)
                {
                }
            }

            // Show message
            SelectableMessageBox.Show(this, content, "Logmessage");
        }

        public class Entity
        {
            public int LineNumber { get; set; }
            public Dictionary<string, string> Properties { get; set; } = new Dictionary<string, string>();
        }

        public IList<Entity> GetEntities(string filename)
        {
            var entities = new List<Entity>();
            using (var readstream = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (var reader = AvroContainer.CreateGenericReader(readstream))
            {
                int counter = 1;
                while (reader.MoveNext())
                {
                    string[] fields = new string[0];
                    if (reader.Schema is RecordSchema schema)
                    {
                        fields = schema.Fields.Select(f => f.FullName).ToArray();
                    }

                    foreach (var record in reader.Current.Objects.OfType<AvroRecord>())
                    {
                        if (_abortClicked)
                            return entities;

                        Entity ent = new Entity();
                        ent.LineNumber = counter++;

                        ReadAvroRecord(record, ent, null);

                        entities.Add(ent);
                    }
                }
            }
            return entities;
        }

        private void ReadAvroRecord(AvroRecord record, Entity ent, string prefix)
        {
            var fields = record.Schema.Fields.Select(f => f.FullName).ToArray();
            foreach (var field in fields)
            {
                object dataobj = record.GetField<object>(field);
                if (dataobj == null)
                {
                    ent.Properties.Add(GetFieldName(field, prefix), "null");
                }
                else if (dataobj is string str)
                {
                    ent.Properties.Add(GetFieldName(field, prefix), str);
                }
                else if (dataobj is byte[] data)
                {
                    ent.Properties.Add(GetFieldName(field, prefix), Encoding.UTF8.GetString(data));
                }
                else if (dataobj is Dictionary<string, string> dictstr)
                {
                    foreach (var kvp in dictstr)
                    {
                        ent.Properties.Add(GetFieldName(field, prefix) + "." + kvp.Key, kvp.Value);
                    }
                }
                else if (dataobj is Dictionary<string, object> dictobj)
                {
                    foreach (var kvp in dictobj)
                    {
                        if (kvp.Value is AvroRecord avrorecord)
                        {
                            ReadAvroRecord(avrorecord, ent, GetFieldName(field, prefix) + "." + kvp.Key);
                        }
                        else
                        {
                            ent.Properties.Add(GetFieldName(field, prefix) + "." + kvp.Key, kvp.Value?.ToString() ?? "null");
                        }
                    }
                }
                else if (dataobj is AvroRecord avrorecord)
                {
                    ReadAvroRecord(avrorecord, ent, GetFieldName(field, prefix));
                }
                else if (!dataobj.GetType().IsPrimitive)
                {
                    try
                    {
                        var jsonstr = JsonConvert.SerializeObject(dataobj);
                        ent.Properties.Add(GetFieldName(field, prefix), jsonstr ?? "null");
                    }
                    catch (Exception)
                    {
                        ent.Properties.Add(GetFieldName(field, prefix), "Could not create json representation for: " + dataobj?.ToString() ?? "null");
                    }
                }
                else
                {
                    ent.Properties.Add(GetFieldName(field, prefix), dataobj?.ToString() ?? "null");
                }
            }
        }

        private string GetFieldName(string fieldname, string prefix)
        {
            if (prefix == null)
                return fieldname;
            return prefix + "." + fieldname;
        }

        public void HandleAbortClicked(object sender, EventArgs e)
        {
            _abortClicked = true;
        }

        private void LoadFromFile(string filename)
        {
            statusStrip1.Visible = true;
            Text = AppVersion.AppName + " " + Version + " - " + Path.GetFileName(filename);
            PerformWork(() =>
            {
                // Load from file
                UpdateStatus("Loading...");
                _abortClicked = false;
                var entities = GetEntities(filename);

                // If there are no entities at all, add a dummy "no results" entity
                if (entities.Count == 0)
                {
                    entities.Add(new Entity
                    {
                        Properties = new Dictionary<string, string>()
                        {
                            { "Message", "No record in file found." }
                        }
                    });
                }

                if (_abortClicked)
                    UpdateStatus("Interrupted, processing what was read so far...");
                else
                    UpdateStatus("Processing...");

                // Determine the available property names by checking each entity
                string[] propertyNames = entities.SelectMany(entity => entity.Properties).Select(p => p.Key).Distinct().ToArray();

                _loadedColumns = new[] { "LineNumber" }.Concat(propertyNames).ToArray();
                _loadedRows = entities
                    .Select(entity => new[] { entity.LineNumber.ToString() }.Concat(propertyNames
                        .Select(propName => entity.Properties.ContainsKey(propName) ? entity.Properties[propName] : string.Empty))
                        .ToArray())
                    .ToArray();
                _filteredRows = _loadedRows;
            },
            () =>
            {
                UpdateStatus("Rendering table...");
                Application.DoEvents();
                ShowDataGridView();
                statusStrip1.Visible = false;

                // Dirty fix to get the horizontal scrollbar active after loading a file.
                Task.Factory.StartNew(async () =>
                {
                    Invoke(new Action(() => {
                        Width += 1;
                        Width -= 1;
                    }), null);
                    
                });
            });
        }

        private void UpdateStatus(string newmessage)
        {
            if (InvokeRequired)
            {
                Invoke((Action<string>)UpdateStatus, newmessage);
                return;
            }
            lblStatus.Text = newmessage;
        }

        private void ShowDataGridView()
        {
            // Make sure our datagrid is added (in case we switched to graph)
            if (!resultPanel.Contains(dataGridView1))
            {
                resultPanel.Controls.Clear();
                resultPanel.Controls.Add(dataGridView1);
            }

            // Setup our datagrid to our liking
            dataGridView1.ReadOnly = true;
            if (!dataGridView1.VirtualMode)
            {
                dataGridView1.VirtualMode = true;
                dataGridView1.CellValueNeeded += HandleCellValueNeeded;
                dataGridView1.CellFormatting += HandleCellFormatting;
            }
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AllowUserToResizeColumns = true; // Resizen mag wel
            dataGridView1.AllowUserToResizeRows = false;
            dataGridView1.AllowUserToOrderColumns = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dataGridView1.Columns.Clear();
            dataGridView1.Rows.Clear();

            // Create columns and determine width of each column
            using (var gfx = dataGridView1.CreateGraphics())
            {
                for (int i=0; i < _loadedColumns.Length; i++)
                {
                    var columnname = _loadedColumns[i];
                    var longestcontent = (from item in _loadedRows select i < item.Length ? item[i] : "").Aggregate("", (max, cur) => max.Length > cur.Length ? max : cur);
                    var colWidth = gfx.MeasureString(longestcontent, dataGridView1.Font);

                    var size = (int) Math.Ceiling(colWidth.Width);
                    size += 20;
                    var dgvc = new DataGridViewTextBoxColumn
                    {
                        FillWeight = 0.00001f,
                        Name = columnname,
                        HeaderText = columnname,
                        Width = size,
                        MinimumWidth = size
                    };
                    dataGridView1.Columns.Add(dgvc);
                }
            }

            // Rowcount goed zetten
            dataGridView1.Rows.Clear();
            dataGridView1.VirtualMode = true;
            dataGridView1.RowCount = _loadedRows.Length;

            // Als er een filter is, pas het filter toe...
            if (GetFilterTextSearchTerms() != null)
                HandleFilterKeyup(this, EventArgs.Empty);
        }

        string GetCellValue(int columnindex, int rowindex)
        {
            if (_filteredRows == null || _loadedColumns == null)
                return "";

            if (columnindex < 0 || columnindex >= _loadedColumns.Length)
                return "";

            if (rowindex < 0 || rowindex >= _filteredRows.Length)
                return "";

            string[] row = _filteredRows[rowindex];
            if (columnindex < row.Length)
                return row[columnindex] ?? "";
            else
                return "";
        }

        void HandleCellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            string[] highlightterms = (GetFilterTextSearchTerms() ?? new string[0]).Where(s => s.StartsWith("#")).Select(s => s.TrimStart('#')).ToArray();
            Color originalcolor = Color.FromKnownColor(KnownColor.Window);

            // Geen highlightterms, default color dus
            if (highlightterms.Length == 0)
            {
                if (e.CellStyle.BackColor != originalcolor)
                {
                    e.CellStyle.BackColor = originalcolor;
                    e.FormattingApplied = true;
                }
                return;
            }

            // Cell wel/niet formatten op basis van content
            string value = GetCellValue(e.ColumnIndex, e.RowIndex);
            foreach (string term in highlightterms)
            {
                if (value.ToLower().Contains(term))
                {
                    if (e.CellStyle.BackColor != Color.Orange)
                    {
                        e.CellStyle.BackColor = Color.Orange;
                        e.FormattingApplied = true;
                    }
                }
                else
                {
                    if (e.CellStyle.BackColor != originalcolor)
                    {
                        e.CellStyle.BackColor = originalcolor;
                        e.FormattingApplied = true;
                    }
                }
            }
        }

        void HandleCellValueNeeded(object sender, DataGridViewCellValueEventArgs e)
        {
            e.Value = GetCellValue(e.ColumnIndex, e.RowIndex);
        }

        private void HandleExitClick(object sender, EventArgs e)
        {
            Close();
        }

        private void HandleChangelogClicked(object sender, EventArgs e)
        {
            AppVersion.DisplayChanges();
        }

        private void HandleAboutClick(object sender, EventArgs e)
        {
            AppVersion.DisplayAbout();
        }

        private void CopyAllToClip()
        {
            dataGridView1.SelectAll();
            DataObject dataObj = dataGridView1.GetClipboardContent();
            Clipboard.SetDataObject(dataObj, true);
        }

        private void CopySelectionToClip()
        {
            DataObject dataObj = dataGridView1.GetClipboardContent();
            Clipboard.SetDataObject(dataObj, true);
        }

        private void exportToCsvToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExportToCsv();
        }

        private void copyAllToClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CopyAllToClip();
        }

        private void copySelectionToClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CopySelectionToClip();
        }

        private void exportSelectionToCsvToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExportToCsv(true);
        }

        private void ExportToCsv(Boolean selected = false)
        {
            String fileName = OpenSaveAsDialog();
            if (fileName == null)
                return;

            var sb = new StringBuilder();
            var headers = dataGridView1.Columns.Cast<DataGridViewColumn>();
            sb.Append(string.Join(",", headers.Select(column => "\"" + column.HeaderText + "\"")));
            sb.AppendLine();
            if (selected == true)
            {
                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                {
                    var cells = row.Cells.Cast<DataGridViewCell>();
                    sb.Append(string.Join(",", cells.Select(cell => "\"" + cell.Value + "\"")));
                    sb.AppendLine();
                }
            }
            else
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    var cells = row.Cells.Cast<DataGridViewCell>();
                    sb.Append(string.Join(",", cells.Select(cell => "\"" + cell.Value + "\"")));
                    sb.AppendLine();
                }
            }

            using (StreamWriter outfile = new StreamWriter(fileName))
            {
                outfile.Write(sb.ToString());
            }
        }

        private static String OpenLoadDialog()
        {
            OpenFileDialog fdOpen = new OpenFileDialog();
            fdOpen.Multiselect = false;
            //fdOpen.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            fdOpen.Filter = "avro files (*.avro)|*.avro|All files (*.*)|*.*";
            fdOpen.FilterIndex = 1;
            fdOpen.RestoreDirectory = true;
            if (fdOpen.ShowDialog() == DialogResult.OK)
                return fdOpen.FileName;
            return null;
        }

        private static String OpenSaveAsDialog()
        {
            SaveFileDialog fdSaveAs = new SaveFileDialog();
            fdSaveAs.InitialDirectory = "c:\\";
            fdSaveAs.Filter = "txt files (*.csv)|*.csv|All files (*.*)|*.*";
            fdSaveAs.FilterIndex = 2;
            fdSaveAs.RestoreDirectory = true;
            if (fdSaveAs.ShowDialog() == DialogResult.OK)
                return fdSaveAs.FileName;
            return null;
        }

        private void HandleFormClosed(object sender, FormClosedEventArgs e)
        {
            // Make sure the process actually terminates, and no background processes keep the application alive.
            // Kinda dirty. We do this as a precaution when bad programming causes another dialog to be left open, which
            // would result in the application not actually closing.
            Hide();
            Application.DoEvents();
            Environment.Exit(0);
        }

        private void HandleSubmitFeedbackClicked(object sender, EventArgs e)
        {
            new FeedbackForm().ShowDialog(this);
        }

        public void OnKeyDown(object sender, KeyEventArgs eventArgs)
        {
            if (eventArgs.Control && eventArgs.KeyCode == Keys.G)
            {
                GoToLine();
            }
        }

        private void GoToLine()
        {
            if (dataGridView1.Rows.Count <= 0)
                return;

            int startValue;
            int endValue;

            if (!int.TryParse(dataGridView1[0, 0].Value as string, out startValue))
            {
                MessageBox.Show(this, "Unexpected value in first column. Not a number.", $"I'm sorry, {Environment.UserName}. I'm afraid I can't do that.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!int.TryParse(dataGridView1[0, dataGridView1.RowCount - 1].Value as string, out endValue))
            {
                MessageBox.Show(this, "Unexpected value in first column. Not a number.", $"I'm sorry, {Environment.UserName}. I'm afraid I can't do that.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (var goToLineForm = new GoToLineForm(startValue, endValue))
            {
                var dialogResult = goToLineForm.ShowDialog();

                if (dialogResult == DialogResult.OK)
                {
                    var rowIndex = GetRowIndexByIndexValue(goToLineForm.LineNumber);

                    if (rowIndex != null)
                        dataGridView1.CurrentCell = dataGridView1.Rows[rowIndex.Value].Cells[0];
                }
            }
        }

        private int? GetRowIndexByIndexValue(string indexValue)
        {
            for (var rowIndex = 0; rowIndex < dataGridView1.RowCount; rowIndex++)
            {
                var rowIndexValue = dataGridView1[0, rowIndex].Value.ToString();

                if (rowIndexValue == indexValue)
                {
                    return rowIndex;
                }
            }

            return null;
        }

        private void HandleOpenClicked(object sender, EventArgs e)
        {
            var filename = OpenLoadDialog();
            if (filename == null)
                return;
            LoadFromFile(filename);
        }

        private void HandleDragEnter(object sender, DragEventArgs e)
        {
            var data = e.Data.GetData("FileNameW");
            if (data != null && data is string[])
            {
                string[] filenames = data as string[];
                if (filenames.Length == 0)
                    return;
                string filename = filenames.First();
                if (File.Exists(filename))
                    e.Effect = DragDropEffects.Copy;
            }
        }

        private void HandleDragDrop(object sender, DragEventArgs e)
        {
            var data = e.Data.GetData("FileNameW");
            if (data != null && data is string[])
            {
                string[] filenames = data as string[];
                if (filenames.Length == 0)
                    return;
                string filename = filenames.First();
                LoadFromFile(filename);
            }
        }

        private void HandleLoad(object sender, EventArgs e)
        {
            // Load file on startup
            if (_initialFilename != null)
                LoadFromFile(_initialFilename);
        }

        private void HandleExportAllToJson(object sender, EventArgs e)
        {
            ExportToJson(dataGridView1.Rows.OfType<DataGridViewRow>());
        }

        private void HandleExportSelectionToJson(object sender, EventArgs e)
        {
            ExportToJson(dataGridView1.SelectedRows.OfType<DataGridViewRow>());
        }

        private void ExportToJson(IEnumerable<DataGridViewRow> rows)
        {
            // Minimum of 1 row?
            if (rows.Count() == 0)
            {
                MessageBox.Show(this, "At least one row must be in the export.", "Yikes!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Find column index
            var bodycolumn = rows.First().DataGridView.Columns.Cast<DataGridViewColumn>().FirstOrDefault(dgvc => dgvc.HeaderText.ToLower() == "body");
            if (bodycolumn == null)
            {
                MessageBox.Show(this, "Cannot find column named \"Body\". This export was created with IoT Hub's .avro exported files in mind.", "Yikes!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Export as JSON array or objects?
            var text = @"Do you want to export to JSON Array or new-line separated JSON Objects?

Select ""YES"" (JSON array) for:
[
  { object },
  { object }
}

Select ""NO"" (New-line separated JSON objects) for:
{ object }
{ object }

Note that this export assumes the ""Body"" is a valid JSON object.";
            var res = MessageBox.Show(this, text, "JSON Export Type?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (res == DialogResult.Cancel)
                return;

            bool asJsonArray = res == DialogResult.Yes;

            // Where to save
            SaveFileDialog fdSaveAs = new SaveFileDialog();
            fdSaveAs.InitialDirectory = "c:\\";
            fdSaveAs.Filter = "json files (*.json)|*.json|All files (*.*)|*.*";
            fdSaveAs.FilterIndex = 1;
            fdSaveAs.RestoreDirectory = true;
            if (fdSaveAs.ShowDialog() != DialogResult.OK)
                return;

            // Create outfile
            using (StreamWriter outfile = new StreamWriter(fdSaveAs.FileName))
            {
                if (asJsonArray)
                {
                    outfile.WriteLine("[");
                }
                bool first = true;
                foreach (var row in rows)
                {
                    if (asJsonArray && !first)
                        outfile.Write(",");
                    outfile.WriteLine(row.Cells[bodycolumn.Index].Value + "");
                    first = false;
                }
                if (asJsonArray)
                {
                    outfile.WriteLine("]");
                }
            }
        }
    }
}

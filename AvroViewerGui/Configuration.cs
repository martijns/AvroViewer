using MsCommon.ClickOnce;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace AvroViewerGui
{
    [Serializable]
    public class Configuration : AppConfiguration<Configuration>
    {
        public Configuration()
        {
        }
    }
}

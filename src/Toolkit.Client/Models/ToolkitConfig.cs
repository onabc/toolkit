using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toolkit.Client
{
    public class ToolkitConfig
    {
        public BaduTranslatorConfig BaduTranslatorConfig { get; set; }
    }

    public struct BaduTranslatorConfig
    {
        public string AppId { get; set; }
        public string SecretKey { get; set; }
        public string BaseUrl { get; set; }
    }
}
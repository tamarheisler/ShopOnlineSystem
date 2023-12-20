using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Encodings;
using System.Text.Json;

namespace HtmlSerializer
{
    public class HtmlHelper
    {
        private readonly static HtmlHelper _helper = new HtmlHelper();
        public static HtmlHelper Helper()
        {
            return _helper;
        }
        public string[] HtmlVoidTags { get; set; }
        public string[] HtmlTags { get; set; }

        private HtmlHelper()
        {
            string contentHtmlVoidTags = File.ReadAllText("HtmlVoidTags.json");
            string contentHtmlTags = File.ReadAllText("HtmlTags.json");
            this.HtmlVoidTags = JsonSerializer.Deserialize<string[]>(contentHtmlVoidTags);
            this.HtmlTags = JsonSerializer.Deserialize<string[]>(contentHtmlTags);
        }
    }
}


using System.Text.RegularExpressions;

namespace HtmlSerializer
{
    public class Functions
    {
        public static List<string> getClasses(List<string> attributes)
        {
            List<string> classes = new List<string>();
            foreach (var att in attributes)
            {
                if (att.Split(" ")[0] == "class")
                {
                    classes = att.Split(' ').ToList();
                }
            }
            classes.Remove("class");
            return classes;
        }

        static string getId(List<string> attributes)
        {
            string id = null;
            foreach (var att in attributes)
            {
                if (att.Split(" ")[0] == "id")
                {
                    id = att.Split(' ')[1];
                }
            }
            return id;
        }
        public static List<string> getAllAttributes(string s)
        {
            List<string> allAttributes = new List<string>();
            MatchCollection matches = Regex.Matches(s, @"(\S+)=[""']([^""']+?)[""']");
            foreach (Match match in matches)
            {
                string attribute = match.Groups[1].Value;
                string value = match.Groups[2].Value;
                allAttributes.Add($"{attribute} {value}");
            }

            return allAttributes;
        }


        public static HtmlElement buildHtmlAttributesTree(List<string> allStrings)
        {

            HtmlElement root = new HtmlElement();
            HtmlElement currentElement = root;

            List<string> selfClosingTags = HtmlHelper.Helper().HtmlVoidTags.ToList();
            List<string> tags = HtmlHelper.Helper().HtmlTags.ToList();

            foreach (var remainingString in allStrings)
            {
                if (remainingString == "/html")
                {
                    currentElement.parent = null;
                    break;
                }
                else if (remainingString.StartsWith("/"))
                {
                    currentElement = currentElement.parent;
                }
                else if (selfClosingTags.Contains(remainingString.Split(" ")[0]) || remainingString.EndsWith("/"))
                {
                    HtmlElement newElem = new HtmlElement();
                    string allAttributes = Regex.Replace(remainingString, @"^\w+\s*", "");
                    newElem.attributes = getAllAttributes(allAttributes);
                    newElem.classes = getClasses(newElem.attributes);
                    newElem.id = getId(newElem.attributes);
                    newElem.name = remainingString.Split(" ")[0];
                    newElem.parent = currentElement;
                    newElem.childrens = null;
                }
                else if (tags.Contains(remainingString.Split(" ")[0]))
                {
                    HtmlElement newElem = new HtmlElement();
                    string allAttributes = Regex.Replace(remainingString, @"^\w+\s*", "");
                    newElem.attributes = getAllAttributes(allAttributes);
                    newElem.classes = getClasses(newElem.attributes);
                    newElem.id = getId(newElem.attributes);
                    newElem.name = remainingString.Split(" ")[0];
                    newElem.parent = currentElement;
                    currentElement.childrens.Add(newElem);
                    currentElement = newElem;
                }
                else
                {
                    currentElement.innerHtml = remainingString;
                }
            }
            return root;
        }

        public static void Descendants(HtmlElement root)
        {
            foreach (HtmlElement descendant in root.Descendants())
            {
                Console.WriteLine(descendant.name);
            }
        }

        public static void Ancestors(HtmlElement parent)
        {
            foreach (HtmlElement p in parent.Ancestors())
            {
                Console.WriteLine(p.name);
            }
        }

        public static async Task<string> Load(string url)
        {
            HttpClient client = new HttpClient();
            var response = await client.GetAsync(url);
            var html = await response.Content.ReadAsStringAsync();
            return html;
        }

    }
}

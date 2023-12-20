
using HtmlSerializer;
using HtmlSerializer;
using System.Text.RegularExpressions;

Console.WriteLine("Enter the website url:");
string? url = Console.ReadLine();
try
{
    await Functions.Load(url);
}
catch(System.InvalidOperationException ex)
{
    Console.WriteLine("Error: invalid url. \n" + ex.Message);
    return;
}
var html = await Functions.Load(url); ;
var cleanHtml = new Regex("[\n\t\r]").Replace(html, " ");
var htmlLines = new Regex("<(.*?)>").Split(cleanHtml).Where(s => s.Length > 0 && (!s.StartsWith(" ")));

HtmlElement dom = Functions.buildHtmlAttributesTree(htmlLines.ToList());
Console.WriteLine("Enter your query:");
string? userQuery = Console.ReadLine();
Selector s = Selector.FromQueryString(userQuery);
var result = dom.FindElementsBySelector(s);
result.ToList().ForEach(e => Console.WriteLine(e.ToString()));


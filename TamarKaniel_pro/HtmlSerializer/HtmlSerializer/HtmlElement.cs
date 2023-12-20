using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HtmlSerializer
{
    public class HtmlElement
    {
        public string id { get; set; }
        public string name { get; set; }
        public List<string> attributes { get; set; }
        public List<string> classes { get; set; }
        public  string innerHtml { get; set; }
        public HtmlElement parent { get; set; }
        public List<HtmlElement> childrens { get; set; }

        public HtmlElement()
        {
            attributes = new List<string>();
            classes = new List<string>();
            childrens = new List<HtmlElement>();
        }



        public IEnumerable<HtmlElement> Descendants()
        {
            Queue<HtmlElement> queue = new Queue<HtmlElement>();
            queue.Enqueue(this);

            while (queue.Count > 0)
            {
                HtmlElement element = queue.Dequeue();
                yield return element;

                foreach (HtmlElement child in element.childrens)
                {
                    queue.Enqueue(child);
                }
            }
        }

        public IEnumerable<HtmlElement> Ancestors()
        {
            HtmlElement parent = this.parent;

            while (parent != null)
            {
                yield return parent;
                parent = parent.parent;
            }
        }
        public override string ToString()
        {
            return $"HtmlElement:\n" +
                $"Name: {this.name}\n" +
                $"Id: {this.id}\n" +
                $"InnerHtml: {this.innerHtml}\n";
        }

        public List<HtmlElement> FindElementsBySelector( Selector selector)
        {
            List<HtmlElement> result = new List<HtmlElement>();
            FindElementsRecursive(this, selector, result);
            return result;
        }

        private void FindElementsRecursive(HtmlElement element, Selector selector, List<HtmlElement> result)
        {
            if (selector == null)
            {
                result.Add(element);
                return;
            }

            foreach (HtmlElement descendant in element.Descendants())
            {
                if (selector.Matches(descendant))
                {
                    if (selector.Child == null)
                    {
                        result.Add(descendant);
                    }
                    else
                    {
                        FindElementsRecursive(descendant, selector.Child, result);
                    }
                }
            }
        }

    }
}

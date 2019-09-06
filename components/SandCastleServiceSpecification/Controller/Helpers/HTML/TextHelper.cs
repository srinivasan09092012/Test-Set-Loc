using HtmlAgilityPack;
using System.Linq;

namespace Controller.Helpers.HTML
{
    public class TextHelper
    {
        private string _text;
        private HtmlDocument _htmlDoc;

        public TextHelper(HtmlDocument htmlDoc, string text)
        {
            this._text = text;
            this._htmlDoc = htmlDoc;
        }

        public void removeTagByTextClass(string containerTag)
        {
            try
            {
                var divNodes = _htmlDoc.DocumentNode.SelectNodes("//div[@class='" + containerTag + "']");

                foreach (var node in divNodes)
                {
                    foreach (var subNode in node.ChildNodes)
                    {
                        if (subNode.InnerHtml.TrimStart().TrimEnd().Trim() == this._text)
                        {
                            subNode.Remove();
                            break;
                        }

                        if (subNode.Name == "p")
                        {
                            if (subNode.InnerText.TrimStart().TrimEnd().Trim().EndsWith(this._text))
                            {
                                subNode.Remove();
                                break;
                            }
                        }
                    }
                }
            }
            catch
            {
            }
        }

        public void removeTagByText(string containerTag)
        {
            try
            {
                var divNodes = _htmlDoc.DocumentNode.SelectNodes("//div[@id='" + containerTag + "']").FirstOrDefault();

                foreach (var Node in divNodes.ChildNodes)
                {
                    if (Node.InnerHtml.TrimStart().TrimEnd().Trim() == this._text)
                    {
                        Node.Remove();
                        break;
                    }

                    if (Node.Name == "p")
                    {
                        if (Node.InnerText.TrimStart().TrimEnd().Trim() == this._text)
                        {
                            Node.Remove();
                            break;
                        }
                    }
                }
            }
            catch
            {
            }
        }

        public void removeTagByTextStartWith(string containerTag)
        {
            try
            {
                var divNodes = _htmlDoc.DocumentNode.SelectNodes("//div[@id='" + containerTag + "']").FirstOrDefault();

                foreach (var Node in divNodes.ChildNodes)
                {
                    if (Node.InnerHtml.TrimStart().TrimEnd().Trim().StartsWith(this._text))
                    {
                        Node.Remove();
                        break;
                    }

                    if (Node.Name == "p")
                    {
                        if (Node.InnerText.TrimStart().TrimEnd().Trim().StartsWith(this._text))
                        {
                            Node.Remove();
                            break;
                        }
                    }
                }
            }
            catch
            {
            }
        }

        public void renameTagByText(string containerTag, string oldLabel)
        {
            var divNodes = _htmlDoc.DocumentNode.SelectNodes("//div[@id='" + containerTag + "']").FirstOrDefault();

            foreach (var Node in divNodes.ChildNodes)
            {
                if (Node.InnerHtml == oldLabel)
                {
                    Node.InnerHtml = this._text;
                    break;
                }
                else if (Node.InnerText.EndsWith(oldLabel))
                {
                    Node.InnerHtml = Node.InnerHtml.Replace(oldLabel, this._text);
                    break;
                }
            }
            
        }
    }
}

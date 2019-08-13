using HtmlAgilityPack;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace APISvcSpec.Helpers.HTML
{
    public sealed class DocumentHelper
    {
        private static DocumentHelper _SharedInstance;

        private bool _documentLoaded = false;

        public string _documentTitle;
        public  string _documentPath;
        public Dictionary<string, HtmlDocument> _documentsQueue;

        public HtmlDocument _loadedDocument { get; set; }

        private DocumentHelper(HtmlDocument agilityHtmlDoc)
        {
            this._loadedDocument = agilityHtmlDoc;
            this._documentsQueue = new Dictionary<string, HtmlDocument>();
        }

        public static DocumentHelper GetInstance()
        {
            if (_SharedInstance == null)
            {
                _SharedInstance = new DocumentHelper(new HtmlDocument());
            }

            return _SharedInstance;
        }

        public void Load()
        {
            this._documentTitle = Path.GetFileName(this._documentPath);

            //if (this._documentsQueue.ContainsKey(this._documentTitle))
            //{
            //    this._loadedDocument = this._documentsQueue[this._documentTitle];
            //}
            //else
            //{
                if (File.Exists(this._documentPath))
                {
                    StringBuilder cleanHTMLDoc = new StringBuilder();
                    foreach (var line in File.ReadLines(this._documentPath))
                    {
                        if (line.Trim() != string.Empty)
                        {
                            cleanHTMLDoc.Append(line.TrimEnd().TrimStart());
                        }
                    }

                    this._loadedDocument.LoadHtml(cleanHTMLDoc.ToString());
                    this._documentLoaded = true;
                    cleanHTMLDoc.Clear();
                }
            //}
        }

        public void Save()
        {
            //if (this._documentsQueue.ContainsKey(this._documentTitle))
            //{
            //    this._documentsQueue.Remove(this._documentTitle);
            //}

            this._loadedDocument.Save(this._documentPath, Encoding.UTF8);
            //_documentsQueue.Add(this._documentTitle, this._loadedDocument); //TODO: failing adding same key twice
            
        }

        
    }
}

namespace Rc.Common.StrUtility
{
    using System;
    using System.Xml;

    public sealed class XMLUtility
    {
        private string _filePath;
        public XmlDocument xmlDoc = new XmlDocument();

        public XMLUtility(string filePath)
        {
            this._filePath = filePath;
            this.xmlDoc.Load(this._filePath);
        }

        public string GetValue(string key)
        {
            XmlNodeList elementsByTagName = this.xmlDoc.GetElementsByTagName(key);
            if (elementsByTagName.Count > 0)
            {
                return elementsByTagName[0].InnerText;
            }
            return null;
        }

        public void SetValue(string key, string value)
        {
            foreach (XmlNode node in this.xmlDoc.GetElementsByTagName(key))
            {
                node.InnerText = value;
            }
            this.xmlDoc.Save(this._filePath);
        }
    }
}


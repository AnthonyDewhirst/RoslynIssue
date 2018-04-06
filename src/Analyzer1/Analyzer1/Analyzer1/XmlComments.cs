using System;
using System.Xml.Linq;

namespace Analyzer1
{
    public sealed class XmlComments
    {
        public XmlComments(string xmlString)
        {
            if (!string.IsNullOrWhiteSpace(xmlString))
            {
                try
                {
                    XDocument.Parse(xmlString);
                    ValidXml = true;
                }
                catch (Exception)
                {
                }
            }
        }

        public bool ValidXml { get; }
    }
}
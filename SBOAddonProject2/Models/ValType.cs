using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SBOAddonProject2.Models
{
    [XmlRoot(ElementName = "ValType")]
    public class ValType
    {
        [XmlElement(ElementName = "Valute")]
        public List<Valute> Valute { get; set; }
        [XmlAttribute(AttributeName = "Type")]
        public string Type { get; set; }
    }
}

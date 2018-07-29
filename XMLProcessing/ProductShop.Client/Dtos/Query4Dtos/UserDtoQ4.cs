using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace ProductShop.Client.Dtos.Query4Dtos
{
    [XmlType("user")]
    public class UserDtoQ4
    {
        [XmlAttribute("first-name")]
        public string FirstName { get; set; }

        [XmlAttribute("last-name")]
        public string LastName { get; set; }

        [XmlAttribute("age")]
        public string Age { get; set; }

        [XmlArray("sold-products")]
        public SoldProductQ4[] ProductsSold { get; set; }
    }
}

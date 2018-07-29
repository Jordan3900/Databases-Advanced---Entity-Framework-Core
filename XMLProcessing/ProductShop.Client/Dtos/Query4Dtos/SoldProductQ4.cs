using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace ProductShop.Client.Dtos.Query4Dtos
{
    [XmlType("sold-products")]
    public class SoldProductQ4
    {
        [XmlAttribute("count")]
        public int Count { get; set; }

        [XmlAttribute("product")]
        public ProductDtoQ4[] ProductDto{ get; set; }
    }
}

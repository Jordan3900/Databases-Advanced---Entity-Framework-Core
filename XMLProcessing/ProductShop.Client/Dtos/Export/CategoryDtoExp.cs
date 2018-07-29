using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace ProductShop.Client.Dtos.Export
{
    [XmlType("Category")]
    public class CategoryDtoExp
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlElement("product-count")]
        public int Count { get; set; }

        [XmlElement("average-price")]
        public decimal AveragePrice { get; set; }

        [XmlElement("total-revenue")]
        public decimal TotalRevenue { get; set; }
    }
}

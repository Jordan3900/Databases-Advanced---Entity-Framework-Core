using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Serialization;

namespace ProductShop.Client.Dtos
{
    [XmlType("category")]
    public class CategoriesDto
    {
        [StringLength(15, MinimumLength = 3)]
        [XmlElement("name")]
        public string Name { get; set; }
    }
}

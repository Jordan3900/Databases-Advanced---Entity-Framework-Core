using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace ProductShop.Client.Dtos.Query4Dtos
{
    [XmlRoot("count")]
    public class UsersDtoQ4
    {
        [XmlAttribute("count")]
        public int Count { get; set; }

        [XmlAttribute("user")]
        public UserDtoQ4[] Users { get; set; }
    }
}

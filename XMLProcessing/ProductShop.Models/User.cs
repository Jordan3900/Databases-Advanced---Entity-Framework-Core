namespace ProductShop.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class User
    {
        public User()
        {
            this.SoldProducts = new HashSet<Product>();
            this.BoughtProducts = new HashSet<Product>();
        }

        public int Id { get; set; }

        public string FirstName { get; set; }

        [MinLength(3)]
        public string LastName { get; set; }

        public int? Age { get; set; }

        public ICollection<Product> SoldProducts { get; set; }
        public ICollection<Product> BoughtProducts { get; set; }
    }
}

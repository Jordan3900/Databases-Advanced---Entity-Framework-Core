namespace ProductShop.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public class Category
    {
        public Category()
        {
            this.CategoryProducts = new HashSet<CategoryProducts>();
        }

        public int Id { get; set; }

        [Range(3, 15)]
        public string Name { get; set; }

        public ICollection<CategoryProducts> CategoryProducts { get; set; }
    }
}

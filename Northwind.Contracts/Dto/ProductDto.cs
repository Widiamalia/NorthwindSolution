using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Contracts.Dto
{
    public class ProductDto
    {
        public long? ProductId { get; set; }
        public string Name { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Description Cannot be longer than 50 Characters.")]
        public string Description { get; set; }

        public decimal Price { get; set; }

        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Please select image")]

        //create link to category
        public IFormFile FilePhoto { get; set; }
    }
}

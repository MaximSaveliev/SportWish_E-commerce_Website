using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUseControl.Domain.Entities.Products
{
    public class ProductImg
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Image { get; set; }
        [Required]
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}

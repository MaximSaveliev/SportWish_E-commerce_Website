using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eUseControl.Domain.Entities.Products;

namespace eUseControl.Domain.Entities
{
    public class Cart
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public int CartId { get; set; }
        [Required]
        public int Quantity { get; set;}
        [Required]
        public int ProductId { get; set; }
        [NotMapped]
        public Product Product { get; set; }
    }
}

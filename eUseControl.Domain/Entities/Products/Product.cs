using eUseControl.Domain.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eUseControl.Domain.Entities.Products
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        public string BrandName { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public float RegularPrice { get; set; }
        public float? PromotionalPrice { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        public UGender Gender { get; set; }
        [Required]
        public string Thumbnail { get; set; }
        [Required]
        public ICollection<ProductImg> Images { get; set; }
        [Required]
        public ICollection<ProductSize> Sizes { get; set; }
    }
}

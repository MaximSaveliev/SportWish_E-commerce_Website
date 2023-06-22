using eUseControl.Domain.Entities.Products;
using eUseControl.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebAplication.Models
{
    public class ProductForm
    {
        [Display(Name = "Product Name"),
         Required(ErrorMessage = "You need to give Product Name.")]
        public string ProductName { get; set; }

        [Display(Name = "Brand Name"),
         Required(ErrorMessage = "You need to give Brand Name.")]
        public string BrandName { get; set; }

        [Required(ErrorMessage = "You need to give product Description.")]
        public string Description { get; set; }

        [Display(Name = "Price"),
         Required(ErrorMessage = "You need to give product Price.")]
        public float RegularPrice { get; set; }

        [Display(Name = "Promotional Price")]
        public float? PromotionalPrice { get; set; }

        [Display(Name = "Category"),
         Required(ErrorMessage = "You need to give product Category.")]
        public string Category { get; set; }

        [Display(Name = "Gender"),
         Required(ErrorMessage = "You need to give Gender.")]
        public UGender Gender { get; set; }

        [Required(ErrorMessage = "You need to provide a product Thumbnail.")]
        [DataType(DataType.ImageUrl)]
        public string Thumbnail { get; set; }

        [Required(ErrorMessage = "You need to provide a product Images.")]
        public List<ProductImg> Images { get; set; }

        [Required(ErrorMessage = "You need to provide a product available Sizes.")]
        public List<ProductSize> Sizes { get; set; }
    }
}
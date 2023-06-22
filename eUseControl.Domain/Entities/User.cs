using eUseControl.Domain.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eUseControl.Domain.Entities
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string NickName { get; set; }
        public string UserName { get; set; }
        public string UserSurname { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Email { get; set; }


        [Column(TypeName = "datetime2")]
        public DateTime LoginDateTime { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime RegisterDateTime { get; set; }

        [Required]
        public URole AccessLevel { get; set; }

        //public string CartProductIds { get; set; } = ";0;;1;;2;";
    }
}

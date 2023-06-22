using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eUseControl.Domain.Entities
{
    public class SDbModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SessionId { get; set; }

        [Required]
        [StringLength(50)]
        public string NickName { get; set; }

        //[Required]
        //public int UserId { get; set; }

        [Required]
        public string CookieString { get; set; }

        [Required]
        public DateTime ExpireTime { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gabbler.Models
{
    public class User
    {
        [Key]
        public int U_ID { get; set; }

        [Required(ErrorMessage = "You should input your name")]
        public string Username { get; set; }

        [Required(ErrorMessage = "You should input your password")]
        public string Password { get; set; }
        public string Age { get; set; }
        public string Sex { get; set; }
        public string Email { get; set; }

        public int AvatarID { get; set; }
        [ForeignKey("AvatarID")]
        public virtual Avatar Avatar { get; set; }

        public int BackgroundID { get; set; }
        [ForeignKey("BackgroundID")]
        public virtual Background Background { get; set; }
        public virtual ICollection<Gab> LikeGab { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gabbler.Models
{
    public class Gab
    {
        public Gab()
        {
            this.LikeUsers = new List<User>();
        }

        [Key]
        public int G_ID { get; set; }
        public int UserID  { get; set; }
        [ForeignKey("UserID")]
        public virtual User User { get; set; }
        public DateTime Date { get; set; }
        public string content { get; set; }
        public int likenumber { get; set; }

        public virtual ICollection<User> LikeUsers { get; set; }
    }
}
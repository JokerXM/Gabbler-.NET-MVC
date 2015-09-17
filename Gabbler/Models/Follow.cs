using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gabbler.Models
{
    public class Follow
    {
        [Key]
        public int F_ID { get; set; }
        public int UserID { get; set; }
        [ForeignKey("UserID")]
        public virtual User User { get; set; }
        public int UFollowID { get; set; }
        [ForeignKey("UFollowID")]
        public virtual User UFollow { get; set; }
    }
}
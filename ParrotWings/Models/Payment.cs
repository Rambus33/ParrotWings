using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParrotWings.Models
{
    public class Payment
    {
        public int PaymentId { get; set; }
        public double Amount { get; set; }
        public DateTime Date { get; set; }
        [NotMapped]
        public double Balance { get; set; }

        [ForeignKey("CorrespondentUser"), Column(Order = 0)]
        public int? CorrespondentUserId { get; set; }
        public virtual User CorrespondentUser { get; set; }

        [ForeignKey("User"), Column(Order = 1)]
        public int? UserId { get; set; }
        public virtual User User { get; set; }        
    }
}
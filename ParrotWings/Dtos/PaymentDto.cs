using System.ComponentModel.DataAnnotations;

namespace ParrotWings.Dtos
{
    public class PaymentDto
    {
        public int CorrespondentUserId { get; set; }
        [RegularExpression(@"^\d+(?:(\.|\,)\d{0,2})?$", ErrorMessage = "Amount is not valid")]
        [Range(0.01,double.MaxValue)]
        public double Amount { get; set; }
    }
}
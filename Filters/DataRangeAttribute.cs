using System.ComponentModel.DataAnnotations;

namespace FIsrtMVCapp.Filters
{
    public class DataRangeAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
                return new ValidationResult("Date is not given");

            DateTime dateVal = (DateTime)value;

            if (dateVal >DateTime.Now.AddYears(-150) && dateVal<= DateTime.Now)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("Date is not in given range");
            }
        }
    }
}

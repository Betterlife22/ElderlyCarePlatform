

using System.ComponentModel.DataAnnotations;

namespace BLL.DTO.BookingDTOs
{
    public class FutureDateTimeAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is DateTime dateTime)
            {
                DateTime now = DateTime.Now;

                // Kiểm tra ngày không được trong quá khứ
                if (dateTime.Date < now.Date)
                {
                    return new ValidationResult("Date can not in the past.");
                }

                // Nếu là hôm nay, kiểm tra giờ không trong quá khứ
                if (dateTime.Date == now.Date && dateTime.TimeOfDay < now.TimeOfDay)
                {
                    return new ValidationResult("Time can not in the past");
                }
            }

            return ValidationResult.Success;
        }
    }
}

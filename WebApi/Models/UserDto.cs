using Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class UserDto:IValidatableObject
    {
        [Required]
        [MaxLength(100)]
        public string FullName { get; set; }
        [Required]
        public int Age { get; set; }
        public GenderEnum Gender { get; set; }

        /// <summary>
        /// For Models that are not related to DataBase
        /// automaticlly add to ModelState
        /// </summary>
        /// <param name="validationContext"></param>
        /// <returns></returns>
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (FullName.Equals("test", StringComparison.OrdinalIgnoreCase))
            {
                yield return new ValidationResult("نام نمیتوند تست باشد", new[] {nameof(FullName) });
            }
            if (Age > 31 && Gender == GenderEnum.Male)
            {
                yield return new ValidationResult("برای مردها سن 31 سال به بالا معتبر نمی باشد", new[] {nameof(Age),nameof(Gender) });
            }
        }
    }
}

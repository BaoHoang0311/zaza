using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace ngay8.Models
{
    public class OneRequired : ValidationAttribute
    {
        private string otherProperty;
        public OneRequired(string otherProperty)
        {
            if (string.IsNullOrEmpty(otherProperty))
            {
                throw new ArgumentNullException("otherProperty");
            }

            this.otherProperty = otherProperty;
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            PropertyInfo? otherPropertyInfo = validationContext.ObjectInstance.GetType().GetProperty(otherProperty);

            var otherPropertyValue = otherPropertyInfo?.GetValue(validationContext.ObjectInstance, null);

            if (otherPropertyValue == null && value == null)
            {
                return new ValidationResult(string.Format("Please fill at least 1 of these fields."));
            }
            else if (otherPropertyValue != null && value != null)
            {
                return ValidationResult.Success;
            }
            return ValidationResult.Success;
        }
    }
    public class ValidationFrom : ValidationAttribute
    {
        public ValidationFrom()
        {
            ErrorMessage = "Ngày From không hợp lệ";
        }

        public override bool IsValid(object value)
        {
            if (value is DateTime day)
            {
                return day > new DateTime(2016, 01, 01);
            }
            return false;
        }
    }
    public class ValidationTo : ValidationAttribute
    {
        private string otherProperty;
        public ValidationTo(string otherProperty)
        {
            if (string.IsNullOrEmpty(otherProperty))
            {
                throw new ArgumentNullException("otherProperty");
            }

            this.otherProperty = otherProperty;
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            PropertyInfo? otherPropertyInfo = validationContext.ObjectInstance.GetType().GetProperty(otherProperty);

            var otherPropertyValue = otherPropertyInfo?.GetValue(validationContext.ObjectInstance, null);

            if (otherPropertyValue is DateTime o1 && value is DateTime o2 && o1 == o2)
            {
                return new ValidationResult(string.Format("Property '{0}' nam bang nhau.", otherProperty));
            }
            return ValidationResult.Success;
        }
    }
}

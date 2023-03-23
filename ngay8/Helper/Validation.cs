using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace ngay8.Helper
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
            PropertyInfo otherPropertyInfo = validationContext.ObjectInstance.GetType().GetProperty(otherProperty);

            var otherPropertyValue = otherPropertyInfo?.GetValue(validationContext.ObjectInstance, null);

            if (string.IsNullOrEmpty(otherPropertyValue?.ToString()) && string.IsNullOrEmpty(value?.ToString()))
            {
                return new ValidationResult(string.Format("Please fill at least 1 of these fields."));
            }
            else if (!string.IsNullOrEmpty(otherPropertyValue?.ToString()) && !string.IsNullOrEmpty(value?.ToString()))
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
                return day >= new DateTime(2016, 01, 01);
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
            PropertyInfo otherPropertyInfo = validationContext.ObjectInstance.GetType().GetProperty(otherProperty);

            var otherPropertyValue = otherPropertyInfo?.GetValue(validationContext.ObjectInstance, null);
            if (otherPropertyValue is DateTime o1 && value is DateTime o2 && o1 == o2)
            {
                //o2 += new TimeSpan(23, 59, 59);
                return new ValidationResult(string.Format("Property '{0}' nam bang nhau.", otherProperty));
            }
            return ValidationResult.Success;
        }
    }
}

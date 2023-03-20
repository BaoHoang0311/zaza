using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace ngay8.Models
{
    public partial class SearchVM
    {
        public int op { get; set; }
        [OneRequired("AgentName")]
        [StringLength(100, MinimumLength = 3)]
        public string? AgentCEANO { get; set; }

        [OneRequired("AgentCEANO")]
        [StringLength(100, MinimumLength = 3)]
        public string? AgentName { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập")]
        [ValidationFrom]
        public DateTime? From { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập")]
        [ValidationTo("From")]
        public DateTime? To { get; set; }
    }
    public partial class SearchVM
    {
        // Paging
        public int Page { get; set; } = 4;
        public int PageSize { get; set; } = 25;
        public int TotalRecord { get; set; }
    }

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
            if(value is DateTime day)
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

            if ( otherPropertyValue is DateTime o1 && value is DateTime o2 && o1 == o2)
            {
                return new ValidationResult(string.Format("Property '{0}' nam bang nhau.", otherProperty));
            }
            return ValidationResult.Success;
        }
    }
}

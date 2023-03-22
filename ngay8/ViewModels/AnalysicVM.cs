using ngay8.Helper;
using System.ComponentModel.DataAnnotations;

namespace ngay8.ViewModels
{
    public class AnalysicVM
    {
        [OneRequired("AgentName")]
        [StringLength(100, MinimumLength = 3)]
        public string AgentCEANO { get; set; }

        [OneRequired("AgentCEANO")]
        [StringLength(100, MinimumLength = 3)]
        public string AgentName { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập")]
        [ValidationFrom]
        public DateTime? From { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập")]
        [ValidationTo("From")]
        public DateTime? To { get; set; }
    }
}

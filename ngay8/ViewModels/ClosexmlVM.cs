using ngay8.Helper;
using System.ComponentModel.DataAnnotations;

namespace ngay8.ViewModels
{
    public class ClosexmlVM
    {
        [Required(ErrorMessage = "Vui lòng nhập")]
        [ValidationFrom]
        public DateTime? From { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập")]
        [ValidationTo("From")]
        public DateTime? To { get; set; }
    }
}

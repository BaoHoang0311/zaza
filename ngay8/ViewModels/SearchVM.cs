using ngay8.Helper;
using System.ComponentModel.DataAnnotations;

namespace ngay8.ViewModels
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
        public string fileName { get; set; }
    }
    public partial class SearchVM
    {
        public int? Page { get; set; } = 1;
        public int? PageSize { get; set; } = 25;
        public int TotalRecord { get; set; }
    }
}

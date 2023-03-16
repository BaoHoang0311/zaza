using MediatR;
using Service.Application.DTOs;

namespace ngay8.ViewModel
{
    public class SearchDetailVM 
    {
        public int op { get; set; }
        public string? AgentCEANO { get; set; }
        public string? AgentName { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
    }
}

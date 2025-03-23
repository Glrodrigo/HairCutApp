using HairCut.Tools.Domain;

namespace HairCutApp.Domain
{
    public class ScheduleDomain
    {
        public int UserId { get; set; }
        public string Phone { get; set; }
        public DateTime Date { get; set; }
        public string? Notes { get; set; }
    }
}


namespace HairCut.Tools.Domain
{
    public class ScheduleResult
    {

        public string Phone { get; set; }
        public string ServiceDescription { get; set; }
        public int Duration { get; set; }
        public double Price { get; set; }
        public string Date { get; set; }
        public string Done { get; set; }

        public class ScheduleTotalResults
        {
            public List<ScheduleResult> Schedules { get; set; }
            public int TotalPages { get; set; }

            public ScheduleTotalResults()
            {
                Schedules = new List<ScheduleResult>();
            }
        }
    }
}

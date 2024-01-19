namespace ngay8.Models
{
    public class GraphData
    {
        public int Month { get; set; }
        public int Year { get; set; }
        public double GrossValue { get; set; }
        public double NetValue { get; set; }
        public string Content { get;set;}
        public Grap1 grap { get;set;}
        public List<string> Str { get;set;}
    }
    public class Grap1
    {
        public string Name { get;set;}
        public string Id { get;set;}
    }
}

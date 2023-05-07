using Newtonsoft.Json;

namespace ngay8.Data
{
    public class JsonToDoList
    {
        [JsonProperty("Id")]
        public int Id { get; set; }
        [JsonProperty("Status")]

        public string Status { get; set; }
        [JsonProperty("Summary")]

        public string Summary { get; set; }
        [JsonProperty("Type")]

        public string Type { get; set; }
        [JsonProperty("Priority")]

        public string Priority { get; set; }
        [JsonProperty("Tags")]

        public string Tags { get; set; }
        [JsonProperty("Estimate")]

        public object Estimate { get; set; }
        [JsonProperty("Assignee")]

        public string Assignee { get; set; }
        [JsonProperty("RankId")]

        public int RankId { get; set; }
    }

}

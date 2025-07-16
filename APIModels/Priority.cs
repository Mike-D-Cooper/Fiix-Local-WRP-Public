using System.Text.Json.Serialization;

namespace Local_WRP.APIModels
{
    public class Priority
    {
        public class FindRequestResults
        {
            public string _maCn { get; set; }
            [JsonPropertyName("objects")]
            public List<Point> Points { get; set; }
            public int totalObjects { get; set; }

            public class Point
            {
                public string className { get; set; }
                public int id { get; set; }
                public int intSysCode { get; set; }
                public int intOrder { get; set; }
                public long intUpdated { get; set; }
                public string strName { get; set; }
                public string strUuid { get; set; }
                public bool dirty { get; set; }
                public bool _new { get; set; }
                public bool uideleted { get; set; }
            }
        }

    }
}

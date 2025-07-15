using System.Text.Json.Serialization;

namespace Local_WRP.APIModels
{
    public class MaintenanceType
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
                public long intUpdated { get; set; }
                public string strColor { get; set; }
                public string strName { get; set; }
                public string strUuid { get; set; }
                public string strUniqueKey { get; set; }
                public bool dirty { get; set; }
                public bool _new { get; set; }
                public bool uideleted { get; set; }
                public int intSysCode { get; set; }
            }
        }        

    }
}

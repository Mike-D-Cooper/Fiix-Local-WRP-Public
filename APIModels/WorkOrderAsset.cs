using System.Text.Json.Serialization;

namespace Local_WRP.APIModels
{
    public class WorkOrderAsset
    {
        public class AddRequest
        {
            public string _maCn { get; set; } = "AddRequest";
            public Clientversion clientVersion { get; set; } = new Clientversion();
            public string className { get; set; } = "WorkOrderAsset";
            public string fields { get; set; } = "*";
            [JsonPropertyName("object")]
            public Point Pnt { get; set; }
            public class Point
            {
                public string className { get; set; } = "WorkOrderAsset";
                public long intWorkOrderID { get; set; }
                public long intAssetID { get; set; }
            }
        }
    }
}

using System.Text.Json.Serialization;

namespace Local_WRP.APIModels
{
    public class WorkOrderTask
    {
        public class FindRequestResults
        {
            public string _maCn { get; set; }
            [JsonPropertyName("objects")]
            public List<Point> Points { get; set; }
            public class Point
            {
                public string className { get; set; }
                public int id { get; set; }
                public double dblTimeSpentHours { get; set; }
                public int intOrder { get; set; }
                public int intTaskGroupControlID { get; set; }
                public int intTaskType { get; set; }
                public object intUpdated { get; set; }
                public int intWorkOrderID { get; set; }
                public string strDescription { get; set; }
                public string strUuid { get; set; }
                public bool dirty { get; set; }
                public bool @new { get; set; }
                public bool uideleted { get; set; }
                public double? dblTimeEstimatedHours { get; set; }
                public int? intMeterReadingUnitID { get; set; }
                public string strTaskNotesCompletion { get; set; }
            }
            public int totalObjects { get; set; }
            
        }
    }
}

using System.Text.Json.Serialization;

namespace Local_WRP.APIModels
{
    public class WorkOrder
    {
        public class AddRequest
        {
            public string _maCn { get; set; } = "AddRequest";
            public Clientversion clientVersion { get; set; } = new Clientversion();
            public string className { get; set; } = "WorkOrder";
            public string fields { get; set; } = "strCode, id";
            [JsonPropertyName("object")]
            public Point Pnt { get; set; } = new Point();
            public class Point
            {
                public string className { get; set; } = "WorkOrder";
                public long intPriorityID { get; set; }
                public string strDescription { get; set; }
                public long intMaintenanceTypeID { get; set; }
                public long intWorkOrderStatusID { get; set; }
                public long intSiteID { get; set; }
            }
        }

        public class AddRequestResults
        {
            public string _maCn { get; set; }
            [JsonPropertyName("object")]
            public Point Points { get; set; }
            public int count { get; set; }
            public class Point
            {
                public string className { get; set; }
                public int id { get; set; }
                public string strCode { get; set; }
                public bool dirty { get; set; }
                public bool _new { get; set; }
                public bool uideleted { get; set; }
            }
        }

        public class FindRequestResults
        {
            public string _maCn { get; set; }
            [JsonPropertyName("objects")]
            public List<Point> Points { get; set; } = new List<Point>();
            public int totalObjects { get; set; }

            public class Point
            {
                public string className { get; set; }
                public int id { get; set; }
                public int bolLocked { get; set; }
                public long dtmDateCreated { get; set; }
                public int intRequestedByUserID { get; set; }
                public int intSiteID { get; set; }
                public object intUpdated { get; set; }
                public int intWorkOrderStatusID { get; set; }
                public string strCode { get; set; }
                public string strDescription { get; set; }
                public string strNameUserGuest { get; set; }
                public string strUuid { get; set; }
                public int statusId { get; set; }
                public bool dirty { get; set; }
                public bool @new { get; set; }
                public bool uideleted { get; set; }
                public long dtmDateLastModified { get; set; } = 0;
                public int? intAccountID { get; set; }
                public int? intChargeDepartmentID { get; set; }
                public int? intLastModifiedByUserID { get; set; }
                public int? intMaintenanceTypeID { get; set; }
                public string strAssetIds { get; set; }
                public string strAssets { get; set; }
                public long? dtmSuggestedCompletionDate { get; set; }
                public int? intPriorityID { get; set; }
                public int? intScheduledMaintenanceID { get; set; }
                public string strAssignedUserIds { get; set; }
                public string strAssignedUsers { get; set; }
                public long? dtmSuggestedStartDate { get; set; }
                public long? dtmDateCompleted { get; set; }
                public int? intCompletedByUserID { get; set; }
                public string strCompletedByUserIds { get; set; }
                public string strCompletedByUsers { get; set; }
                public string strCompletionNotes { get; set; }
                public int LocalWOId { get; set; }
                public ExtraFields extraFields { get; set; } = new ExtraFields();
            }

            public class ExtraFields
            {
                public string dv_intSiteID { get; set; }
                public string dv_intMaintenanceTypeID { get; set; }
            }
        }
    }
}

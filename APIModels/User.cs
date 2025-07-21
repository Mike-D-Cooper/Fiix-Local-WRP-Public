using System.Text.Json.Serialization;

namespace Local_WRP.APIModels
{
    public class User
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
                public long id { get; set; }
                public int bolGroup { get; set; }
                public int bolPushNotificationMessages { get; set; }
                public int intLocalizationID { get; set; }
                public long intUpdated { get; set; }
                public int intUserStatusID { get; set; }
                public string strFullName { get; set; }
                public string strUuid { get; set; }
                public bool dirty { get; set; }
                public bool _new { get; set; }
                public bool uideleted { get; set; }
                public int bolApiApplicationUser { get; set; }
                public int bolApiManaged { get; set; }
                public string strDefaultLoginLocation { get; set; }
                public string strEmailAddress { get; set; }
                public string strPreferences { get; set; }
                public string strUserName { get; set; }
                public string strUserTitle { get; set; }
                public float dblHourlyRate { get; set; }
                public string strPersonnelCode { get; set; }
                public string strAddress1 { get; set; }
                public int intCountryID { get; set; }
                public string strCity { get; set; }
                public string strPostalCode { get; set; }
                public string strState { get; set; }
                public string strTelephone { get; set; }
                public string strBusinessIds { get; set; }
            }
        }

    }
}

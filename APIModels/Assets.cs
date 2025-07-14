using System.Text.Json.Serialization;

namespace Local_WRP.APIModels
{
    public class Assets
    {
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
                public int bolIsBillToFacility { get; set; }
                public int bolIsOnline { get; set; }
                public int bolIsRegion { get; set; }
                public int bolIsShippingOrReceivingFacility { get; set; }
                public int bolIsSite { get; set; }
                public double dblWeightedPrice { get; set; }
                public int intAccountID { get; set; }
                public int? intAssetParentID { get; set; }
                public string ParentAssetName { get; set; }
                public string ParentAssetUUID { get; set; }
                public int intCategoryID { get; set; }
                public int intChargeDepartmentID { get; set; }
                public int intCountryID { get; set; }
                public int intKind { get; set; }
                public int intSiteID { get; set; }
                public int intSuperCategorySysCode { get; set; }
                public object intUpdated { get; set; }
                public double qtyStockCount { get; set; }
                public string strAisle { get; set; }
                public string strBinNumber { get; set; }
                public string strCity { get; set; }
                public string strCode { get; set; } = "";
                public string strDescription { get; set; }
                public string strMake { get; set; }
                public string strModel { get; set; }
                public string strName { get; set; } = "";
                public string strNotes { get; set; }
                public string strPostalCode { get; set; }
                public string strProvince { get; set; }
                public string strRow { get; set; }
                public string strSerialNumber { get; set; }
                public string strUuid { get; set; }
                public string strAddressParsed { get; set; }
                public bool dirty { get; set; }
                public bool @new { get; set; }
                public bool uideleted { get; set; }
                public int? intAssetLocationID { get; set; }
                public string strRFQTriggerSiteLevelSetting { get; set; }
                public string strTimezone { get; set; }
                public string strStockLocation { get; set; }
                public string strVendorIds { get; set; }
                public double? dblLastPrice { get; set; }
                public string strBarcode { get; set; }
                public string strInventoryCode { get; set; }
                public double? dblLongitude { get; set; }
                public double? dblLatitude { get; set; }
                public string dv_intCategoryID { get; set; }
                public string dv_intChargeDepartmentID { get; set; }
                public ExtraFields extraFields { get; set; } = new ExtraFields();
                public class ExtraFields
                {
                    public string dv_intCategoryID { get; set; }
                }
                public string strShippingTerms { get; set; }
                public string strAddress { get; set; }
                public string strMASourceProduct { get; set; }
                public string strUnspcCode { get; set; }
                public string strQuotingTerms { get; set; }
            }
        }
        public class FindSitesRequest
        {
            public string _maCn { get; set; } = "FindRequest";
            public Clientversion clientVersion { get; set; } = new Clientversion();
            public string className { get; set; } = "Asset";
            public string fields { get; set; } = "*";
            public List<Filter> filters { get; set; } = new List<Filter>();

            public class Filter
            {
                public string ql { get; set; }
                public List<int> parameters { get; set; }
            }
        }

    }
}

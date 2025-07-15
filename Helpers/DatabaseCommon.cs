using Local_WRP.Data;
using System.Text.Json;
using static Local_WRP.Components.Pages.Admin.TenantDetails;

namespace Local_WRP.Helpers
{
    public class DatabaseCommon
    {
        public static void GetAPICreds(ApplicationDbContext _db, CallFiixApi FiixAPI)
        {
            var apiDetails = _db.TenantDetails.FirstOrDefault();
            if (apiDetails != null)
            {
                FiixAPI.AppKey = apiDetails.AppKey;
                FiixAPI.AccessKey = apiDetails.AccessKey;
                FiixAPI.Secret = apiDetails.SecretKey;
                FiixAPI.BaseUrl = "https://" + apiDetails.TenantName + ".macmms.com";
            }
        }

        public static long? CheckIfSitesAreLimited(ApplicationDbContext _db)
        {
            var tenantDetails = _db.TenantDetails.FirstOrDefault();
            if(tenantDetails != null && tenantDetails.LimitToSite.HasValue)
            {
                return tenantDetails.LimitToSite.Value;
            }
            else
            {
                return null;
            }
        }

        public static async Task<List<SiteDetail>> GetSiteList(CallFiixApi FiixAPI)
        {
            List<SiteDetail> Sites = new List<SiteDetail>();
            var request = new APIModels.Assets.FindRequestWithFilter();
            request.filters.Add(new APIModels.Assets.FindRequestWithFilter.Filter
            {
                ql = "bolIsSite = ? ",
                parameters = new List<long> { 1 } // Fix: Initialize the List<int> directly instead of using an array.
            });

            string requestStr = JsonSerializer.Serialize(request);

            var results = await FiixAPI.GetServerResponce<APIModels.Assets.FindRequestResults>(requestStr);
            if (results != null && results.Points.Count() > 0)
            {
                Sites = new List<SiteDetail>();
                Sites = results.Points.OrderBy(x => x.strName).Select(x => new SiteDetail
                {
                    Id = x.id,
                    Code = x.strCode,
                    Name = x.strName
                }).ToList();
            }

            return Sites;
        }
        public class SiteDetail
        {
            public long Id { get; set; }
            public string Code { get; set; }
            public string Name { get; set; }
        }
    }
    
}

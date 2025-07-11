using System.Diagnostics;
using System.Security.Cryptography;
using System.Text.Json;

namespace Local_WRP
{
    public class CallFiixApi
    {
        public string BaseUrl = "";
        public string AccessKey = "";
        public string AppKey = "";
        public string Secret = "";

        public async Task<string> GetServerResponce(string BodyJsonObjStr)
        {
            string uri = getRequestUri(BaseUrl, AccessKey, AppKey);
            string ReturnContent = "";
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", getAuth(BaseUrl, AccessKey, AppKey, Secret));

                var content = new StringContent(BodyJsonObjStr, System.Text.Encoding.UTF8, "application/json");

                var response = await client.PostAsync(uri, content);

                response.EnsureSuccessStatusCode();

                ReturnContent = await response.Content.ReadAsStringAsync();
            }
            Thread.Sleep(10);
            return ReturnContent;
        }

        public async Task<T> GetServerResponce<T>(object request)
        {
            string uri = getRequestUri(BaseUrl, AccessKey, AppKey);
            string ReturnContent = "";
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", getAuth(BaseUrl, AccessKey, AppKey, Secret));

                var content = new StringContent(JsonSerializer.Serialize(request), System.Text.Encoding.UTF8, "application/json");

                var response = await client.PostAsync(uri, content);

                response.EnsureSuccessStatusCode();

                ReturnContent = await response.Content.ReadAsStringAsync();
            }
            Thread.Sleep(10);

            var result = JsonSerializer.Deserialize<T>(ReturnContent);

            return result;
        }

        public async Task<T> GetServerResponce<T>(string request)
        {
            string uri = getRequestUri(BaseUrl, AccessKey, AppKey);
            string ReturnContent = "";
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", getAuth(BaseUrl, AccessKey, AppKey, Secret));

                var content = new StringContent(request, System.Text.Encoding.UTF8, "application/json");

                var response = await client.PostAsync(uri, content);

                response.EnsureSuccessStatusCode();

                ReturnContent = await response.Content.ReadAsStringAsync();
            }
            Thread.Sleep(10);

            Console.WriteLine(ReturnContent);

            var result = JsonSerializer.Deserialize<T>(ReturnContent);

            return result;
        }

        public async Task<string> PostPutServerResponceV6(string Uri, string JWT, string BodyJsonObjStr, bool Post = true)
        {
            JWT = JWT.Replace("Bearer ", "");

            string ReturnContent = "";
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + JWT);

                var content = new StringContent(BodyJsonObjStr, System.Text.Encoding.UTF8, "application/json");

                if (Post)
                {
                    HttpResponseMessage response = await client.PostAsync(Uri, content);
                    //response.EnsureSuccessStatusCode();
                    ReturnContent = await response.Content.ReadAsStringAsync();
                }
                else
                {
                    HttpResponseMessage response = await client.PutAsync(Uri, content);
                    //response.EnsureSuccessStatusCode();
                    ReturnContent = await response.Content.ReadAsStringAsync();
                }
            }
            return ReturnContent;
        }

        public async Task<string> GetServerResponceV6(string Uri, string JWT)
        {
            Debug.WriteLine("Started v6 API Call @ " + DateTime.Now.ToShortTimeString());
            JWT = JWT.Replace("Bearer ", "");
            string ReturnContent = "";
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + JWT);

                var response = await client.GetAsync(Uri);

                try
                {
                    var httpResp = response.EnsureSuccessStatusCode();
                    ReturnContent = await response.Content.ReadAsStringAsync();
                }
                catch
                {
                    return "";
                }
            }
            Debug.WriteLine("Completed v6 API Call @ " + DateTime.Now.ToShortTimeString());
            Thread.Sleep(100);

            return ReturnContent;
        }

        public async Task<string> GetV6Token(string Key, string Secret)
        {
            string ReturnContent = "";
            using (var client = new HttpClient())
            {
                var content = new StringContent("{\"apiKey\":\"" + Key + "\",\"secret\":\"" + Secret + "\"}", System.Text.Encoding.UTF8, "application/json");

                var response = await client.PostAsync("https://api.fiix.software/public-gateway/login", content);

                response.EnsureSuccessStatusCode();

                ReturnContent = await response.Content.ReadAsStringAsync();
            }
            Thread.Sleep(100);
            return ReturnContent;
        }

        public async Task DownloadFileAsync(long FileContentsId, string Filename, string LocalPath)
        {
            string uri = getRequestUri(BaseUrl, AccessKey, AppKey, 2, FileContentsId);

            using (HttpClient client = new HttpClient())
            {
                string authCode = getAuth(BaseUrl, AccessKey, AppKey, Secret, 2, FileContentsId);
                client.DefaultRequestHeaders.Add("Authorization", authCode);
                HttpResponseMessage response = await client.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    // Ensure the directory exists
                    Directory.CreateDirectory(LocalPath);

                    using (var fs = new FileStream(Path.Combine(LocalPath, Filename), FileMode.Create, FileAccess.Write, FileShare.None))
                    {
                        await response.Content.CopyToAsync(fs);
                    }

                    Console.WriteLine("File downloaded successfully.");
                }
                else
                {
                    Console.WriteLine($"Failed to download file. Status code: {response.StatusCode}");
                }
            }
        }

        public async Task<byte[]> DownloadFileAsync(long FileContentsId)
        {
            string uri = getRequestUri(BaseUrl, AccessKey, AppKey, 2, FileContentsId);

            using (HttpClient client = new HttpClient())
            {
                string authCode = getAuth(BaseUrl, AccessKey, AppKey, Secret, 2, FileContentsId);
                client.DefaultRequestHeaders.Add("Authorization", authCode);
                HttpResponseMessage response = await client.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsByteArrayAsync();
                }
                else
                {
                    Console.WriteLine($"Failed to download file. Status code: {response.StatusCode}");
                }

                return null;
            }
        }

        public async Task<string> PostFileUpload(string Filename, List<CallFiixApi.FileUploadDescription> BodyJsonObjStr, StreamContent fileStreamContent, string contentType = "")
        {
            string uri = getRequestUri(BaseUrl, AccessKey, AppKey, 1);
            string ReturnContent = "";

            using (var client = new HttpClient())
            using (var multipartFormContent = new MultipartFormDataContent())
            {
                using (var fileContent = new ByteArrayContent(await fileStreamContent.ReadAsByteArrayAsync()))
                {
                    string authCode = getAuth(BaseUrl, AccessKey, AppKey, Secret, 1);
                    client.DefaultRequestHeaders.Add("Authorization", authCode);
                    string StrContent = JsonSerializer.Serialize(BodyJsonObjStr);
                    var content = new StringContent(StrContent, System.Text.Encoding.UTF8, "application/json");

                    multipartFormContent.Add(content, "descriptions");
                    multipartFormContent.Headers.ContentEncoding.Add("gzip");

                    fileContent.Headers.Remove("Content-Type");
                    fileContent.Headers.Add("Content-Type", contentType != "" ? contentType : "multipart/form-data; boundary=----WebKitFormBoundaryaYcxA3wAp5XMUV2w");

                    multipartFormContent.Add(fileContent, "file1", Filename);
                    var response = await client.PostAsync(uri, multipartFormContent);
                    //response.EnsureSuccessStatusCode();
                    ReturnContent = await response.Content.ReadAsStringAsync();
                }
            }

            return ReturnContent;
        }

        public class FileUploadDescription
        {
            public string sourceInfo { get; set; }
            public string sourceIdString { get; set; }
            public string fieldName { get; set; }
        }

        public String getRequestUri(string BaseUrl, string AccessKey, string AppKey, int IsFileUpload = 0, long FileContentsId = 0)
        {
            BaseUrl = BaseUrl.Replace(".com/", ".com");
            String requestUri = String.Format("{2}/api/?appKey={0}&accessKey={1}&signatureMethod=HmacSHA256&signatureVersion=1", AppKey, AccessKey, BaseUrl);

            if (IsFileUpload == 1)
            {
                requestUri = String.Format("{2}/api/upload?action=uploadFile&appKey={0}&accessKey={1}&signatureMethod=HmacSHA256&signatureVersion=1", AppKey, AccessKey, BaseUrl);
            }

            //file download
            if (IsFileUpload == 2)
            {
                requestUri = String.Format("{2}/api/download/{3}?action=downloadFile&appKey={0}&accessKey={1}&signatureMethod=HmacSHA256&signatureVersion=1", AppKey, AccessKey, BaseUrl, FileContentsId);
            }

            return requestUri;
        }

        public String getAuth(string BaseUrl, string AccessKey, string AppKey, string Secret, int IsFileUpload = 0, long FileContentsId = 0)
        {
            String requestUri = getRequestUri(BaseUrl, AccessKey, AppKey, IsFileUpload, FileContentsId);
            if (requestUri.IndexOf("http://") == 0)
            {
                requestUri = requestUri.Substring(7);
            }
            else if (requestUri.IndexOf("https://") == 0)
            {
                requestUri = requestUri.Substring(8);
            }

            byte[] requestUriBytes = System.Text.Encoding.UTF8.GetBytes(requestUri);

            byte[] secretKeyBytes = System.Text.Encoding.UTF8.GetBytes(Secret);

            HMACSHA256 hmac = new HMACSHA256(secretKeyBytes);
            byte[] hashValue = hmac.ComputeHash(requestUriBytes);
            return String.Concat(Array.ConvertAll(hashValue, x => x.ToString("X2"))).ToLower();
        }

        public async Task<bool> PingTenant()
        {
            string Request = "{\"_maCn\":\"RpcRequest\",\"clientVersion\":{\"major\":2,\"minor\":8,\"patch\":1},\"name\":\"Ping\"}";
            var Responce = await GetServerResponce(Request);
            if (Responce != null && !Responce.ToLower().Contains("error"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

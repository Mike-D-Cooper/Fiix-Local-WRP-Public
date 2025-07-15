namespace Local_WRP.APIModels
{
    public class General
    {
        public class FindRequest
        {
            public string _maCn { get; set; } = "FindRequest";
            public Clientversion clientVersion { get; set; } = new Clientversion();
            public string className { get; set; } = "";
            public string fields { get; set; } = "*";
        }
    }
    public class Clientversion
    {
        public int major { get; set; } = 2;
        public int minor { get; set; } = 8;
        public int patch { get; set; } = 1;
    }
    
}

namespace FenixLauncher.Functions
{
    public class SysConfiguration
    {
        public string connection_string { get; set; }
        public string jwt_key { get; set; }
        public string jwt_issuer { get; set; }
        public string jwt_audience { get; set; }
        public string session_name { get; set; }
        public SysConfiguration()
        {
            IConfigurationRoot datos = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            connection_string = datos.GetSection("ConnectionStrings")["DefaultConnection"];
            jwt_key = datos.GetSection("Jwt")["Key"];
            jwt_issuer = datos.GetSection("Jwt")["Issuer"];
            jwt_audience = datos.GetSection("Jwt")["Audience"];
            session_name = datos.GetSection("Variables")["SessionName"];
        }
    }
}

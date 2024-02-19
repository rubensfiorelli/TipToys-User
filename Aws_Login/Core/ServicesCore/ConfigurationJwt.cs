namespace Aws_Login.Core.ServicesCore;

public static class ConfigurationJwt
{
    public static string JwtPrivateKey = "cHJpdmF0ZS1rZXktdGVzdCBqd3QgYXBsaWNhY8OnYW8gbWljcm9zZXJ2aWNlcyBhd3M=";
    public static string ApiKeyName = "api_key";
    public static string ApiKey = "teste_api";
    public static SmtpConfiguration Smtp = new SmtpConfiguration();


    public class SmtpConfiguration
    {
        public string Host { get; set; }
        public int Port { get; set; } = 465;
        public string UserName { get; set; }
        public string PassWord { get; set; }
    }
}

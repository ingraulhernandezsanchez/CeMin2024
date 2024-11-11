namespace CeMin2024.Server.Options
{
    public class CorsOptions
    {
        public string[] AllowedOrigins { get; set; } = Array.Empty<string>();
        public bool AllowCredentials { get; set; } = true;
        public string[] ExposedHeaders { get; set; } = new[] { "Content-Disposition" };
    }
}
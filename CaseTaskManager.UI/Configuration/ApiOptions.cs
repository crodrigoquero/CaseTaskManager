// /Configuration/ApiOptions.cs
namespace CaseTaskManager.UI.Configuration
{
    public sealed class ApiOptions
    {
        public const string SectionName = "Api";
        public string BaseUrl { get; set; } = "https://localhost:7156/api/";
        // if we later add auth headers, timeouts, etc., we have to add them here
    }
}

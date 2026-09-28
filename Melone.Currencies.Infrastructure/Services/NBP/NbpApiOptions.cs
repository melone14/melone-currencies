namespace Melone.Currencies.Infrastructure.Services.NBP;

public class NbpApiOptions
{
    public const string SectionName = "NbpApi";

    public string BaseAddress { get; set; } = string.Empty;
    public string TableBEndpoint { get; set; } = string.Empty;
}
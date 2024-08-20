namespace IRSI.TipsDistribution.Application.Options;

public class StoreSettings
{
    public const string SectionName = "StoreOptions";

    public int StoreId { get; set; }
    public List<string> Filenames { get; set; } = [];
    public string Token { get; set; } = string.Empty;
}
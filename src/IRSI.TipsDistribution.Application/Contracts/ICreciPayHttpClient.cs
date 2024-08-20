namespace IRSI.TipsDistribution.Application.Contracts;

public interface ICreciPayHttpClient
{
    Task UploadRecurring(int storeId, string token, MemoryStream stream);
    Task UploadFinal(int storeId, string token, MemoryStream stream);
}
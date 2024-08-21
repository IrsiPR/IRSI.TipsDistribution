namespace IRSI.TipsDistribution.Application.Contracts;

public interface ICreciPayHttpClient
{
    Task<HttpResponseMessage> UploadRecurring(int storeId, string token, MemoryStream stream);
    Task<HttpResponseMessage> UploadFinal(int storeId, string token, MemoryStream stream);
}
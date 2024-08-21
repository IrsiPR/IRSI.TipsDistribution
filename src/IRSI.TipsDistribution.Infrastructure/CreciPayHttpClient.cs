using IRSI.TipsDistribution.Application.Contracts;

namespace IRSI.TipsDistribution.Infrastructure;

public class CreciPayHttpClient : ICreciPayHttpClient
{
    private readonly HttpClient _httpClient;

    public CreciPayHttpClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task UploadRecurring(int storeId, string token, MemoryStream stream)
    {
        var content = new MultipartFormDataContent();
        content.Add(new StringContent(token), "token");
        content.Add(new StringContent(storeId.ToString()), "storeId");

        var fileContent = new StreamContent(stream);
        fileContent.Headers.ContentType = new("application/octet-stream");
        content.Add(fileContent, "zip", "recurring.zip");

        await _httpClient.PostAsync("/upload-tips-recurring", content);
    }

    public async Task UploadFinal(int storeId, string token, MemoryStream stream)
    {
        var content = new MultipartFormDataContent();
        content.Add(new StringContent(token), "token");
        content.Add(new StringContent(storeId.ToString()), "storeId");

        var fileContent = new StreamContent(stream);
        fileContent.Headers.ContentType = new("application/octet-stream");
        content.Add(fileContent, "zip", "final.zip");

        await _httpClient.PostAsync("/upload-tips-daily", content);
    }
}
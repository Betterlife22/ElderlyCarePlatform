using ElderlyCareApp.Models;

namespace ChatFPT.Service.Interfaces
{
    public interface IAIService
    {
        Task<List<float>> GetEmbeddingAsync(string text);
        Task<bool> UploadDataToPineconeAsync(UploadDataModel model);
        Task<string> QueryDataAsync(string question, int userId);
    }
}

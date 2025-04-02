using ChatFPT.Service.Interfaces;
using ElderlyCareApp.Models;
using Microsoft.Extensions.Configuration;
using Pinecone;
using System.Text;
using System.Text.Json;

public class AIService : IAIService
{
    private readonly string _pineconeApiKey;
    private readonly string _openaiApiKey;
    private readonly string _indexName;
    private readonly string _model;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly HttpClient _httpClient;

    public AIService(IConfiguration configuration, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _pineconeApiKey = configuration["PineCone:PineconeApiKey"] ?? throw new Exception("PineconeApiKey Not Found");
        _openaiApiKey = configuration["OpenAI:ApiKey"] ?? throw new Exception("ApiKey Not Found");
        _indexName = configuration["PineCone:PineconeIndexName"] ?? throw new Exception("PineconeIndexName Not Found");
        _model = configuration["OpenAI:OpenAIModel"] ?? throw new Exception("OpenAIModel Not Found");
        _httpClient = new HttpClient();
        _httpClient.DefaultRequestHeaders.Add("Api-Key", _pineconeApiKey);
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<List<float>> GetEmbeddingAsync(string text)
    {
        var requestBody = new
        {
            model = "text-embedding-3-small",
            input = text
        };

        var content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");
        _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_openaiApiKey}");

        HttpResponseMessage response = await _httpClient.PostAsync("https://api.openai.com/v1/embeddings", content);

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("");
        }

        var responseJson = await response.Content.ReadAsStringAsync();
        using JsonDocument doc = JsonDocument.Parse(responseJson);
        var embedding = doc.RootElement.GetProperty("data")[0].GetProperty("embedding");

        List<float> vector = new List<float>();
        foreach (var value in embedding.EnumerateArray())
        {
            vector.Add(value.GetSingle());
        }

        return vector;
    }

    public async Task<string> QueryDataAsync(string question, int userId)
    {
        using var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_openaiApiKey}");

        string query = await QueryPinecone(question);

        //var userRepo = _unitOfWork.GetRepository<User>();

        //var user = await userRepo.SearchAsync(u => u.Id == userId);

        var requestBody = new
        {
            model = _model,
            messages = new[]
            {
                new {
                    role = "system",
                    content = "You are a virtual assistant specialized in supporting patients at MediaLab Hospital. " +
                             "Provide accurate and helpful responses based on the following information:\n\n" +
                             query + "\n\n" +
                             "You can assist with:\n" +
                             "- Exercise recommendations for various health conditions and recovery stages\n" +
                             "- Medication information and reminders\n" +
                             "- Nutrition advice and healthy eating guidelines\n" +
                             "- General health information and wellness tips\n" +
                             "- Hospital services and visiting hours\n" +
                             "- You can give youtube link for advice user\n\n" +
                             "Important notes:\n" +
                             "1. Always remind patients that your advice doesn't replace professional medical consultation\n" +
                             "2. For urgent medical issues, advise contacting emergency services immediately\n" +
                             "3. For appointment bookings or specific medical inquiries, suggest contacting " +
                             "the hospital directly at (123) 456-7890 or info@medialabhospital.com"
                },
                new {
        role = "user",
        content = question
                }
            }
        };

        var content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");

        var response = await httpClient.PostAsync("https://api.openai.com/v1/chat/completions", content);

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("AI Fail");
        }

        var jsonResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(await response.Content.ReadAsStringAsync());

        var answer = jsonResponse!.choices[0].message.content.ToString();

        return answer;
    }

    public async Task<bool> UploadDataToPineconeAsync(UploadDataModel model)
    {

        var document = model.Description + " " + model.Link;
        var client = new PineconeClient(_pineconeApiKey);
        var index = client.Index(_indexName);

        if (index == null)
        {
            throw new Exception("AI Fail");
        }


        List<Vector> vectors = new List<Vector>();


            var embedding = await GetEmbeddingAsync(document);
            if (embedding == null)
            {
                throw new Exception("AI Fail");
            }

            vectors.Add(new Vector
            {
                Id = Guid.NewGuid().ToString(),
                Values = embedding.ToArray(),
                Metadata = new Metadata { { "text", document }}
            });
        

        var upsertRequest = new UpsertRequest
        {
            Vectors = vectors
        };

        await index.UpsertAsync(upsertRequest);

        return true;
    }
    public async Task<string> QueryPinecone(string query)
    {
        var client = new PineconeClient(_pineconeApiKey);
        var index = client.Index(_indexName);

        if (index == null)
        {
            throw new Exception("AI Fail");
        }

        var embedding = await GetEmbeddingAsync(query);
        if (embedding == null)
        {
            throw new Exception("AI Fail");
        }

        var queryResponse = await index.QueryAsync(new QueryRequest
        {
            Vector = embedding.ToArray(),
            TopK = 1,
            IncludeMetadata = true
        }
        ) ?? throw new Exception("AI Fail");

        if (queryResponse.Matches!.Count() == 0)
            throw new Exception("AI Fail");

        return string.Join("\n", queryResponse.Matches!.Select(m => m.Metadata["text"]));
    }


}


using Azure;
using Azure.AI.TextAnalytics;

namespace real_time_chat.TextAnalysis;

public class TextAnalysis
{
    private static readonly AzureKeyCredential Credential = new AzureKeyCredential("3a0fe0740e7441139eade13bab2f7905");
    private static readonly Uri Endpoint = new Uri("https://azureai-textanalysis.cognitiveservices.azure.com/");
    private readonly TextAnalyticsClient _client;

    public TextAnalysis()
    {
        this._client = new TextAnalyticsClient(Endpoint, Credential);
    }

    public string SentimentAnalysis(string text)
    {
        DocumentSentiment documentSentiment = _client.AnalyzeSentiment(text);

        return documentSentiment.Sentiment.ToString();

    }
    
    
}
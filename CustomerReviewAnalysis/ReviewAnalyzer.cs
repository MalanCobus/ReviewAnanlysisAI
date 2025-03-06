using Azure.AI.TextAnalytics;
using Azure;
using Microsoft.Extensions.Configuration;
using Azure.AI.Vision.ImageAnalysis;
using Azure.AI.FormRecognizer.DocumentAnalysis;
using System.Text;
namespace CustomerReviewAnalysis;
public class ReviewAnalyzer
{
    private string endpoint;
    private string key;
    public ReviewAnalyzer(IConfiguration config)
    {
        endpoint = config["AzureAIServices:Endpoint"]!;
        key = config["AzureAIServices:Key"]!;
    }
    public async Task<DocumentSentiment> AnalyzeReviewTextAsync(string reviewText)
    {
        var client = new TextAnalyticsClient(new Uri(endpoint), new AzureKeyCredential(key));
        var response = await client.AnalyzeSentimentAsync(reviewText, options: new AnalyzeSentimentOptions() { IncludeOpinionMining = true });
        DocumentSentiment documentSentiment = response.Value;
        return documentSentiment;
    }

    public async Task<DocumentSentiment> AnalyzeReviewFromImage(string imagePath)
    {
        var client = new ImageAnalysisClient(new Uri(endpoint), new AzureKeyCredential(key));
        using var stream = File.OpenRead(imagePath);
        BinaryData fileBinary = await BinaryData.FromStreamAsync(stream);
        ImageAnalysisResult imageAnalysisResult = await client.AnalyzeAsync(fileBinary, VisualFeatures.Read);
        var extractedText = new List<string>();
        foreach (var block in imageAnalysisResult.Read.Blocks)
        {
            foreach (var line in block.Lines)
            {
                extractedText.Add(line.Text);
            }
        }
        var reviewText = string.Join("\n", extractedText);
        return await AnalyzeReviewTextAsync(reviewText);
    }

    public async Task<DocumentSentiment> AnalyzeReviewFromPdf(string filePath)
    {
        var client = new DocumentAnalysisClient(new Uri(endpoint), new AzureKeyCredential(key));
        using var stream = File.OpenRead(filePath);
        var result = await client.AnalyzeDocumentAsync(WaitUntil.Completed, "prebuilt-read", stream);
        var extractedText = new StringBuilder();
        foreach (var page in result.Value.Pages)
        {
            foreach (var line in page.Lines)
            {
                extractedText.AppendLine(line.Content);
            }
        }
        return await AnalyzeReviewTextAsync(extractedText.ToString());
    }

}
using Azure.AI.TextAnalytics;
using CustomerReviewAnalysis;
using Microsoft.Extensions.Configuration;

IConfigurationRoot config = new ConfigurationBuilder()
            .AddUserSecrets<Program>()
            .Build();
ReviewAnalyzer reviewAnalyzer = new ReviewAnalyzer(config);

Console.WriteLine("Select the type of review analysis to run:");
Console.WriteLine("1. Analyze text review");
Console.WriteLine("2. Analyze review from image");
Console.WriteLine("3. Analyze review from PDF");
Console.Write("Enter your choice (1/2/3): ");
string choice = Console.ReadLine();
string resourcesDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources");

switch (choice)
{
    case "1":
        string textReview = @"
        I recently purchased a refurbished MacBook from GadgetSphere and I'm beyond happy with my purchase! 
        The laptop arrived in pristine condition, almost like new. Not only was the price unbeatable, 
        but their customer service was also incredibly helpful throughout the process. 
        Highly recommend GadgetSphere for anyone looking for great deals on electronics!
        ";
        DocumentSentiment textReviewAnalysis = await reviewAnalyzer.AnalyzeReviewTextAsync(textReview);
        PrintSentiment(textReviewAnalysis);
        break;

    case "2":
        string imagePath = Path.Combine(resourcesDir, "handwritten-review.jpeg");

        DocumentSentiment imageReviewAnalysis = await reviewAnalyzer.AnalyzeReviewFromImage(imagePath);
        PrintSentiment(imageReviewAnalysis);
        break;

    case "3":
        string pdfPath = Path.Combine(resourcesDir, "review-mail.pdf");

        DocumentSentiment pdfReviewAnalysis = await reviewAnalyzer.AnalyzeReviewFromPdf(pdfPath);
        PrintSentiment(pdfReviewAnalysis);
        break;

    default:
        Console.WriteLine("Invalid choice. Please run the program again and select a valid option.");
        break;
}

static void PrintSentiment(DocumentSentiment result)
{
    Console.WriteLine($"Overall sentiment: {result.Sentiment.ToString()}");
    foreach (SentenceSentiment sentence in result.Sentences)
    {
        Console.WriteLine($"\tSENTENCE: {sentence.Text}");
        Console.WriteLine($"\tSENTIMENT: {sentence.Sentiment.ToString()}");
        foreach (SentenceOpinion sentenceOpinion in sentence.Opinions)
        {
            Console.WriteLine($"\t\tTARGET: {sentenceOpinion.Target.Text}");
            foreach (var assessment in sentenceOpinion.Assessments)
            {
                Console.WriteLine($"\t\t\tOPINION: {assessment.Text}");
                Console.WriteLine($"\t\t\tSENTIMENT: {assessment.Sentiment.ToString()}");
            }
        }
    }
}

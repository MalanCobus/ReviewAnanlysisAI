**Customer Review Analysis**

Purpose
The Customer Review Analysis application is designed to analyze customer reviews from various sources such as text, images, and PDFs. It uses Azure AI services to extract and analyze the sentiment of the reviews, providing insights into customer opinions and feedback.


**Features**

•	Analyze text reviews for sentiment and opinion mining.

•	Extract and analyze text from handwritten reviews in images.

•	Extract and analyze text from reviews in PDF documents.



**Technologies Used**

•	.NET 9

•	C# 13.0

•	Azure AI Form Recognizer

•	Azure AI Text Analytics

•	Azure AI Vision Image Analysis

•	Microsoft Cognitive Services Speech

•	Microsoft Extensions Configuration

•	Microsoft Extensions Configuration UserSecrets



**Setup Instructions**

1.	Clone the Repository:
```
   git clone <repository-url>
   cd CustomerReviewAnalysis
```
2.	Install .NET 9 SDK: Ensure you have the .NET 9 SDK installed. You can download it from the official .NET website.
3.	Set Up Azure AI Services:
   
       •	Create an Azure account if you don't have one.

       •	Set up the following Azure AI services:

       •	Azure AI Form Recognizer

       •	Azure AI Text Analytics

       •	Azure AI Vision Image Analysis

       •	Obtain the endpoint and key for each service.


4.	Configure User Secrets:
        •	In the project directory, run the following command to initialize user secrets:
```
     dotnet user-secrets init
```

   •	Add your Azure AI service credentials to the user secrets:
        
```
dotnet user-secrets set "AzureAIServices:Endpoint" "<your-endpoint>"
dotnet user-secrets set "AzureAIServices:Key" "<your-key>"
```

5.	Build and Run the Application:
•	Build the project:
```
dotnet run
```
6.	Resources Directory: Ensure that the Resources directory contains the following files:
   
•	handwritten-review.jpeg

•	review-mail.pdf


Usage

1.	When you run the application, you will be prompted to select the type of review analysis to run:
   
       •	'1' for analyzing a text review.

    •	'2' for analyzing a review from an image.

    •	'3' for analyzing a review from a PDF.

3.	The application will process the selected review type and output the sentiment analysis results to the console.
   
Notes

•	Ensure that the Azure AI service credentials are correctly configured in the user secrets.

•	The application requires internet access to connect to Azure AI services.

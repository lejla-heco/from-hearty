# FromHearty
Cardiovascular diseases (CVDs) are slowly taking the leading position in the list of diseases that cause the highest mortality rate. By using AI technologies FromHearty is eager to optimize the process of recognition symptoms and diagnosing CVD in patients. Our solution starts from simple but yet effective so-called quick checks during which a trained AI model collects basic data on patient health in order to recognize the signs of CVD as early as possible. We strive to partner with clinics where CVD tests are conducted in order to further innovate such facilities by providing an AI model that will support the process of recognizing symptoms of CVD. From the very beginning of the project, we defined the thesis that this AI solution should in no way replace experts in the field of CVD, but on the contrary, represent their support and facilitate daily activities, all with the goal of more efficient diagnostics. Therefor we aim to implement our solution in such a way that the assessment and final diagnosis which the patient receives is done by an expert. We hope that with continuous feedback our AI solution will vastly improve in terms of accuracy, specificity and sensitivity.

### Team
Our team consists of thoughtful individuals each being employed in the IT industry with a track record of creating, maintaining and managing software products. With the combined experience of over a decade in the industry, we feel confident to deliver and be able to maintain high levels of QA, manage complex DevOps processes and long-term deliver more comprehensive functional features while growing a business.

Each of us has a formal as well as informal education and is currently in the last year of our master's degree at the Faculty of Information Technology in Mostar. Regarding that, we are supported by our professor who is at the same time our mentor and a Microsoft MVP with dedicated experience in the software development and IT industry. Together we are eager to reach our goal in not just shaping our practical knowledge but rather, with the help of innovative technologies such as AI & ML, developing meaningful real-world solutions by creating products from which people all over the world can benefit from.

## Commands
- Run: `dotnet run --project WebAPI`
- Swagger URL: `http://localhost:5000/swagger/index.html`
- Add package: `dotnet add package RhoMicro.MicroDI --version 4.3.1`
- ML.NET Example 1: `mlnet classification --dataset "yelp_labelled.txt" --label-col 1 --has-header false --train-time 10`
- ML.NET Example 2: `mlnet classification --dataset "wikipedia-detox-250-line-data.tsv" --label-col "Sentiment" --ignore-cols "LoggedIn" --has-header true --train-time 10`

## Changelog
1. Default .NET project.
- Added .sln `dotnet new sln --name FromHeart`
- Created WebAPI project: `dotnet new webapi -n WebAPI`
- Adding project to .sln: `dotnet sln add ./WebAPI/WebAPI.csproj`
- Run SampleClassification: `mlnet classification --dataset "yelp_labelled.txt" --label-col 1 --has-header false --train-time 10`

2. ML.NET
- Command to create SampleClassification `mlnet classification --dataset "yelp_labelled.txt" --label-col 1 --has-header false --train-time 10`
- Adding project to .sln: `dotnet sln add ./SampleClassification/SampleClassification.csproj`
- cd WebAPI
- 
using TrainingTypeClient;
using TrainingTypeClient.Models;
using TrainingTypeSharedModels.Models;

var trainingRecords = new[]
{
    new { Id = 1, Name = "Freddie Mercury", StdCode = 32, TrainingType="ShorterApps" },
    new { Id = 2, Name = "Michael Jackson", StdCode = 25, TrainingType="FoundationApps" },
    new { Id = 3, Name = "Elvis Presley", StdCode = 35, TrainingType="RegularApps" }
};

var _configurationClient = new ConfigurationServiceClient();

Console.WriteLine("Provide Feedback Domain -> question descriptions are training type dependant.");
foreach (var tr in trainingRecords)
{
    var providerFeedbackConfig = 
        await _configurationClient.GetConfigurationAsync<ProvideFeedbackDomainConfig>(Domains.ProvideFeedback, tr.TrainingType);

    Console.WriteLine($"\n** Learner name: {tr.Name} on standard: {tr.StdCode} is on training type: {tr.TrainingType}");
    foreach(var question in providerFeedbackConfig!.FeedbackQuestionNames)
        Console.WriteLine($"\t{question}");
}

Console.WriteLine("\n\nVacancy Domain -> min duration and whether to ask for quals are training type dependant.");
foreach (var tr in trainingRecords)
{
    var recruitConfig = 
        await _configurationClient.GetConfigurationAsync<RecruitDomainConfig>(Domains.Recruit, tr.TrainingType);

    Console.WriteLine($"\n** Learner name: {tr.Name} on standard: {tr.StdCode} is on training type: {tr.TrainingType}");
    Console.WriteLine($"\tAsk for essential quals?: {recruitConfig!.AskForEssentialQuals}, Min months in training: {recruitConfig!.MinLearningDurationMonths}");
}

Console.WriteLine("\n Completed \n");
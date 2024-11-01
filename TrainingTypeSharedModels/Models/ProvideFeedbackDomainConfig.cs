using System.Text.Json.Serialization;

namespace TrainingTypeSharedModels.Models;

public class ProvideFeedbackDomainConfig
{
    public Dictionary<string,string> FeedbackQuestionNames { get; private set; }

    [JsonConstructor]
    public ProvideFeedbackDomainConfig(Dictionary<string,string> feedbackQuestionNames)
    {
        FeedbackQuestionNames = feedbackQuestionNames;
    }
}


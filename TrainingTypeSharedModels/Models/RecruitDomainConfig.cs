using System.Text.Json.Serialization;

namespace TrainingTypeSharedModels.Models;

public class RecruitDomainConfig
{
    public bool AskForEssentialQuals { get; private set; }
    public int MinLearningDurationMonths { get; private set; }

    [JsonConstructor]
    public RecruitDomainConfig(bool askForEssentialQuals, int minLearningDurationMonths)
    {
        AskForEssentialQuals = askForEssentialQuals;
        MinLearningDurationMonths = minLearningDurationMonths;
    }
}


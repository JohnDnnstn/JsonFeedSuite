using GenericJsonSuite.EtlaToolbelt.Forms;

namespace GenericJsonWizard.BackingData;

public class RolesData : IWizardData
{
    public string? InitialisedStagingOwner { get; set; } = null;
    public string? InitialisedLoaderRole { get; set; } = null;
    public string? InitialisedTargetSchemaOwner { get; set; } = null;
    public string? InitialisedDbPowerUsers { get; set; } = null;
    public string? InitialisedTargetSchemaUsers { get; set; } = null;

    internal string StagingOwner
    {
        get { return InitialisedStagingOwner ?? "staging_owner"; }
        set { InitialisedStagingOwner = value; }
    }

    public string LoaderRole
    {
        get { return InitialisedLoaderRole ?? $"{ChosenData.FeedDetails.DbName}_loader"; }
        set { InitialisedLoaderRole = value; }
    }

    public string TargetSchemaOwner
    {
        get { return InitialisedTargetSchemaOwner ?? GetTargetSchemaOwner(); }
        set { InitialisedTargetSchemaOwner = value; }
    }

    public string DbPowerUsers
    {
        get { return InitialisedDbPowerUsers ?? $"{ChosenData.FeedDetails.DbName}_dbas"; }
        set { InitialisedDbPowerUsers = value; }
    }

    public string TargetSchemaUsers
    {
        get { return InitialisedTargetSchemaUsers ?? GetTargetSchemaUsers(); }
        set { InitialisedTargetSchemaUsers = value; }
    }

    private static string GetTargetSchemaOwner()
    {
        var db = ChosenData.FeedDetails.DbName.ToLower();
        return db switch
        {
            "epic" => "epicadmin",
            "vsiu_prototype" => "vsiu_owner",
            _ => $"{db}_owner",
        };
    }

    private static string GetTargetSchemaUsers()
    {
        var db = ChosenData.FeedDetails.DbName.ToLower();
        var schema = ChosenData.FeedDetails.SchemaName.ToLower();
        return db switch
        {
            "epic" => $"epic{schema}",
            "vsiu_prototype" => $"vsiu_{schema}",
            _ => $"{db}_{schema}_user",
        };
    }
}

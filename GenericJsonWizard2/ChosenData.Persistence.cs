using GenericJsonSuite;
using System.Text.Json;

namespace GenericJsonWizard;

public static partial class ChosenData
{
    private static readonly JsonSerializerOptions _JsonSerialisationOptions = new() { WriteIndented = true };

    //private const string _FeedName = "persistence";
    //private const string _Env = "test";
    private const string _Company = "SRUC";
    private const string _App = "GenericJsonWizard2";

    public static string ToJson()
    {
        return JsonSerializer.Serialize(_State, _JsonSerialisationOptions);
    }

    public static void LoadFeedList(string? env = null)
    {
        env ??= WelcomeData.Env;
        var fileName = GetFilepath(null, env);
        var dir = Path.GetDirectoryName(fileName);
        if (dir.IsBlack() && !Directory.Exists(dir)) { Directory.CreateDirectory(dir); }
        if (!File.Exists(fileName)) { SaveFeedList(saveEmptyList: true); }

        var jsonString = File.ReadAllText(fileName);
        FeedList = JsonSerializer.Deserialize<List<string>>(jsonString) ?? [];
        var temp = new List<string>(FeedList);
        foreach (var feed in temp)
        {
            var feedFilename = GetFilepath(feed, env);
            if (!File.Exists(feedFilename))
            {
                FeedList.Remove(feed);
            }
        }
    }

    public static void SaveFeedList(bool saveEmptyList = false, string? env = null)
    {
        var feedList = FeedList;
        if (saveEmptyList) { feedList = []; }
        env ??= WelcomeData.Env;
        var fileName = GetFilepath(null, env);
        var dir = Path.GetDirectoryName(fileName);
        if (dir.IsBlack() && !Directory.Exists(dir)) { Directory.CreateDirectory(dir); }

        var jsonString = JsonSerializer.Serialize(feedList, _JsonSerialisationOptions);
        File.WriteAllText(fileName, jsonString);
    }

    public static void LoadFeedData(string? feedName = null, string? env = null)
    {
        feedName ??= WelcomeData.FeedName;
        env ??= WelcomeData.Env;
        var fileName = GetFilepath(feedName, env);
        var jsonString = File.ReadAllText(fileName);
        _State = JsonSerializer.Deserialize<State>(jsonString) ?? new();
    }


    public static void SaveFeedData(string? feedName = null, string? env = null)
    {
        feedName ??= FeedDetails.FeedFullName;
        env ??= WelcomeData.Env;
        var fileName = GetFilepath(feedName, env);
        var jsonString = ToJson();
        File.WriteAllText(fileName, jsonString);
    }
    private static string GetFilepath(string? feedBaseName = null, string? env = null)
    {
        feedBaseName ??= "AllKnownFeeds";
        if (env == null || env == "TEST") { env = ""; }
        var dir = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            _Company,
            _App,
            env);
        if (!Directory.Exists(dir)) { Directory.CreateDirectory(dir); }

        return Path.Combine(dir, feedBaseName + ".json");
    }
}

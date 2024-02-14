using System;
using System.IO;
using System.Reflection;
using System.Text.Json;

namespace TACSV.Config;

public class ConfigManager
{
    public static ApplicationOptions Options;

    public static void Initialise()
    {
        string appDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "TACSV");
        string configPath = Path.Combine(appDataPath, "TACSVConfig.json");
        if (!File.Exists(configPath))
        {
            File.Create(configPath);
            File.WriteAllText(configPath, GetBaseConfig());
        }

        string configData = File.ReadAllText(configPath);
        Options = JsonSerializer.Deserialize<ApplicationOptions>(configData) ?? throw new Exception("Config Invalid");
    }

    private static string GetBaseConfig()
    {
        var resourceName = "TACSV.Resources.BaseConfig.json";
        var assembly = Assembly.GetExecutingAssembly();
        using Stream? stream = assembly.GetManifestResourceStream(resourceName);
        if (stream != null)
        {
            using var reader = new StreamReader(stream);
            return reader.ReadToEnd();
        }

        TACSVConsole.Log("Cannot find config file");
        return "{}";
    }
}
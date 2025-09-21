using System;
using System.Collections.Generic;
using System.Linq;
using BepInEx.Configuration;
using GorillaConfigurationManager.Patcher;
using HarmonyLib;

namespace GorillaConfigurationManager;

public class ConfigManager
{
    private static readonly Lazy<ConfigManager> instance = new Lazy<ConfigManager>(() => new ConfigManager());
    public static ConfigManager Instance => instance.Value;

    public ConfigManager()
    {

    }

    public bool SetValue(ConfigEntryBase entry, string newValue)
    {
        object converted = entry.DefaultValue;
        try
        {
            converted = Convert.ChangeType(newValue, entry.SettingType);
        }
        catch
        {
            return false;
        }
        entry.BoxedValue = converted;
        return true;
    }

    public ConfigFileData GetData(ConfigEntryBase entry)
    {
        return GetConfigs().FirstOrDefault(x => x.Config == entry.ConfigFile);
    }

    public IEnumerable<ConfigFileData> GetConfigs()
    {
        var datas = new List<ConfigFileData>()
        {
            new(null, BepInEx.Configuration.ConfigFile.CoreConfig)
        };
        datas.AddRange(Patcher.ConfigFiles.Configs);
        var configs = datas.Where(data => data.Config?.Count != 0);
        return configs;
    }
}

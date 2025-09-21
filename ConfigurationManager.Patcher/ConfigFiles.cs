using BepInEx;
using BepInEx.Configuration;

namespace GorillaConfigurationManager.Patcher;

public static class ConfigFiles
{
    public static List< ConfigFileData> Configs = new List<ConfigFileData>();
}

public record class ConfigFileData(BepInPlugin? Owner, ConfigFile Config);

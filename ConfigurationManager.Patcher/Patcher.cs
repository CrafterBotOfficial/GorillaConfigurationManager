using System.Reflection;
using System.Text;
using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using Mono.Cecil;

namespace GorillaConfigurationManager.Patcher;

internal static class Patcher
{
#pragma warning disable CS8618
    private static ManualLogSource _logger;
#pragma warning restore CS8618

    private static string[] _targetDLLs = new string[0];
    public static IEnumerable<string> TargetDLLs => _targetDLLs;

    public static void Initialize()
    {
        _logger = Logger.CreateLogSource("ConfigFile Patcher");
        _logger.LogInfo("Applying early patches...");

        new Harmony("config_file_patcher").PatchAll(typeof(Patcher));
    }

    [HarmonyPatch(typeof(ConfigFile), argumentTypes: [typeof(string), typeof(bool), typeof(BepInPlugin)], methodType: MethodType.Constructor)]
    [HarmonyPostfix]
    private static void ConfigFile_Constructor_Patch(BepInEx.Configuration.ConfigFile __instance, string configPath, bool saveOnInit, BepInEx.BepInPlugin ownerMetadata)
    {
        var info = new ConfigFileData(ownerMetadata, __instance);
        ConfigFiles.Configs.Add(info);
    }

    [HarmonyPatch(typeof(ConfigFile), argumentTypes: [typeof(string), typeof(bool)], methodType: MethodType.Constructor)]
    [HarmonyPostfix]
    private static void ConfigFile_Untracked_Constructor_Patch(BepInEx.Configuration.ConfigFile __instance, string configPath, bool saveOnInit)
    {
        _logger.LogInfo("New untracked config file. Tip: Use the provided config file in the baseunityplugin instead of making your own manually -_-");
        var info = new ConfigFileData(null, __instance);
        ConfigFiles.Configs.Add(info);
    }

#if DEBUG
    private static void DumpMethods(Mono.Cecil.TypeDefinition type)
    {
        foreach (var method in type.Methods)
        {
            var line = new System.Text.StringBuilder();
            foreach (var param in method.Parameters) line.Append(param.Name + ", ");
            _logger.LogInfo(method.Name + " | " + line.ToString());
        }
    }
#endif

    public static void Patch(AssemblyDefinition _) { }
}

class ILCodeGenerator
{
    public ILCodeGenerator(string configPath, bool saveOnInit, string ownerMetadata)
    {
        MyMethod(ownerMetadata);
    }

    public void MyMethod(string arg1)
    {
        System.Console.WriteLine(arg1);
    }
}

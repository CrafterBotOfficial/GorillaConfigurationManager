#if DEBUG

using System.IO;
using BepInEx;
using BepInEx.Configuration;

namespace GorillaConfigurationManager.Tests;

public static class DebugConfig
{
    public static void Test()
    {
        var config = new ConfigFile(Path.Combine(Paths.ConfigPath, "HelloWorld.cfg"), true);
        const int section_count = 10;
        const int debug_value_count = 50;
        for (int i = 0; i < section_count; i++)
        {
            for (int y = 0; y < debug_value_count; y++)
            {
                config.Bind<string>(i.ToString(), y.ToString(), "Hi cruel world", "A value that you hopefully can edit and stuff.");
            }
        }
    }
}

#endif

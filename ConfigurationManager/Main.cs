using BepInEx;
using BepInEx.Logging;

namespace GorillaConfigurationManager;

[BepInPlugin("crafterbot.configurationmanager", "Configuration Manager", "2.0.0")]
public class Main : BaseUnityPlugin
{
    private static Main instance;

    private void Awake()
    {
        instance = this;
    }

    public static void Log(object message, LogLevel level = LogLevel.Info)
    {
        instance?.Logger.Log(level, message);
    }
}

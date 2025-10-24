using System;
using BepInEx.Configuration;
using ComputerInterface;
using ComputerInterface.Enumerations;

namespace GorillaConfigurationManager.ValueEditHandler;

public class BoolHandler : IEditHandler
{
    public void OnManipulate(ref string text, EKeyboardKey key)
    {
        bool textBoolean = Convert.ToBoolean(text);
        text = (!textBoolean).ToString();
    }

    public void OnSet(string text, ConfigEntryBase entry) {
        entry.BoxedValue = Convert.ToBoolean(text);
    }

    public void Reset(string defaultValue)
    {
    }

    public string GetTooltip()
    {
        return "(true/false)";
    }
}

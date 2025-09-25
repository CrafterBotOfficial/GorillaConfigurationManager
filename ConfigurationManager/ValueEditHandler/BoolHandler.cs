using System;
using ComputerInterface;

namespace GorillaConfigurationManager.ValueEditHandler;

public class BoolHandler : IEditHandler
{
    public void OnManipulate(ref string text, EKeyboardKey key)
    {
        bool textBoolean = Convert.ToBoolean(text);
        text = (!textBoolean).ToString();
    }

    public void Reset(string defaultValue)
    {

    }

    public string GetTooltip()
    {
        return "(true/false)";
    }
}

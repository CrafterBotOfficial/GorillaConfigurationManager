using System;
using ComputerInterface;

namespace GorillaConfigurationManager.ValueEditHandler;

public class BoolHandler : IEditHandler
{
    public string GetHeader() => "Boolean (true/false): ";

    public void OnManipulate(ref string text, EKeyboardKey key)
    {
        bool textBoolean = Convert.ToBoolean(text);
        text = (!textBoolean).ToString();
    }
}

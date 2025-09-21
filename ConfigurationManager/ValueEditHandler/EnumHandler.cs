using System;
using ComputerInterface;

namespace GorillaConfigurationManager.ValueEditHandler;

public class EnumHandler : IEditHandler
{
    public string[] Options;
    private int selectIndex;

    public EnumHandler(string[] names)
    {
        Options = names;
    }

    public void OnManipulate(ref string text, EKeyboardKey key)
    {
        selectIndex++;
        text = Options[selectIndex % Options.Length];
    }

    public string GetTooltip()
    {
        return string.Empty;
    }
}

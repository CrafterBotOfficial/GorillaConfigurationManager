using System;
using ComputerInterface;

namespace GorillaConfigurationManager.ValueEditHandler;

public class EnumHandler : IEditHandler
{
    public string GetHeader() => "Select an option";

    public string[] Options;
    private int selectIndex;

    public void OnManipulate(ref string text, EKeyboardKey key)
    {
        selectIndex++;
        text = Options[selectIndex % Options.Length];
    }
}

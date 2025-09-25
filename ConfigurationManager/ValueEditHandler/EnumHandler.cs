using System;
using System.Linq;
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

    public void Reset(string defaultValue)
    {
        selectIndex = Options.ToList().IndexOf(defaultValue);
        if (selectIndex == -1)
        {
            Main.Log("Couldn't find name in enum for default");
            selectIndex = 0;
        }
    }

    public string GetTooltip()
    {
        return string.Empty;
    }
}

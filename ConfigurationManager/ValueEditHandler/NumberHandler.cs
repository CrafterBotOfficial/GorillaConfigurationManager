using System;
using System.Linq;
using ComputerInterface;
using ComputerInterface.Extensions;
using ComputerInterface.ViewLib;

namespace GorillaConfigurationManager.ValueEditHandler;

public class NumberHandler : IEditHandler
{
    private UITextInputHandler textInputHandler;

    public NumberHandler(string defaultValue)
    {
        textInputHandler = new UITextInputHandler();
        textInputHandler.Text = defaultValue;
    }

    public void OnManipulate(ref string text, EKeyboardKey key)
    {
        if (key == EKeyboardKey.Space)
        {
            textInputHandler.Text += ".";
            text += ".";
            return;
        }

        if ((key.IsNumberKey() || key == EKeyboardKey.Delete) && textInputHandler.HandleKey(key))
        {
            text = textInputHandler.Text;
            return;
        }
    }

    public string GetTooltip()
    {
        return "Use space to make a decimal";
    }
}

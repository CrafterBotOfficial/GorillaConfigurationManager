using System;
using System.Linq;
using ComputerInterface;
using ComputerInterface.Extensions;
using ComputerInterface.ViewLib;

namespace GorillaConfigurationManager.ValueEditHandler;

public class NumberHandler : IEditHandler
{
    public string GetHeader() => "Enter a valid Number: ";

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

        if (key.IsNumberKey() || key == EKeyboardKey.Delete && textInputHandler.HandleKey(key))
        {
            text = textInputHandler.Text;
            return;
        }
    }
}

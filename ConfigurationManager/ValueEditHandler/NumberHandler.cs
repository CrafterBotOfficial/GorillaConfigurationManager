using System;
using System.Linq;
using BepInEx.Configuration;
using ComputerInterface;
using ComputerInterface.Enumerations;
using ComputerInterface.Extensions;
using ComputerInterface.Models.UI;

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

    public void OnSet(string text, ConfigEntryBase entry) {
        if (text.IsNullOrWhiteSpace()) {
            entry.BoxedValue = 0;
            return;
        }
        entry.BoxedValue = Convert.ChangeType(text, entry.SettingType);
    }

    public void Reset(string defaultValue)
    {
        textInputHandler.Text = defaultValue;
    }

    public string GetTooltip()
    {
        return "Use space to make a decimal";
    }
}

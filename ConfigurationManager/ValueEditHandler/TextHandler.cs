using System;
using System.Linq;
using BepInEx.Configuration;
using ComputerInterface;
using ComputerInterface.Enumerations;
using ComputerInterface.Extensions;
using ComputerInterface.Models.UI;

namespace GorillaConfigurationManager.ValueEditHandler;

public class TextHandler : IEditHandler
{
    private UITextInputHandler textInputHandler;

    public TextHandler(string text)
    {
        textInputHandler = new UITextInputHandler();
        textInputHandler.Text = text;
    }

    public void OnManipulate(ref string text, EKeyboardKey key)
    {
        if (textInputHandler.HandleKey(key))
        {
            text = textInputHandler.Text;
        }
    }

    public void OnSet(string text, ConfigEntryBase entry) {
        entry.BoxedValue = textInputHandler.Text;
    }

    public void Reset(string defaultValue)
    {
        textInputHandler.Text = defaultValue;
    }

    public string GetTooltip()
    {
        return string.Empty;
    }
}

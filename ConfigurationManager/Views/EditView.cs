using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BepInEx.Configuration;
using ComputerInterface;
using ComputerInterface.Extensions;
using ComputerInterface.ViewLib;
using GorillaConfigurationManager.ValueEditHandler;

namespace GorillaConfigurationManager.Views;

public class EditView : ComputerView
{
    private ConfigEntryBase entry;
    private IEditHandler editHandler;

    private string text;

    public override void OnShow(object[] args)
    {
        base.OnShow(null);

        if (args.FirstOrDefault() is not ConfigEntryBase entryBase)
        {
            ShowView<ErrorView>("Cannot show view with no entry");
            return;
        }

        entry = entryBase;
        text = entry.BoxedValue.ToString().ToLower();
        editHandler = GetEditHandlerForType(entry.SettingType);

        UpdateText();
    }

    private void UpdateText()
    {
        var stringBuilder = new StringBuilder();

        stringBuilder.BeginCenter();
        stringBuilder.Append(entry.Definition.Key);
        stringBuilder.EndAlign();

        stringBuilder.AppendLines(2);
        stringBuilder.Append(text);

        // footer
        const int footer_height = 4;
        stringBuilder.AppendLines(SCREEN_HEIGHT - footer_height);
        stringBuilder.AppendLine(new string('=', ComputerView.SCREEN_WIDTH));
        stringBuilder.AppendLine("Enter a " + entry.SettingType.Name);
        stringBuilder.AppendLine("Press enter to save");
        stringBuilder.AppendLine(editHandler.GetTooltip());
        SetText(stringBuilder);
    }

    public override void OnKeyPressed(EKeyboardKey key)
    {
        if (key == EKeyboardKey.Enter)
        {
            Main.Log("Setting value to " + text);
            ConfigManager.Instance.SetValue(entry, text);
            UpdateText();
            return;
        }

        if (key == EKeyboardKey.Option1)
        {
            entry.BoxedValue = entry.DefaultValue;
            text = entry.BoxedValue.ToString().ToLower();
            UpdateText();
            return;
        }

        if (key == EKeyboardKey.Back)
        {
            ShowView<ConfigView>(ConfigManager.Instance.GetData(entry));
            return;
        }

        editHandler.OnManipulate(ref text, key);
        UpdateText();
    }

    private IEditHandler GetEditHandlerForType(Type type) => type switch
    {
        _ when type.IsEnum => new EnumHandler(Enum.GetNames(entry.SettingType)),
        _ when type == typeof(bool) => new BoolHandler(),
        _ when type == typeof(int) || type == typeof(float) || type == typeof(double) || type == typeof(decimal) || type == typeof(long) || type == typeof(short) => new NumberHandler(text),
        _ => new TextHandler(text),
    };
}

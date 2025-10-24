using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BepInEx.Configuration;
using ComputerInterface;
using ComputerInterface.Enumerations;
using ComputerInterface.Extensions;
using ComputerInterface.Models;
using ComputerInterface.Views;
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
        var stringBuilder = new StringBuilder()
            .BeginCenter()
            .AppendLine(entry.Definition.Key)
            .EndAlign()
            .AppendLine("Press enter to save")
            .AppendLine("Press option1 to reset")
            .AppendLine(new string('=', ScreenWidth));

        stringBuilder.AppendLines(1);
        stringBuilder.AppendLine("Value: " + text);

        SetText(stringBuilder);
    }

    public override void OnKeyPressed(EKeyboardKey key)
    {
        if (key == EKeyboardKey.Enter)
        {
            Main.Log("Setting value to " + text);
            editHandler.OnSet(text, entry);
            UpdateText();
            return;
        }

        if (key == EKeyboardKey.Option1)
        {
            entry.BoxedValue = entry.DefaultValue;
            text = entry.BoxedValue.ToString().ToLower();
            editHandler.Reset(text);
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

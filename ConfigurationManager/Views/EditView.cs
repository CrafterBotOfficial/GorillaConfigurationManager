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
        stringBuilder.AppendLines(1);
        stringBuilder.Append(editHandler.GetHeader());
        stringBuilder.Append(text);
        stringBuilder.AppendLines(1);
        string acceptable = entry.Description.AcceptableValues != null
            ? "Accepted: " + entry.Description.AcceptableValues.ToDescriptionString()
            : $"Enter a {entry.SettingType.Name}";
        stringBuilder.AppendLine(acceptable);
        SetText(stringBuilder);
    }

    public override void OnKeyPressed(EKeyboardKey key)
    {
        if (key == EKeyboardKey.Enter)
        {
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
        _ when type == typeof(string) || type.IsEnum => new EnumHandler() { Options = Enum.GetNames(entry.SettingType) },
        _ when type == typeof(bool) => new BoolHandler(),
        _ when type == typeof(int) || type == typeof(float) || type == typeof(double) || type == typeof(decimal) || type == typeof(long) || type == typeof(short) => new NumberHandler(),
        _ => new TextHandler()
    };
}

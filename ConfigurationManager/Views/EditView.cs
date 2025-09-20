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
        text = Convert.ToString(entry.BoxedValue);

        UpdateText(); // TODO Add event callback
    }

    public void UpdateText()
    {
        Main.Log("Setting text for edit view");
        var stringBuilder = new StringBuilder();
        stringBuilder.BeginCenter();
        stringBuilder.Append(entry.Definition.Key);
        stringBuilder.EndAlign();
        stringBuilder.AppendLines(1);
        stringBuilder.Append(editHandler.GetHeader());
        stringBuilder.Append(text);
        stringBuilder.AppendLines(1);
        stringBuilder.AppendLine(entry.Description.AcceptableValues is not null ? "Accepted: " + entry.Description.AcceptableValues.ToDescriptionString() : "Enter a " + entry.SettingType.Name);
        SetText(stringBuilder);
    }

    public override void OnKeyPressed(EKeyboardKey key)
    {
        if (key == EKeyboardKey.Enter)
        {
            ConfigManager.Instance.SetValue(entry, text);
            return;
        }

        if (key == EKeyboardKey.Option1)
        {
            entry.BoxedValue = entry.DefaultValue;
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
}

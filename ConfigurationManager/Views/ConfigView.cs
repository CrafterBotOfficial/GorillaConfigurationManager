using System.Linq;
using BepInEx.Configuration;
using ComputerInterface.Extensions;
using GorillaConfigurationManager.Patcher;

namespace GorillaConfigurationManager.Views;

public class ConfigView : SelectView
{
    private ConfigFileData config;

    public override void OnShow(object[] args)
    {
        if (args.FirstOrDefault() is not ConfigFileData configData)
        {
            Main.Log("Must be used with params");
            SetText(new System.Text.StringBuilder("Invalid args"));
            return;
        }

        base.Header = "Select Entry:";
        config = configData;
        Lines = GetLines();
        base.OnShow(null);
    }

    public override System.Text.StringBuilder GetContent()
    {
        return base.GetContent()
                   .AppendLines(1)
                   .Append(config.Config.ElementAt(base.SelectedIndex).Value.Description.Description); // TODO Add overflow cutoff
    }

    private LineElement[] GetLines()
    {
        return config.Config.Select(option => new LineElement(option.Value.Definition.Key, () => OnPress(option.Value))).ToArray();
    }

    public void OnPress(ConfigEntryBase entry)
    {
        Main.Log(entry.Definition.Key);
        ShowView<EditView>(entry);
    }
}

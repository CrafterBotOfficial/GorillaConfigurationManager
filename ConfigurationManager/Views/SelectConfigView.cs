using System.IO;
using System.Linq;
using System.Text;
using ComputerInterface;

namespace GorillaConfigurationManager.Views;

public class SelectConfigView : SelectView
{
    public override void OnShow(object[] args)
    {
        Lines = GetLines();
        base.OnShow(null);
    }

    private LineElement[] GetLines()
    {
        // good luck future me, sorry for this monstrosity
        return ConfigManager.Instance.GetConfigs()
            .Select(config => new LineElement(
                        config.Owner is not null
                        ? config.Owner.Name
                        : Path.GetFileName(config.Config.ConfigFilePath),
                        () => ShowView(typeof(Views.ConfigView), config)
                        )
                   ).ToArray();
    }
}

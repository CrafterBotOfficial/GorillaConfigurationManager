using System.Linq;
using ComputerInterface.ViewLib;

namespace GorillaConfigurationManager.Views;

public class ErrorView : ComputerView
{
    public override void OnShow(object[] args)
    {
        var text = $"<color=red>{args.FirstOrDefault()}</color>";
        SetText(new System.Text.StringBuilder());
    }
}

using System.Linq;
using ComputerInterface.Models;

namespace GorillaConfigurationManager.Views;

public class ErrorView : ComputerView
{
    public override void OnShow(object[] args)
    {
        var text = $"<color=red>{args.FirstOrDefault()}</color>";
        SetText(new System.Text.StringBuilder());
    }
}

using ComputerInterface;

namespace GorillaConfigurationManager.ValueEditHandler;

public interface IEditHandler
{
    public void OnManipulate(ref string text, EKeyboardKey key);
    public string GetTooltip();
}

using ComputerInterface;

namespace GorillaConfigurationManager.ValueEditHandler;

public interface IEditHandler
{
    public string GetHeader();
    public void OnManipulate(ref string text, EKeyboardKey key);
}

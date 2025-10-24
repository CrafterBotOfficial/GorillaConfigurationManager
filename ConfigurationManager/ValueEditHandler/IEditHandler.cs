using BepInEx.Configuration;
using ComputerInterface;
using ComputerInterface.Enumerations;

namespace GorillaConfigurationManager.ValueEditHandler;

public interface IEditHandler
{
    public void OnManipulate(ref string text, EKeyboardKey key);
    public void OnSet(string text, ConfigEntryBase entry);
    public void Reset(string defaultValue);
    public string GetTooltip();
}

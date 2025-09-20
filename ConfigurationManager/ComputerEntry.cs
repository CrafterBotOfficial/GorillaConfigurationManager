using System;
using ComputerInterface.Interfaces;

namespace GorillaConfigurationManager;

public class ComputerEntry : IComputerModEntry
{
    public string EntryName => "Config Manager";
    public Type EntryViewType => typeof(Views.SelectConfigView);
}

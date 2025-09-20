using System;
using System.Linq;
using ComputerInterface;
using ComputerInterface.Extensions;

namespace GorillaConfigurationManager.ValueEditHandler;

public class NumberHandler : IEditHandler
{
    public string GetHeader() => "Enter a valid Number:";

    public void OnManipulate(ref string text, EKeyboardKey key)
    {
        if (key.IsNumberKey())
        {
            text += Enum.GetName(typeof(EKeyboardKey), key).Skip(3).ToString(); // numbers start with num<1>
            return;
        }

        if (key == EKeyboardKey.Space)
        {
            text += ".";
            return;
        }
    }
}

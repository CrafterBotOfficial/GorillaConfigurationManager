using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ComputerInterface;
using ComputerInterface.Extensions;
using ComputerInterface.ViewLib;

namespace GorillaConfigurationManager.Views;

public class SelectView : ComputerView
{
    public IEnumerable<LineElement> Lines;

    public string Header = "Select Configuration:";
    public int SelectedIndex => selectionHandler.CurrentSelectionIndex;

    private UISelectionHandler selectionHandler;
    private UIElementPageHandler<LineElement> pageHandler;

    public override void OnShow(object[] args)
    {
        base.OnShow(null);

        if (Lines is null)
        {
            Main.Log("Lines must be assigned before base method is called.", BepInEx.Logging.LogLevel.Error);
            return;
        }

        pageHandler = new UIElementPageHandler<LineElement>();
        pageHandler.EntriesPerPage = 8;
        pageHandler.SetElements(Lines.ToArray());

        selectionHandler = new UISelectionHandler(EKeyboardKey.Up, EKeyboardKey.Down, EKeyboardKey.Enter);
        selectionHandler.OnSelected += OnSelected;
        selectionHandler.MaxIdx = Lines.Count() - 1;
        selectionHandler.ConfigureSelectionIndicator("<color=#ed6540>> </color>", "", "  ", "");

        SetText(GetContent());
    }

    private void OnSelected(int index)
    {
        if (Lines is not null && Lines.Count() > index)
        {
            Lines.ElementAt(index).OnSelect();
            return;
        }
        Main.Log("Error invoking on select", BepInEx.Logging.LogLevel.Error);
    }

    public virtual StringBuilder GetContent()
    {
        if (Lines is null || !Lines.Any())
        {
            return new StringBuilder("Error: No lines");
        }

        pageHandler.MovePageToIdx(selectionHandler.CurrentSelectionIndex);

        var builder = new StringBuilder(Header);
        builder.AppendLines(1);

        pageHandler.EnumarateElements((line, relativeIndex) =>
        {
            int index = pageHandler.GetAbsoluteIndex(pageHandler.CurrentPage, relativeIndex);
            string color = index % 2 == 0 ? "white" : "#ffffff50";
            string text = selectionHandler.GetIndicatedText(index, $"<color={color}>{line.Name}</color>");
            builder.AppendLine(text);
        });

        return builder;
    }

    // public new virtual void OnKeyPressed(EKeyboardKey key)
    public override void OnKeyPressed(EKeyboardKey key)
    {
        if (selectionHandler.HandleKeypress(key))
        {
            SetText(GetContent());
            return;
        }
        if (key == EKeyboardKey.Back)
            ReturnToMainMenu();
    }
}

public record class LineElement(string Name, Action OnSelect);

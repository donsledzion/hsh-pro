using Walls2D;

public class ToolDeleteSection : Selector2D
{

    protected override void SelectSection(WallSection section)
    {
        base.SelectSection(section);
        WallSectionDeleter.DeleteSection(_hoveredSection);
        Unselect();
        Unhover();
        ClearLines();
    }
}

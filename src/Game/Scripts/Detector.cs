using Godot;
using GodotUtilities;

namespace CatTopDown2D;

[Scene]
public partial class Detector : Area2D
{
    [Export(PropertyHint.ColorNoAlpha)]
    private Color invalidColor = Colors.Red;

    [Export(PropertyHint.ColorNoAlpha)]
    private Color validColor = Colors.Green;

    [Node]
    private Sprite2D sprite2D = null!;

    private int objectCount;

    public override void _Notification(int what)
    {
        if (what == NotificationSceneInstantiated)
        {
            WireNodes();
        }
    }

    public override void _Ready()
    {
        BodyEntered += OnBodyEntered;
        BodyExited += OnBodyExited;
    }

    private void OnBodyEntered(Node2D node)
    {
        if (objectCount == 0)
        {
            sprite2D.Modulate = validColor;
        }

        objectCount++;
    }

    private void OnBodyExited(Node2D node)
    {
        objectCount--;
        if (objectCount == 0)
        {
            sprite2D.Modulate = invalidColor;
        }
    }
}
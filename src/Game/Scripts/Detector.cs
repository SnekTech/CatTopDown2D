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
        sprite2D.Modulate = validColor;
    }

    private void OnBodyExited(Node2D node)
    {
        sprite2D.Modulate = invalidColor;
    }
}
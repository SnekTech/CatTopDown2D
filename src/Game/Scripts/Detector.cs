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

    [Export(PropertyHint.Range, "0.1,1")]
    private float pulseFrequency = 0.25f;

    [Node]
    private Sprite2D sprite2D = null!;

    private int objectCount;
    private float pulseProgress;

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

        pulseProgress = GD.Randf();
    }

    public override void _Process(double delta)
    {
        if (objectCount == 0)
        {
            pulseProgress += (float)delta * pulseFrequency;
            if (pulseProgress >= 1)
            {
                pulseProgress -= 1;
            }

            var brightness = Mathf.Cos(pulseProgress * float.Tau) * 0.25f + 0.75f;
            sprite2D.Modulate = invalidColor with
            {
                R = invalidColor.R * brightness,
                G = invalidColor.G * brightness,
                B = invalidColor.B * brightness
            };
        }
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
            pulseProgress = 0;
        }
    }
}
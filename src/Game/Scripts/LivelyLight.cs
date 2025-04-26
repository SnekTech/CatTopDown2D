using Godot;
using GodotUtilities;

namespace CatTopDown2D;

[Scene]
public partial class LivelyLight : Node2D
{
    [Export] private float jitterOffset = 0.25f;
    [Export] private float jitterSpeed = 6.0f;
    [Export(PropertyHint.Range, "0,1")] private float jitterMinEnergy = 0.95f;


    [Node] private PointLight2D light1 = null!;
    [Node] private PointLight2D light2 = null!;

    private float _progress;

    public override void _Notification(int what)
    {
        if (what == NotificationSceneInstantiated)
        {
            WireNodes();
        }
    }

    public override void _Process(double delta)
    {
        _progress += (float)delta * jitterSpeed;
        if (_progress >= 1)
        {
            _progress -= 1;
            Jitter();
        }
    }

    private void Jitter()
    {
        var randomPositionNearby = new Vector2(GetRandomCoord(), GetRandomCoord());
        light1.Position = randomPositionNearby;
        light2.Position = randomPositionNearby;

        var energy = (float)GD.RandRange(jitterMinEnergy, 1);
        light1.Energy = energy;
        light2.Energy = energy;

        return;
        float GetRandomCoord() => (float)GD.RandRange(-jitterOffset, jitterOffset);
    }
}
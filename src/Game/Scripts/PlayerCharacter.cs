using Godot;

namespace CatTopDown2D;

public partial class PlayerCharacter : CharacterBody2D
{
    [Export(PropertyHint.Range, "0,1000")] public int Speed { get; private set; } = 60;

    public override void _PhysicsProcess(double delta)
    {
        GetPlayerInput();

        var hitSome = MoveAndSlide();
        if (hitSome)
        {
            ResolveCollisions();
        }
    }

    private void GetPlayerInput()
    {
        var vector = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
        Velocity = vector * Speed;
    }

    private void ResolveCollisions()
    {
        for (var i = 0; i < GetSlideCollisionCount(); i++)
        {
            var collision = GetSlideCollision(i);
            if (collision.GetCollider() is MovableObject obj)
            {
                obj.ApplyImpact(Velocity);
            }
        }
    }
}
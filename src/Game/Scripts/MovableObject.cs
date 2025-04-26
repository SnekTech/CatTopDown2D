using Godot;

namespace CatTopDown2D;

public partial class MovableObject : CharacterBody2D
{
    [Export(PropertyHint.Range, "0.0,10.0")]
    private float drag = 5;

    [Export(PropertyHint.Range, "0.0,1.0")]
    private float impactResponse = 0.5f;
    

    public override void _PhysicsProcess(double delta)
    {
        if (Velocity.LengthSquared() > 1)
        {
            Velocity *= (float)(1.0 - drag * delta);
            var hitSome = MoveAndSlide();
            if (hitSome)
            {
                ResolveCollisions();
            }
        }
        else
        {
            Position = Position.Round();
            Velocity = Vector2.Zero;
        }
    }

    public void ApplyImpact(Vector2 impactVelocity)
    {
        Velocity += (impactVelocity - Velocity) * impactResponse;
    }

    private void ResolveCollisions()
    {
        var currentVelocity = Velocity;
        for (var i = 0; i < GetSlideCollisionCount(); i++)
        {
            var collision = GetSlideCollision(i);
            if (collision.GetCollider() is MovableObject anotherMoveable)
            {
                ApplyImpact(anotherMoveable.Velocity);
                anotherMoveable.ApplyImpact(currentVelocity);
            }
            else
            {
                Velocity -= Velocity.Project(collision.GetNormal());
            }
        }
    }
}
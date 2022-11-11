using UnityEngine;

public class PawnMover : MonoBehaviour
{
    private IMovable _movable;
    private Rigidbody2D _pawn;
    private Vector2 _direction;

    private void FixedUpdate()
    {
        _movable?.Move(_direction);
    }

    public void UpdatePawn(Rigidbody2D newPawn)
    {
        _pawn = newPawn;
        _movable = _pawn.GetComponent(typeof(IMovable)) as IMovable;
    }

    public void UpdateDirection(Vector2 newDirection)
    {
        _direction = newDirection;
    }
}
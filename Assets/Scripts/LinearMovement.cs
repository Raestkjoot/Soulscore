using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class LinearMovement : MonoBehaviour, IMovementGenerator
{
    private Rigidbody2D _rigidbody;

    public void Move(Vector2 direction, float speed)
    {
        _rigidbody.MovePosition(_rigidbody.position +
                                direction.normalized *
                                speed *
                                Time.fixedDeltaTime);
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }
}
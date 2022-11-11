using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BasicMove : MonoBehaviour, IMove
{
    private Rigidbody2D _rigidbody;

    public void Move(Vector2 dir, float speed)
    {
        _rigidbody.MovePosition(_rigidbody.position +
                                dir.normalized *
                                speed *
                                Time.fixedDeltaTime);
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }
}
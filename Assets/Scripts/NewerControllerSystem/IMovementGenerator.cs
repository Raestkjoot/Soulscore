using UnityEngine;

public interface IMovementGenerator
{
    void Move(Vector2 direction, float speed);
}
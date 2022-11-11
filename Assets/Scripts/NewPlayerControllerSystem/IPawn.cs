using UnityEngine;

public interface IPawn
{
    public void Move(Vector2 dir);
    public void Dash(Vector2 dir);
    public void Attack();
    public void Ability1();
    public void Ability2();
}
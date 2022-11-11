using Common;
using UnityEditor.SceneManagement;
using UnityEngine;

public class Pawn : MonoBehaviour, IPawn
{
    [field: SerializeField] public float MovementSpeed { get; private set; }
    
    private IMove move;
    private float _curMoveSpeed;

    public void Move(Vector2 dir)
    {
        move.Move(dir, _curMoveSpeed);
    }

    public void Dash(Vector2 dir)
    {
        throw new System.NotImplementedException();
    }

    public void Attack()
    {
        throw new System.NotImplementedException();
    }

    public void Ability1()
    {
        throw new System.NotImplementedException();
    }

    public void Ability2()
    {
        throw new System.NotImplementedException();
    }

    private void Awake()
    {
        if (MovementSpeed == 0)
            this.LogLog("Movement speed is set to 0, the player will not move. Remember to set stats in the inspector.");
        else
            _curMoveSpeed = MovementSpeed;

        move = gameObject.AddComponent<BasicMove>();
    }
}
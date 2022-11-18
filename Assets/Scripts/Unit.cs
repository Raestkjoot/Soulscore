using Common;
using UnityEngine;

[RequireComponent(typeof(IMovementGenerator))]
public class Unit : MonoBehaviour
{
    [field: SerializeField] public float MovementSpeed { get; private set; }
    private UnitStats _stats;

    [SerializeField] private Ability ability;


    private IMovementGenerator _movementGenerator;

    // TODO: AbilitySystem
    // TODO: CooldownHistory

    public void Move(Vector2 direction) => 
        _movementGenerator?.Move(direction, _stats.MovementSpeed);

    private void Awake()
    {
        if (MovementSpeed == 0)
            this.LogLog("Movement speed is set to 0, the player will not move. Remember to set stats in the inspector.");
        else
            _stats.MovementSpeed = MovementSpeed;

        if (TryGetComponent(out IMovementGenerator movementGenerator))
            _movementGenerator = movementGenerator;
        else
            this.LogError("No MovementGenerator");
    }
}
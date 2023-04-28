using UnityEngine;

[RequireComponent(typeof(IMovementGenerator))]
public class Unit : MonoBehaviour
{
    [SerializeField] private AttributesContainerSO attributes;

    [SerializeField] private Ability ability;


    private IMovementGenerator _movementGenerator;

    // TODO: AbilitySystem
    // TODO: CooldownHistory

    public void Move(Vector2 direction) => 
        _movementGenerator?.Move(direction, attributes.GetAttributeCurValue(EAttribute.MovementSpeed));

    private void Awake()
    {
        attributes.Initialize();
        ILogr logger = new ConsoleLogger();

        if (attributes.GetAttributeCurValue(EAttribute.MovementSpeed) == 0)
            logger.Log("Movement speed is set to 0, the player will not move. Remember to set stats in the inspector.");

        if (TryGetComponent(out IMovementGenerator movementGenerator))
            _movementGenerator = movementGenerator;
        else
            logger.Error("No MovementGenerator");
    }
}
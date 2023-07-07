using System.Threading.Tasks;
using UnityEngine;

[RequireComponent(typeof(IMovementGenerator))]
public class Unit : MonoBehaviour
{
    [SerializeField] private AttributesContainerSO attributes;

    [SerializeField] private Ability ability;

    private ILogr _logger;
    private IMovementGenerator _movementGenerator;

    private Vector2 _direction;

    private float _force;
    private Vector2 _forceDir;
    private float _forceDuration;
    private float _friction;

    // TODO: AbilitySystem
    // TODO: CooldownHistory

    public void Move(Vector2 direction)
    {
        float speed = attributes.GetAttributeCurValue(EAttribute.MovementSpeed);
        // only update _direction if direction is not 0. This way we can still use last input direction when not moving.
        if (direction.magnitude > 0)
        {
            _direction = direction;
        }

        if (_force != 0)
        {
            // Add force and movement direction together
            // TODO: make sure both vectors are normalized at this point.
            _direction *= speed;
            Vector2 relativeForceDir = _forceDir *_force;
            _direction += relativeForceDir;

            speed += _force;
            _movementGenerator?.Move(_direction, speed);
        }
        else
        {
            _movementGenerator?.Move(direction, speed);
        }
    }

    public void ApplyForceInMoveDirection(float force, float forceDuration, float friction) =>
        ApplyForce(force, _direction, forceDuration, friction);

    public void ApplyForce(float force, Vector2 forceDir, float forceDuration, float friction)
    {
        _force += force;
        _forceDir += forceDir;
        _friction = friction;

        // calculate max duration
        _forceDuration = Mathf.Clamp(forceDuration, 0.01f, _force / _friction);
        ApplyFriction();
    }

    private async void ApplyFriction()
    {
        while (_force >= 0 && _forceDuration >= 0)
        {
            _force -= _friction * Time.deltaTime;
            _forceDuration -= Time.deltaTime;
            await Task.Yield();
        }

        _force = 0;
        _forceDir = Vector2.zero;
    }

    private void Awake()
    {
        attributes.Initialize();
        _logger = new ConsoleLogger();

        if (attributes.GetAttributeCurValue(EAttribute.MovementSpeed) == 0)
            _logger.Log("Movement speed is set to 0, the player will not move. Remember to set stats in the inspector.");

        if (TryGetComponent(out IMovementGenerator movementGenerator))
            _movementGenerator = movementGenerator;
        else
            _logger.Error("No MovementGenerator");
    }
}
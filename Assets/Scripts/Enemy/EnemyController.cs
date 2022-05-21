using UnityEngine;

namespace Enemy
{
    public class EnemyController : MonoBehaviour
    {
        public Transform player;
        
        // States
        public OutOfRangeTempState outOfRangeTempState;
        public AttackState attackState;

        private float attackRange;
        private float attackSpeed;
        private float attackDamage;

        public float StopAtDist { get; private set; }
        public float CurrentDist { get; private set; }

        private Rigidbody2D _rigidbody;
        private EnemyStateMachine _StateMachine;

        public float GetDistToPlayer()
        {
            return Vector3.Distance(transform.position, player.position);
        }

        private void Move(Vector2 direction, float speed)
        {
            _rigidbody.MovePosition(_rigidbody.position + direction.normalized * speed * Time.fixedDeltaTime);
        }

        private void Start()
        {
            _rigidbody = gameObject.GetComponent<Rigidbody2D>();

            _StateMachine = new EnemyStateMachine();

            outOfRangeTempState = new OutOfRangeTempState(_StateMachine, this);
            attackState = new AttackState(_StateMachine, this);

            _StateMachine.Initialize(outOfRangeTempState);
        }

        private void Update()
        {
            _StateMachine.CurrentState.LogicUpdate();
        }
        private void FixedUpdate()
        {
            _StateMachine.CurrentState.PhysicsUpdate();
        }

    } 
}
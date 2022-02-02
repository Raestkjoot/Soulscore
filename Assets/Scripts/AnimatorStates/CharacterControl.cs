using UnityEngine;

namespace PlayerAnimStates
{
    public class CharacterControl : MonoBehaviour
    {
        private Rigidbody2D _rigidbody => gameObject.GetComponent<Rigidbody2D>();
        [field: SerializeField] public float MovementSpeed { get; private set; }

        public void Move(Vector2 direction, float speed)
        {
            _rigidbody.MovePosition(_rigidbody.position + direction.normalized * speed * Time.fixedDeltaTime);
        }
    }
}
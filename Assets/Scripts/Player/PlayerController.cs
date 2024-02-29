using UnityEngine;

namespace MatteoLoPiccolo.Player
{
    public class PlayerController : MonoBehaviour
    {
        [Header("Player Components")]
        [SerializeField] private Rigidbody2D _rb2D;

        [Space]
        [Header("Collision Check")]
        [SerializeField] private Transform _groundCheck;
        [SerializeField] private float _groundCheckRadius;
        [SerializeField] private LayerMask _groundLayer;

        private bool _isGrounded;

        [Space]
        [Header("Move variables")]
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _jumpForce;

        private float _xInput;

        private void Update()
        {
            Move();
            GroundCheckCollision();

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }
        }

        private void Jump()
        {
            if (_isGrounded)
                _rb2D.velocity = new Vector2(_rb2D.velocity.x, _jumpForce);
        }

        private void Move()
        {
            _xInput = Input.GetAxisRaw("Horizontal");
            _rb2D.velocity = new Vector2(_xInput * _moveSpeed, _rb2D.velocity.y);
        }

        private void GroundCheckCollision()
        {
            _isGrounded = Physics2D.OverlapCircle(_groundCheck.position, _groundCheckRadius, _groundLayer);
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(_groundCheck.position, _groundCheckRadius);
        }
    }
}
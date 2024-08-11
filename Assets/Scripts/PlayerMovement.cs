using UnityEngine;

[RequireComponent( typeof( CharacterController ) )]
public class PlayerMovement : MonoBehaviour
{
    [Header( "Movement Settings" )]
    [SerializeField] private float _moveSpeed = 6f;
    [SerializeField] private float _rotationSpeed = 500f;

    [Header( "Jumping Settings" )]
    [SerializeField] private float _jumpHeight = 2f;
    [SerializeField] private float _gravity = -9.8f;
    [SerializeField] private float _groundDistance = 0.4f;
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private Transform _feetTransform;  // Assign the "Feet" GameObject here

    private CharacterController _characterController;
    private Vector3 _velocity;
    private bool _isGrounded;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        _isGrounded = CheckGroundStatus();
        HandleMovement();
        HandleJump();
        ApplyGravity();
    }

    private bool CheckGroundStatus()
    {
        // Check if feetTransform is touching the ground
        bool grounded = Physics.CheckSphere( _feetTransform.position, _groundDistance, _groundMask );
        if (grounded && _velocity.y < 0)
        {
            _velocity.y = -2f; // Keep the player grounded
        }
        return grounded;
    }

    private void HandleMovement()
    {
        float horizontalInput = Input.GetAxisRaw( "Horizontal" );
        float verticalInput = Input.GetAxisRaw( "Vertical" );

        Vector3 dir = new Vector3( horizontalInput, 0, verticalInput ).normalized;

        if (dir != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation( dir );
            transform.rotation = Quaternion.RotateTowards( transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime );
        }

        _characterController.Move( dir * _moveSpeed * Time.deltaTime );
    }

    private void HandleJump()
    {
        if (Input.GetButtonDown( "Jump" ) && _isGrounded)
        {
            _velocity.y = Mathf.Sqrt( _jumpHeight * -2f * _gravity );
        }
    }

    private void ApplyGravity()
    {
        _velocity.y += _gravity * Time.deltaTime;
        _characterController.Move( _velocity * Time.deltaTime );
    }

    private void OnDrawGizmosSelected()
    {
        if (_feetTransform == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere( _feetTransform.position, _groundDistance );
    }
}

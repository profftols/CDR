using System;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Moving : MonoBehaviour
{
    private const string Horizontal = "Horizontal";
    private const string Vertical = "Vertical";
    
    [SerializeField] private float _speed = 6.0f;
    
    private CharacterController _characterController;
    
    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        if (_characterController != null)
        {
            Vector3 playerInput = new Vector3(Input.GetAxis(Horizontal), 0, Input.GetAxis(Vertical));
            playerInput *= Time.deltaTime * _speed;

            if (_characterController.isGrounded)
            {
                _characterController.Move(playerInput + Vector3.down);
            }
            else
            {
                _characterController.Move(_characterController.velocity + Physics.gravity * Time.deltaTime);
            }
        }
    }
}

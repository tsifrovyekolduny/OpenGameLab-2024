using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5;
    [SerializeField] float mouseSensivity = 10;
    [SerializeField] float health = 100;
    [SerializeField] float dashDistance = 30;
    [SerializeField] float dashReloadTime = 1;
    [SerializeField] Camera gameCamera;
    [SerializeField] Vector3 position;

    private Vector3 _moveDirection;
    private CharacterController _characterController;
    private PlayerInput _playerInput;
    private Vector2 _mouseMove;
    private float xRotation = 0f;
    private bool _isDashReloading;
    private bool _isDashing;
    readonly private float _dashFrames = 30f;
    readonly private float _dashTime = 0.2f;

    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _playerInput = GetComponent<PlayerInput>();
        _playerInput.actions.Enable();
        _isDashReloading = false;
        _isDashing = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    IEnumerator DashReload()
    {

        _isDashReloading = true;

        yield return new WaitForSeconds(dashReloadTime);

        _isDashReloading = false;
    }

    IEnumerator DashProcess(Vector2 moveDirection)
    {
        _isDashing = true;
        _moveDirection = (moveDirection.y * transform.forward) + (moveDirection.x * transform.right);
        float oneDashFrame = _dashTime / _dashFrames;
        Debug.Log(oneDashFrame);
        float moveInOneFrame = dashDistance * oneDashFrame;
        float counter = _dashTime;
        while (counter > 0) 
        {
            _characterController.Move(moveInOneFrame * _moveDirection);
            counter -= oneDashFrame;
            yield return new WaitForSeconds(oneDashFrame);
        }
        _isDashing = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_isDashing)
        {
            Move();
        }
        
        LookAround();
    }
    

    public void Move()
    {
        Vector2 moveDirection = _playerInput.actions.FindAction("Movement").ReadValue<Vector2>();
        _moveDirection = (moveDirection.y * transform.forward) + (moveDirection.x * transform.right);
        _characterController.Move(moveSpeed * Time.deltaTime * _moveDirection);
    }

    public void LookAround()
    {
        _mouseMove = _playerInput.actions.FindAction("LookAround").ReadValue<Vector2>();

        float mouseX = _mouseMove.x * mouseSensivity * Time.deltaTime;
        float mouseY = _mouseMove.y * mouseSensivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -40, 40);

        gameCamera.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        gameObject.transform.Rotate(Vector3.up * mouseX);
    }

    public void Dash(InputAction.CallbackContext context)
    {
        if (context.performed && !_isDashReloading)
        {
            Vector2 moveDirection = _playerInput.actions.FindAction("Movement").ReadValue<Vector2>();
            StartCoroutine(DashProcess(moveDirection));
            StartCoroutine(DashReload());
            //Vector2 moveDirection = _playerInput.actions.FindAction("Movement").ReadValue<Vector2>();
            //_moveDirection = (moveDirection.y * transform.forward) + (moveDirection.x * transform.right);
            //_characterController.Move(dashDistance * _moveDirection);
        }
        
    }
}

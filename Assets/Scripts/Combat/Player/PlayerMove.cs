using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    public const float Gravity = -9.81f;

    private float _gravitatedY = 0f;
    private Vector3 _moveVector;
    private Vector3 _moveInput;
    private CharacterController _controller;
    private PlayerInputs _playerInputs;
    [SerializeField] private float _rotationSpeed = 20f;
    [SerializeField] private Camera _camera;

    private bool _isMoving;
    private Animator _animator;

    private Dictionary<StatType, float> _stats;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();

        _playerInputs = new PlayerInputs();

        _animator = GetComponentInChildren<Animator>();

        _playerInputs.Player.Move.performed += Move;
        _playerInputs.Player.Move.canceled += Move;

        GameManager.OnGamePaused += GameStateChanged;
        GameManager.OnInventoryUpdate += GameStateChanged;
        GameManager.OnPlayerLiveStatusUpdate += GameStateChanged;
        GameManager.OnStatWindowUpdate += GameStateChanged;
    }

    void OnEnable()
    {
        _playerInputs.Player.Enable();
    }
    void Start()
    {
        if (_camera == null)
        {
            _camera = Camera.main;
        }
        _stats = GetComponent<PlayerStats>().Stats;
    }

    void Update()
    {
        CalculateGravity();

        _moveVector = (_camera.transform.forward * _moveInput.y + _camera.transform.right * _moveInput.x);
        _moveVector.y = 0;
        _moveVector.Normalize();

        if (_moveVector.magnitude > 0.1f)
        {
            _controller.Move(_moveVector * ((_stats != null ? _stats[StatType.Speed] : 5f) * Time.deltaTime));
            Quaternion targetRot = Quaternion.LookRotation(_moveVector);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * _rotationSpeed);
        }
        if (_gravitatedY < 0)
        {
            _controller.Move(new Vector3(0, _gravitatedY * Time.deltaTime, 0));
        }

    }

    private void OnDisable()
    {
        _playerInputs.Player.Disable();
    }

    private void OnDestroy()
    {
        _playerInputs.Player.Move.performed -= Move;
        _playerInputs.Player.Move.canceled -= Move;

        GameManager.OnGamePaused -= GameStateChanged;
        GameManager.OnInventoryUpdate -= GameStateChanged;
        GameManager.OnPlayerLiveStatusUpdate -= GameStateChanged;
        GameManager.OnStatWindowUpdate -= GameStateChanged;
    }

    void Move(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _moveInput = context.ReadValue<Vector2>();
            _isMoving = true;
            _animator.SetBool("IsMoving", _isMoving);
        }
        if (context.canceled)
        {
            _moveInput = Vector3.zero;
            _isMoving = false;
            _animator.SetBool("IsMoving", _isMoving);
        }

    }

    void CalculateGravity()
    {
        if (_controller.isGrounded)
        {
            _gravitatedY = 0;
            return;
        }
        _gravitatedY += Gravity * Time.deltaTime;

    }
    private void GameStateChanged(bool lockMove)
    {
        if (lockMove)
            _playerInputs.Player.Disable();
        else
            _playerInputs.Player.Enable();
    }
}

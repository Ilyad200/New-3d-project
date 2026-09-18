using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private float _mouseSensitivity = 0.1f;

    [SerializeField] private GameObject _target;
    [SerializeField] private float _distance = 10f;

    [SerializeField] private float _minPitch = 0;
    [SerializeField] private float _maxPitch = 60f;

    [SerializeField] private float _rotationSmoothTime = 0.08f;

    private float _xRotation;
    private float _yRotation;
    private Vector3 _currentRotation;
    private Vector3 _rotationSmoothVelocity;

    private PlayerInputs _input;

    [SerializeField] private Camera _camera;

    private void Awake()
    {
        _input = new PlayerInputs();

        GameManager.OnGamePaused += GameStateChanged;
        GameManager.OnInventoryUpdate += GameStateChanged;
        GameManager.OnPlayerLiveStatusUpdate += GameStateChanged;
        GameManager.OnStatWindowUpdate += GameStateChanged;
    }
    void Start()
    {
        _xRotation = transform.rotation.x;
        _yRotation = transform.rotation.y;
    }

    private void OnEnable() => _input.Player.Enable();
    private void OnDisable() => _input.Player.Disable();


    void Update()
    {
        MoveCamera();
        RotateCamera();
    }

    void RotateCamera()
    {
        Vector2 mouseDelta = _input.Player.Look.ReadValue<Vector2>();

        if (mouseDelta.sqrMagnitude < 0.001f) return;

        float yaw = mouseDelta.x;
        float pitch = mouseDelta.y;

        _xRotation -= pitch * _mouseSensitivity;
        _yRotation += yaw * _mouseSensitivity;

        _xRotation = Mathf.Clamp(_xRotation, _minPitch, _maxPitch);

        Vector3 targetEuler = new(_xRotation, _yRotation, 0f);

        transform.rotation = Quaternion.Euler(targetEuler);
    }

    void MoveCamera()
    {
        transform.position = _target.transform.position;

        _camera.transform.position = transform.position - _camera.transform.forward * _distance;
    }
    private void GameStateChanged(bool lockCameraMove)
    {
        if (lockCameraMove)
            _input.Player.Disable();
        else
            _input.Player.Enable();
    }

    private void OnDestroy()
    {
        GameManager.OnGamePaused -= GameStateChanged;
        GameManager.OnInventoryUpdate -= GameStateChanged;
        GameManager.OnPlayerLiveStatusUpdate -= GameStateChanged;
        GameManager.OnStatWindowUpdate -= GameStateChanged;
    }
}

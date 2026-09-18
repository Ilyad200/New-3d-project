using UnityEngine;

public class EnemyHPBar : HPBar
{
    [SerializeField] private float _yOffset = 1f;
    [SerializeField] private Transform _camera;

    protected override void Awake()
    {
        base.Awake();
        transform.localPosition = new Vector3(0, _yOffset, 0);
    }
    protected override void Start()
    {
        base.Start();
        if (_camera == null)
        {
            _camera = Camera.main.transform;
        }
        if (_camera == null)
        {
            Debug.Log($"{name} couldn't find Camera");
        }
    }
    private void LateUpdate()
    {
        if (_camera == null) return;

        transform.rotation = Quaternion.LookRotation(transform.position - _camera.position, Vector3.up);
    }
}

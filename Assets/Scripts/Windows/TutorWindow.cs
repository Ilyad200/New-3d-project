using UnityEngine;

public class TutorWindow : MonoBehaviour
{
    [SerializeField] private Camera _camera;

    private void Start()
    {
        if (_camera == null)
        {
            _camera = Camera.main;
        }
    }
    private void LateUpdate()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - _camera.transform.position, Vector3.up);
    }
}

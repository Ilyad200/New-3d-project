using System;
using UnityEngine;
using UnityEngine.Events;

public class DetectionTrigger : MonoBehaviour
{
    public event Action<Transform> OnPlayerIn;
    public event Action<Transform> OnPlayerOut;

    private SphereCollider _collider;
    public SphereCollider Collider => _collider;

    private void Awake()
    {
        _collider = GetComponent<SphereCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log($"[Trigger] player enter");
            OnPlayerIn.Invoke(other.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnPlayerOut.Invoke(other.transform);
            Debug.Log($"[Trigger] player exit");
        }
        }
}

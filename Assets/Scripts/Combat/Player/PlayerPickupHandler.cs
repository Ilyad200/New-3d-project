using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerPickupHandler : MonoBehaviour
{
    [SerializeField] private Inventory _inventory;
    [SerializeField] private float _pickupRange = 3f;
    [SerializeField] private LayerMask _pickupLayer;

    private PlayerInputs input;

    private void Awake()
    {
        input = new PlayerInputs();
    }

    private void OnEnable()
    {
        input.Player.Pickup.performed += OnPickupPressed;
        input.Player.Pickup.Enable();
    }

    private void OnDisable()
    {
        input.Player.Pickup.performed -= OnPickupPressed;
        input.Player.Pickup.Disable();
    }

    private void OnPickupPressed(InputAction.CallbackContext context)
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, _pickupRange, _pickupLayer);

        WorldPickup closest = null;
        float minDistSqr = Mathf.Infinity;

        foreach (var col in hits)
        {
            if (col.TryGetComponent<WorldPickup>(out var pickup))
            {
                float distSqr = Vector3.SqrMagnitude(transform.position - pickup.transform.position);
                if (distSqr < minDistSqr)
                {
                    minDistSqr = distSqr;
                    closest = pickup;
                }
            }
        }

        if (closest != null)
        {
            closest.TryPickup(_inventory);
        }
    }
}

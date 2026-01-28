using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private LayerMask _interactableMask;
    [SerializeField] private Transform _selectedObject;

    private PlayerStats _stats;
    private PlayerInput _input;
    private LockPick _lockPick;

    private void Start()
    {
        _stats = GetComponent<PlayerStats>();
        if (_stats == null) DebugManager.LogMissingComponent(gameObject, "PlayerInteraction", "PlayerStats");

        _input = GetComponent<PlayerInput>();
        if (_input == null) DebugManager.LogMissingComponent(gameObject, "PlayerInteraction", "PlayerInput");

        _lockPick = GetComponent<LockPick>();
        if (_lockPick == null) DebugManager.LogMissingComponent(gameObject, "PlayerInteraction", "LockPick");

    }

    private void Update()
    {
        if (!_input.isInteracting) return;

        Ray ray = new Ray(_selectedObject.position, _selectedObject.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, _stats.interactionRange, _interactableMask))
        {
            if (_input.isInteracting && hit.collider.TryGetComponent<IInteractable>(out IInteractable interactable))
            {
                interactable.Interact();
            }
        }

        _input.ResetInteractInput();
    }

    private void OnDrawGizmos()
    {
        if (_stats == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawRay(_selectedObject.position, _selectedObject.forward * _stats.interactionRange);
    }
}

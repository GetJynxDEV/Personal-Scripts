using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    #region Variables

    [Header("INTERACT PROPERTIES")]

    [SerializeField] private float interactCooldown = 2.5f; // seconds
    [SerializeField] private float interactTimer = 0f;
    [SerializeField] private bool isInteracting;

    //This will determine the layer index for ite
    [SerializeField] private int itemIndex;

    //SCRIPTS that are interactable shall always start as Null
    [SerializeField] private tombstone currentTombstone;

    #endregion

    #region Unity Methods

    private void Start()
    {
        resetValue();

        itemIndex = LayerMask.NameToLayer("item");
    }

    private void Update()
    {
        if (interactTimer > 0f)
            interactTimer -= Time.deltaTime;
    }

    void resetValue()
    {
        interactTimer = 0f;
        //currentBox = null;
    }

    #endregion

    #region Interact Input

    public void Interact(InputAction.CallbackContext context)
    {
        if (interactTimer > 0f || currentTombstone == null) return;

        Debug.Log("PLAYER HAS INTERACTED");

        if (!currentTombstone.hasCompletedInteraction)
        {
            currentTombstone.hasInteracted = true;
            currentTombstone.spawnItem(gameObject); //Pass to Player    
            interactTimer = interactCooldown; //Reset Timer
        }
    }

    #endregion

    #region Trigger Enter

    private void OnTriggerEnter2D(Collider2D collision)
    {
        tombstone tombstoneCS = collision.GetComponent<tombstone>();
        if (tombstoneCS != null)
        {
            currentTombstone = tombstoneCS;

            Debug.Log("Tombstone detected");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        currentTombstone = null;

        Debug.Log("Player has left the Tombstone Trigger Zone");
    }

    #endregion
}

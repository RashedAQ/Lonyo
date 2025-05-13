using UnityEngine;

public class Door : MonoBehaviour, IInteraction
{
    [SerializeField] private Animator doorAnimationController = null;
    public bool isDoorOpen = false; 
    public string requiredKey = "Key";
    [SerializeField] private BoxCollider doorInteractionCollider;

    void Start()
    {
        if (doorAnimationController == null)
            doorAnimationController = GetComponent<Animator>();
        if (doorInteractionCollider == null)
            doorInteractionCollider = GetComponent<BoxCollider>();
        gameObject.layer = LayerMask.NameToLayer("Interactable"); 
    }
    // This method handles the interaction with the door, opening or closing it based on the player's key.
    public void Interact()
    {
        if (!InventorySystem.Instance.itemList.Contains(requiredKey))
        {
            Debug.Log("You need a key to open this door!");
            return;
        }

        if (doorAnimationController != null)
        {
            isDoorOpen = !isDoorOpen;

            if (isDoorOpen)
            {
                doorAnimationController.Play("Door_Open");

            }
            else
            {
                doorAnimationController.Play("Door_Close");
                
            }
        }
    }
    // This method returns the prompt text for interacting with the door.
    public string GetPrompt()
    {
        if (!InventorySystem.Instance.itemList.Contains(requiredKey))
        {
            return "You need a key to open this door!";
        }

        if (isDoorOpen)
        {
            return "Press E to close the door";
        }
        else
        {
            return "Press E to open the door";
        }
    }
}
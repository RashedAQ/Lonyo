using UnityEngine;

public class Gun : MonoBehaviour
{
    public Transform holdPosition;
    public float throwForce = 500f;
    public float pickUpRange = 9f;

    public static bool isHoldingObject = false;

    public GameObject heldObject;
    private Rigidbody heldObjectRb;

    private bool isHolding = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {

            isHolding = true;
        }
    }

    // Picks up an object when pressing R
    public void TryPickupWithKey()
    {
        if (Input.GetKeyDown(KeyCode.R)&& isHolding==true)
        {
      
            isHoldingObject = true;
           RaycastHit hit;

            if (Physics.Raycast(transform.position, transform.forward, out hit, pickUpRange))
            {
                if (hit.transform.GetComponent<Door>() != null)
                    return;

                IInteraction interactable = hit.transform.GetComponent<IInteraction>();
                if (interactable != null)
                {
                    interactable.Interact();
                  
                }
            }

        }
       
    }

    // Handles picking up, dropping, and throwing the gun with keys

    public void HandlePickupInput()
    {



      
        if (Input.GetKeyDown(KeyCode.V) )
        {
            if (heldObject == null)
                TryPickup();
            else
                DropObject();
        }

        if (heldObject != null && Input.GetKeyDown(KeyCode.Tab))
        {
            ThrowObject();
        }
    }
    // Attempts to pick up an object in front of the player
    private void TryPickup()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, pickUpRange))
        {
            if (hit.transform.GetComponent<Door>() != null)
                return;

            IInteraction interactable = hit.transform.GetComponent<IInteraction>();
            if (interactable != null)
            {
                interactable.Interact();
                PickUpObject(hit.transform.gameObject);
            }
        }
    }
    // Picks up the specified object
    public void PickUpObject(GameObject obj)
    {
        heldObject = obj;
        heldObjectRb = obj.GetComponent<Rigidbody>();

        if (heldObjectRb == null)
            return;

        heldObjectRb.isKinematic = true;
        isHoldingObject = true;

        heldObject.transform.SetParent(holdPosition);
        heldObject.transform.localPosition = Vector3.zero;
        heldObject.transform.localRotation = Quaternion.identity;


      
    }
    // Drops the currently held object
    private void DropObject()
    {
        if (heldObject != null)
        {
            isHoldingObject = false;

            heldObject.transform.SetParent(null);
            heldObjectRb.isKinematic = false;

            heldObject = null;
            heldObjectRb = null;
       
        }
    }
    // Throws the currently held object
    private void ThrowObject()
    {
        if (heldObject != null)
        {
            isHoldingObject = false;

            heldObject.transform.SetParent(null);
            heldObjectRb.isKinematic = false;
            heldObjectRb.AddForce(transform.forward * throwForce);

            heldObject = null;
            heldObjectRb = null;
        
        }
    }
}


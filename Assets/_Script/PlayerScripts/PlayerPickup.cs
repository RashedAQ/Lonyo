using UnityEngine;

public class PlayerPickup : MonoBehaviour
{
    public Transform holdPosition;
    public float throwForceAmount = 500f;
    public float pickUpRange = 9f;

    private GameObject heldObject;
    private Rigidbody heldObjectRb;

    // This method handles the player's input for picking up, dropping, or throwing an object.

    public void Pickup()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            if (heldObject == null)
                TryPickup();
            else
                DropObject();
        }

        if (heldObject != null && Input.GetKeyDown(KeyCode.Q))
        {
            ThrowObject();
        }
    }
    // This method attempts to pick up an object by casting a ray and checking for interactable objects.
    private void TryPickup()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, pickUpRange))
        {

            if (hit.transform.GetComponent<Door>() != null)
            {
                return;
            }

            IInteraction interactable = hit.transform.GetComponent<IInteraction>();
            if (interactable != null)
            {
                interactable.Interact();
                PickUpObject(hit.transform.gameObject);
            }
        }
    }

    // This method picks up the object, attaches it to the player's hold position, and makes it kinematic.
    private void PickUpObject(GameObject obj)
    {
        heldObject = obj;
        heldObjectRb = obj.GetComponent<Rigidbody>();

        heldObjectRb.isKinematic = true;

        heldObject.transform.SetParent(holdPosition);
        heldObject.transform.localPosition = Vector3.zero;
        heldObject.transform.localRotation = Quaternion.identity;

    }

    // This method drops the held object, making it non-kinematic and detaching it from the player's hold position.
    private void DropObject()
    {
        if (heldObject != null)
        {
            heldObject.transform.SetParent(null); 
            heldObjectRb.isKinematic = false;
            heldObject = null;
        }
    }
    // This method throws the held object with force in the direction the player is facing.
    private void ThrowObject()
    {
        if (heldObject != null)
        {
            heldObject.transform.SetParent(null); 
            heldObjectRb.isKinematic = false;
            heldObjectRb.AddForce(transform.forward * throwForceAmount);
            heldObject = null;
        }
    }

}

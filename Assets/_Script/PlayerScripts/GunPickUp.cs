using UnityEngine;

public class GunPickUp : MonoBehaviour, IInteraction
{
    public void Interact()
    {
        Debug.Log("You interacted with: " + gameObject.name);
    }
    public string GetPrompt()
    {
        return "Press E to pick up The Gun";
    }
}

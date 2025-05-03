using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyUi : MonoBehaviour, IInteraction
{
    public string GetPrompt()
    {
        return "Press E to pick up Key";
    }

    public void Interact()
    {
        Debug.Log("You interacted with: " + gameObject.name);
    }

}

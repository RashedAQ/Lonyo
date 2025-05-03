using UnityEngine;

public class PressKeyBell : MonoBehaviour, IInteraction
{
    public AudioSource bellAudioSource;
    // This method returns the prompt text for interacting with the bell.
    public string GetPrompt()
    {
        return "Press E to Ring the Bell";
    }
    public void Interact()
    {
        Debug.Log("Bell Pressed!");
        bellAudioSource.PlayOneShot(bellAudioSource.clip);
    }
}

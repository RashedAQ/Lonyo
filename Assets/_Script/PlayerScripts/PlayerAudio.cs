using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip backgroundMusic;

    void Start()
    {
        PlayerAudioSource();
    }
    // This method sets the background music clip to the audio source and plays it.
    public void PlayerAudioSource()
    {
            audioSource.clip = backgroundMusic;
            audioSource.Play();
    }
}

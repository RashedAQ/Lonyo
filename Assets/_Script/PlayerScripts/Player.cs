
using TMPro;
using UnityEngine;

[RequireComponent(typeof(PlayerAudio), typeof(PlayerPickup), typeof(Gun))]
public class Player : MonoBehaviour
{
    private PlayerAudio playerAudio;
    private Gun gun;
    private PlayerPickup playerPickup;

    private void Start()
    {
        playerAudio = GetComponent<PlayerAudio>();

        playerPickup = GetComponent<PlayerPickup>();
        gun = GetComponent<Gun>();
        playerAudio.PlayerAudioSource();
    }

    private void Update()
    {
        playerPickup.Pickup();
        gun.HandlePickupInput();
        gun.TryPickupWithKey();



    }

 

}

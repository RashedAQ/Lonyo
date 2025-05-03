

using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class GoldenApple : MonoBehaviour, IUsable, IInteraction, IDataPersistence
{
    [Header("Speed Boost Settings")]
    [Tooltip("Speed multiplier (2 = double speed)")]
    public float speedMultiplier = 2f;
    [Tooltip("Duration of the speed boost in seconds")]
    public float boostDuration = 5f;

    [Header("Visual Effects")]
    public ParticleSystem pickupEffect;
    public AudioClip pickupSound;

    private bool hasBeenCollected = false;
    [SerializeField] private string goldenAppleID;
    public GameObject goldenAppleVisual;


    [ContextMenu("Generate GUID id")]
    private void GenerateGuid()
    {
        goldenAppleID = System.Guid.NewGuid().ToString();
    }


    public void Interact()
    {
       
    }

    public string GetPrompt()
    {
        return "Press E to Pickup the Golden Apple";
    }
    // This method loads the saved data, specifically the collected state of this Golden Apple, and disables its visual representation if it was collected.
    public void LoadData(GameData data)
    {
    
        if (data._isCollected.TryGetValue(goldenAppleID, out hasBeenCollected) && hasBeenCollected)
        {
            goldenAppleVisual.SetActive(false);
        }
    }
    // This method saves the collected state of the Golden Apple in the data.
    public void SaveData(ref GameData data)
    {
        if (data._isCollected.ContainsKey(goldenAppleID))
        {
            data._isCollected[goldenAppleID] = hasBeenCollected;
        }
        else
        {
            data._isCollected.Add(goldenAppleID, hasBeenCollected);
        }
      
    }

    // This method is used when the player "eats" the Golden Apple, applying a speed boost and playing visual and sound effects.
    public void Use()
    {
        if (hasBeenCollected) return;

        var player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            var controller = player.GetComponent<FirstPersonController>();
            if (controller != null)
            {
                controller.AddSpeedModifier(speedMultiplier, boostDuration);

                if (pickupEffect != null)
                    Instantiate(pickupEffect, transform.position, Quaternion.identity);

                if (pickupSound != null)
                    AudioSource.PlayClipAtPoint(pickupSound, transform.position);

                Debug.Log($"Golden apple used! Speed boosted by {speedMultiplier}x for {boostDuration} seconds");

               hasBeenCollected = true;
                goldenAppleVisual.SetActive(false);

             
            }
        }
    }
}
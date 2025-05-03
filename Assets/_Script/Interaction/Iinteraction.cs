using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Interaction : MonoBehaviour
{
    private IInteraction currentInteraction;
    public float interactionDistance = 6f;
    public string interactionButton = "E";
    public Camera playerCamera;
    public TextMeshProUGUI interactionText;
    public GameObject promptUI; 
    public static string InteractionButtonText { get; private set; } = "E";

    void Start()
    {
        if (playerCamera == null)
        {
          
            playerCamera = Camera.main;
            if (playerCamera == null)
                Debug.LogError("Player Camera is not assigned in the Inspector on the Interaction script!");
        }

        if (interactionText == null)
        {
            Debug.LogError("Interaction Text is not assigned in the Inspector on the Interaction script!");
        }
        InteractionButtonText = interactionButton;

        if (promptUI != null)
            promptUI.SetActive(false);
    }

    void Update()
    {
        DetectInteractable();

        if (Input.GetKeyDown(KeyCode.E) && currentInteraction != null)
        {
            currentInteraction.Interact();
        }
    }
    // Casts a ray to detect interactable objects
    void DetectInteractable()
    {
        RaycastHit hit;
        Vector3 rayOrigin = playerCamera.transform.position;
        Vector3 rayDirection = playerCamera.transform.forward;

        Debug.DrawRay(rayOrigin, rayDirection * interactionDistance, Color.white);

    /*    if (Physics.Raycast(rayOrigin, rayDirection, out hit, interactionDistance))
        {
            IInteraction interaction = hit.transform.GetComponent<IInteraction>();

            if (interaction != null)
            {
                Debug.DrawRay(rayOrigin, rayDirection * hit.distance, Color.yellow);
                currentInteraction = interaction;
                string prompt = interaction.GetPrompt();

                if (interactionText != null)
                {
                    interactionText.text = prompt;
                }

                if (promptUI != null)
                    promptUI.SetActive(true);

                return;
            }
        }*/

        currentInteraction = null;

        if (promptUI != null)
            promptUI.SetActive(false);

   
        if (interactionText != null)
        {
            interactionText.text = "";
        }
    }
}
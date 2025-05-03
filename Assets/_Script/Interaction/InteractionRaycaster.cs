

using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class InteractionRaycaster : MonoBehaviour
{
    [Header("Raycasting Settings")]
    public float rayDistance = 3f;
    public LayerMask interactLayer;
    public bool showDebugRay = true;
    [Header("UI Elements")]
    public TextMeshProUGUI promptText;
    public GameObject promptUI; 
    private IInteraction currentTarget;
    private Camera playerCamera;

    void Start()
    {
        playerCamera = GetComponentInParent<Camera>();
        if (playerCamera == null)
            playerCamera = Camera.main;

        if (promptUI != null)
            promptUI.SetActive(false);
    }

    void Update()
    {
        Ray ray;
        if (playerCamera != null)
            ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        else
            ray = new Ray(transform.position, transform.forward);

        RaycastHit hit;
        Vector3 rayOrigin = playerCamera.transform.position;
        if (showDebugRay)
        {
            Debug.DrawRay(rayOrigin, ray.direction * rayDistance, Color.yellow);
        }

        if (Physics.Raycast(ray, out hit, rayDistance, interactLayer))
        {
            IInteraction interactable = hit.collider.GetComponent<IInteraction>();

            if (interactable != null)
            {
                currentTarget = interactable;
                string prompt = interactable.GetPrompt();

                if (promptText != null)
                    promptText.text = prompt;

                if (promptUI != null)
                    promptUI.SetActive(true);

                if (Keyboard.current != null && Keyboard.current.eKey.wasPressedThisFrame)
                {
                    interactable.Interact();

                    promptText.text = interactable.GetPrompt();
                }

                return;
            }
        }

        currentTarget = null;

        if (promptUI != null)
            promptUI.SetActive(false);
    }
}
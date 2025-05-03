
using UnityEngine;
using UnityEngine.Events;
using StarterAssets;

public class PauseAndInventoryManager : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject inventoryMenu;

    [Header("Other References")]
    [SerializeField] private PlayerAudio playerAudio;
    [SerializeField] private StarterAssetsInputs playerInputs;

    public bool IsPauseMenuOpen = false;
    private bool isInventoryMenuOpen = false;

    private void Awake()
    {
        if (playerAudio == null)
            playerAudio = FindObjectOfType<PlayerAudio>();

        if (playerInputs == null)
            playerInputs = FindObjectOfType<StarterAssetsInputs>();

        if (pauseMenu == null)
            pauseMenu = GameObject.Find("PauseMenu");

        if (inventoryMenu == null)
            inventoryMenu = GameObject.Find("InventoryUI");
    }

    private void Update()
    {
        HandlePauseMenu();
        HandleInventoryMenu();
    }
    // Resumes the game by closing the pause menu, resuming audio, and enabling player controls
    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;

        if (playerInputs != null)
        {
            playerInputs.cursorLocked = true;
            playerInputs.cursorInputForLook = true;
            playerInputs.SetCursorState(true);
            playerInputs.look = Vector2.zero;
            playerInputs.EnablePlayerMovement(true);
        }

        if (playerAudio != null)
        {
            playerAudio.audioSource.UnPause();
        }

        IsPauseMenuOpen = false;
    }
    // Handles opening and closing the pause menu when Escape key is pressed
    private void HandlePauseMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isInventoryMenuOpen)
            {
                CloseInventoryMenu();
                return;
            }

            IsPauseMenuOpen = !IsPauseMenuOpen;
            pauseMenu.SetActive(IsPauseMenuOpen);
            if (IsPauseMenuOpen && isInventoryMenuOpen)
            {
                CloseInventoryMenu();
            }

            playerInputs.look = Vector2.zero;
            playerInputs.EnablePlayerMovement(!IsPauseMenuOpen);
            playerInputs.SetCursorState(!IsPauseMenuOpen);
            playerInputs.cursorLocked = !IsPauseMenuOpen;
            playerInputs.cursorInputForLook = !IsPauseMenuOpen;
            Time.timeScale = IsPauseMenuOpen ? 0f : 1f;

            // Handle audio
            if (playerAudio != null)
            {
                if (IsPauseMenuOpen)
                    playerAudio.audioSource.Pause();
                else
                    playerAudio.audioSource.UnPause();
            }
        }
    }
    // Handles opening and closing the inventory UI when F key is pressed
    private void HandleInventoryMenu()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (IsPauseMenuOpen && !isInventoryMenuOpen)
                return;

            isInventoryMenuOpen = !isInventoryMenuOpen;
            inventoryMenu.SetActive(isInventoryMenuOpen);
            playerInputs.look = Vector2.zero;
            playerInputs.EnablePlayerMovement(!isInventoryMenuOpen);
            playerInputs.SetCursorState(!isInventoryMenuOpen);
            playerInputs.cursorLocked = !isInventoryMenuOpen;
            playerInputs.cursorInputForLook = !isInventoryMenuOpen;
        }
    }
    // Closes the inventory UI and enables player controls if the pause menu is not open
    private void CloseInventoryMenu()
    {
        isInventoryMenuOpen = false;
        inventoryMenu.SetActive(false);

        if (!IsPauseMenuOpen)
        {
            playerInputs.EnablePlayerMovement(true);
            playerInputs.SetCursorState(true);
            playerInputs.cursorLocked = true;
            playerInputs.cursorInputForLook = true;
        }
    }
}

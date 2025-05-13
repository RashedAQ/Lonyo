using StarterAssets;
using UnityEngine.SceneManagement;
using UnityEngine;
public class PauseMenu : MonoBehaviour
{
    public static GameObject activePauseMenu;
    [SerializeField] private StarterAssetsInputs playerInputSystem;
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private PlayerAudio audioManager;
    public static bool isGamePaused { get; private set; }
    private void Awake()
    {
        if (playerInputSystem == null)
            playerInputSystem = FindObjectOfType<StarterAssetsInputs>();
        if (audioManager == null)
            audioManager = FindObjectOfType<PlayerAudio>();
        activePauseMenu = pauseMenuUI;
        ResetGameState();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isGamePaused)
                ResumeGame();
            else
                PauseGame();
        }
    }


    // This method resets the game state when the game is resumed or initially started, including the time scale and input system.
    private void ResetGameState()
    {
        Time.timeScale = 1f;
        isGamePaused = false;
        if (pauseMenuUI != null)
            pauseMenuUI.SetActive(false);
        if (playerInputSystem != null)
        {
            playerInputSystem.EnablePlayerMovement(true);
            playerInputSystem.SetCursorState(true);
            playerInputSystem.cursorLocked = true;
            playerInputSystem.cursorInputForLook = true;
        }
    }

    // This method pauses the game, shows the pause menu, and disables player movement and audio.
    public void PauseGame()
    {
        if (pauseMenuUI == null) return;
        DataPersistenceManager.Instance?.SaveGame();
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isGamePaused = true;
        if (playerInputSystem != null)
        {
            playerInputSystem.cursorLocked = false;
            playerInputSystem.cursorInputForLook = false;
            playerInputSystem.SetCursorState(false);
            playerInputSystem.look = Vector2.zero;
            playerInputSystem.EnablePlayerMovement(false);
        }
        if (audioManager != null && audioManager.audioSource != null)
        {
            audioManager.audioSource.Pause();
        }
    }
    // This method resumes the game, hides the pause menu, and restores player movement and audio.
    public void ResumeGame()
    {
        if (pauseMenuUI == null) return;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isGamePaused = false;
        if (playerInputSystem != null)
        {
            playerInputSystem.cursorLocked = true;
            playerInputSystem.cursorInputForLook = true;
            playerInputSystem.SetCursorState(true);
            playerInputSystem.look = Vector2.zero;
            playerInputSystem.EnablePlayerMovement(true);
        }
        if (audioManager != null && audioManager.audioSource != null)
        {
            audioManager.audioSource.UnPause();
        }
    }
    // This method restarts the game, loading the first scene and resetting the game state.
    public void Restart()
    {
       
       DataPersistenceManager.Instance.NewGame();

  
        SceneManager.LoadSceneAsync(1);
        Time.timeScale = 1f;

     
        pauseMenuUI.SetActive(false);
    
        isGamePaused = false;

     
        if (playerInputSystem != null)
        {
            playerInputSystem.cursorLocked = true;
            playerInputSystem.cursorInputForLook = true;
            playerInputSystem.SetCursorState(true);
            playerInputSystem.EnablePlayerMovement(true);
        }
    }
    // This method takes the player to the main menu, saving the game and loading the main menu scene.

    public void GoToMainMenu()
    {
        DataPersistenceManager.Instance?.SaveGame();
        Time.timeScale = 1f;
        isGamePaused = false;
        SceneManager.LoadSceneAsync(0);
    }
    // This method is called when the pause menu is destroyed, ensuring it doesn't interfere with other instances.
    private void OnDestroy()
    {
        if (activePauseMenu == pauseMenuUI)
        {
            activePauseMenu = null;
        }
    }
}
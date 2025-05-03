using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public static MainMenu Instance;
    private void Awake()
    {
       
     

        Instance = this;
      
    }
    // Starts a new game by resetting data and loading the first scene
    public void PlayGame()
        
    {


        DataPersistenceManager.Instance.NewGame();
     
        SceneManager.LoadSceneAsync(1);
        Time.timeScale = 1f;
    }
    // Quits the game application
    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
    // Resumes the game from saved data if available
    public void OnClickResume()
    {
       
        if (!DataPersistenceManager.Instance.HasGameData())
        {
            Debug.LogWarning("No save data found - starting new game instead");
            PlayGame();
            return;
        }
        string sceneToLoad = DataPersistenceManager.Instance.GetCurrentSceneName();
        if (string.IsNullOrEmpty(sceneToLoad))
        {
            Debug.LogWarning("No saved scene found - starting new game");
            PlayGame();
            return;
        }
        Debug.Log($"Loading saved game at scene: {sceneToLoad}");
        SceneManager.LoadSceneAsync(sceneToLoad).completed += (op) =>
        {
            Time.timeScale = 1f;
        };
    }
}
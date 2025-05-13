using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;
using System;

public class DataPersistenceManager : MonoBehaviour
{
    [Header("File Storage Config")]
    [SerializeField] private string fileName = "savegame.json";

    private GameData gameData;
    private List<IDataPersistence> dataPersistenceObjects;
    private FileDataHandler dataHandler;

    public static DataPersistenceManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("Found more than one DataPersistenceManager in the scene. Destroying the newest one.");
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(this.gameObject);

        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);

        // Initialize dataPersistenceObjects right away
        this.dataPersistenceObjects = FindAllDataPersistenceObjects();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Refresh the list whenever a new scene loads
        this.dataPersistenceObjects = FindAllDataPersistenceObjects();

        // Only load game if we're not starting a new game
        if (gameData == null || !gameData._isNewGame)
        {
            LoadGame();
        }
    }

    public void NewGame()
    {
        this.gameData = new GameData();
        gameData._isNewGame = true;
        gameData._sceneName = SceneManager.GetActiveScene().name;

        // Initialize all objects with new game data
        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            if (dataPersistenceObj != null)
            {
                dataPersistenceObj.LoadData(gameData);
            }
        }
    }

    public void LoadGame()
    {
        this.gameData = dataHandler.Load();

        if (this.gameData == null)
        {
            Debug.Log("No data was found. Initializing data to defaults.");
            NewGame();
            return;
        }

        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            if (dataPersistenceObj != null)
            {
                dataPersistenceObj.LoadData(gameData);
            }
        }

        gameData._isNewGame = false; 
    }

    public void SaveGame()
    {
        if (this.gameData == null)
        {
            Debug.LogWarning("No data was found. A new game needs to be started before data can be saved.");
            return;
        }

       
        if (dataPersistenceObjects == null || dataPersistenceObjects.Count == 0)
        {
            dataPersistenceObjects = FindAllDataPersistenceObjects();
        }

        gameData._sceneName = SceneManager.GetActiveScene().name;
        gameData._isNewGame = false; 

        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            if (dataPersistenceObj != null)
            {
                dataPersistenceObj.SaveData(ref gameData);
            }
        }

        dataHandler.Save(gameData);
        Debug.Log($"Game saved to {Application.persistentDataPath}/{fileName}");
    }

    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        // Include inactive objects to catch everything
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>(true)
            .OfType<IDataPersistence>();

       

        return new List<IDataPersistence>(dataPersistenceObjects);
   /////////////////////     var saveObjects = FindObjectsOfType<IDataPersistence>();
    }

    public string GetCurrentSceneName()
    {
        return gameData?._sceneName ?? SceneManager.GetActiveScene().name;
    }

    public bool HasGameData()
    {
        return dataHandler.Load() != null;
    }
}
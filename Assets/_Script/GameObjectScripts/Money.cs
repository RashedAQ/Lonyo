using UnityEngine;

public class Money : MonoBehaviour, IDataPersistence
{
    private bool isMoneyCollected = false;
    [SerializeField] private string moneyID;
    public GameObject moneyVisual;
    [ContextMenu("Generate GUID id")]
      private void GenerateGuid()
      {
      moneyID = System.Guid.NewGuid().ToString();
      }
    private void Update()
    {
        if (isMoneyCollected == true)
        {
            moneyVisual.SetActive(false);
        }
    }
    // Loads the collected state of the money from saved game data
    public void LoadData(GameData data)
    {
        if (data._moneyCollected.TryGetValue(moneyID, out isMoneyCollected) && isMoneyCollected)
        {
            moneyVisual.SetActive(false);
        }
    }
    // Saves the collected state of the money into the game data
    public void SaveData(ref GameData data)
    {
        if (data._moneyCollected.ContainsKey(moneyID))
        {
            data._moneyCollected[moneyID] = isMoneyCollected;
        }
        else
        {
            data._moneyCollected.Add(moneyID, isMoneyCollected);
        }
    }
    // Triggered when another collider enters the money's trigger collider (typically the player). Collects the money.
    private void OnTriggerEnter(Collider other)
    {
        if (isMoneyCollected || !other.CompareTag("Player")) return;

        isMoneyCollected = true;
        FindObjectOfType<PlayerMoneyCollector>()?.AddMoney(1);
        gameObject.SetActive(false);

     
    }
    // Called automatically when the money object becomes visible by the camera
    private void OnBecameVisible()
    {
        MoneyManager.instance?.Register(this);
    }
    // Called automatically when the money object is no longer visible by the camera
    private void OnBecameInvisible()
    {
        MoneyManager.instance?.Unregister(this);
    }
}

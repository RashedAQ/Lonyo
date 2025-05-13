using UnityEngine;
using TMPro;

public class PlayerDeathCounter : MonoBehaviour, IDataPersistence
{
    public TextMeshProUGUI deathCountText;
    private int deathCount = 0;

    private void Start()
    {
        UpdateDeathText();
    }

    // This method loads the death count from the saved data and updates the UI.
    public void LoadData(GameData data)
    {
        this.deathCount = data._deathCount;
        Debug.Log($"Loaded death count: {deathCount}");
        UpdateDeathText();
    }

    // This method saves the current death count to the game data.
    public void SaveData(ref GameData data)
    {
        data._deathCount = this.deathCount;
        Debug.Log($"Saved death count: {deathCount}");
    }

    // This method triggers when an object collides with the tonic, increasing the death count.
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Tonic")
        {
            deathCount++;
            UpdateDeathText();
            DataPersistenceManager.Instance.SaveGame();
        }
    }

    // This method updates the UI text to show the current death count.
    private void UpdateDeathText()
    {
        if (deathCountText != null)
        {
            deathCountText.text = $"Deaths: {deathCount}";
        }
    }
}

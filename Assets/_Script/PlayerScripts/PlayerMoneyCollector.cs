using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerMoneyCollector : MonoBehaviour, IDataPersistence
{
    public TextMeshProUGUI moneyTextUI; 
    private int collectedMoney = 0;

    private void Start()
    {
     
        UpdateMoneyUI();
    }

    // This method adds the specified amount of money and updates the UI.
    public void AddMoney(int amount)
    {
        collectedMoney += amount;
        UpdateMoneyUI();
    }

    // This method updates the money display on the UI.
    private void UpdateMoneyUI()
    {
        if (moneyTextUI != null)
        {
            moneyTextUI.text = "Money : " + collectedMoney.ToString();
        }
    }

    // This method loads the money collected data from saved game data.
    public void LoadData(GameData data)
    {
        foreach (KeyValuePair<string, bool> moneyPair in data._moneyCollected)
        {
            if (moneyPair.Value)
            {
                collectedMoney++;
            }
        }
    }

    // This method saves the current money data into the game data.
    public void SaveData(ref GameData data)
    {
        data.totalMoney = this.collectedMoney;
    }
}

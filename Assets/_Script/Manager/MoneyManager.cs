using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    [SerializeField] private float RotationSpeed = 50f;
    private Quaternion rotation;
    private List<Money> moneys;
    #region Singleton
    public static MoneyManager instance;
    private void Awake()
    {
        instance = this;
        rotation = Quaternion.identity;
        moneys = new List<Money>();

    }
    #endregion
    private void Update()
    {
        rotation *= Quaternion.Euler(0f, RotationSpeed * Time.deltaTime, 0f);
        foreach (Money money in moneys)
        {
            money.transform.rotation = rotation;
         

        }
    }
    // Registers a Money object into the manager
    public void Register(Money money)
    {
        if (!moneys.Contains(money))
        {
            moneys.Add(money);
        }
    }
    // Unregisters a Money object from the manager
    public void Unregister(Money money)
    {

        if (moneys.Contains(money))
        {

            moneys.Remove(money);
        }
    }

}

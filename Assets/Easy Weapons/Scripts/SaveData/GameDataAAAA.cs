using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameDataAAA
{
    public string currentScene;
    public PlayerData playerData;
    public List<string> destroyedObjects = new List<string>();
    public WeaponData weaponData;
    public DateTime saveTime;

    public GameDataAAA(string sceneName)
    {
        currentScene = sceneName;
        saveTime = DateTime.Now;
    }
}

[System.Serializable]
public class PlayerData
{
    public Vector3 position;
    public Quaternion rotation;
    public float currentHealth;
    public float maxHealth;
}

[System.Serializable]
public class WeaponData
{
    public string weaponName;
    public Vector3 position;
    public Quaternion rotation;
    public bool isEquipped;
}
/// <summary>
/// Health.cs
/// Author: MutantGopher
/// This is a sample health script.  If you use a different script for health,
/// make sure that it is called "Health".  If it is not, you may need to edit code
/// referencing the Health component from other scripts
/// </summary>

using UnityEngine;
using System;

public class Health : MonoBehaviour, IDataPersistence
{
    [Header("Health Settings")]
    public bool canDie = true;
    public float startingHealth = 100.0f;
    public float maxHealth = 100.0f;
    private float currentHealth;

    [Header("Death Effects")]
    public bool replaceWhenDead = false;
    public GameObject deadReplacement;
    public bool makeExplosion = false;
    public GameObject explosion;

    [Header("Player Settings")]
    public bool isPlayer = false;
    public GameObject deathCam;

    [Header("Save System")]
    public string uniqueID;
    public bool dead = false;
    public GameObject healthGameObject;

    [ContextMenu("Generate GUID id")]
    private void GenerateGuid()
    {
        uniqueID = System.Guid.NewGuid().ToString();
    }
    private void Start()
    {
        currentHealth = startingHealth;

    }

    // Loads the death state from saved data
    public void LoadData(GameData data)
    {
        if (data._bojectDie.TryGetValue(uniqueID, out dead) && dead)
        {
            healthGameObject.SetActive(false);
        }
    }
    // Saves the current death state to the save data
    public void SaveData(ref GameData data)
    {
        if (data._bojectDie.ContainsKey(uniqueID))
        {
            data._bojectDie[uniqueID] = dead;
        }
        else
        {
            data._bojectDie.Add(uniqueID, dead);
        }
    }


    // Applies a change to the health value
    public void ChangeHealth(float amount)
    {
        currentHealth += amount;
        Debug.Log($"Health: {currentHealth}");

        if (currentHealth <= 0 && !dead && canDie)
        {
            Die();
        }
        else if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }
    // Handles the object's death logic
    public void Die()
    {
        if (dead) return;
        dead = true;

        if (replaceWhenDead && deadReplacement != null)
        {
            Instantiate(deadReplacement, transform.position, transform.rotation);
        }

        if (makeExplosion && explosion != null)
        {
            Instantiate(explosion, transform.position, transform.rotation);
        }

        if (isPlayer && deathCam != null)
        {
            deathCam.SetActive(true);
        }

      healthGameObject.SetActive(false);
    }


}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SlotTag { None, Head, Chest, Legs, Feet }

public class Inventory : MonoBehaviour
{
    public static Inventory Singleton;
    public static InventoryItem carriedItem;

    [Header("Inventory UI Slots")]
    [SerializeField] private InventorySlot[] inventorySlots;
    [SerializeField] private InventorySlot[] hotbarSlots;
    [SerializeField] private InventorySlot[] equipmentSlots;

    [Header("Item List")]
    [SerializeField] private Item[] items;  // List of available items
    [SerializeField] private InventoryItem itemPrefab;

    private List<string> inventoryItems = new List<string>();

    // UI management
    public static bool IsOpen { get; set; }
    public GameObject inventoryUI;

    void Awake()
    {
        Singleton = this;
    }

    // Method to add an item to the inventory
    public void AddItem(string itemID)
    {
        inventoryItems.Add(itemID);
        Debug.Log($"Item {itemID} added to inventory.");
    }

    // Method to retrieve the item IDs in the inventory
    public List<string> GetItemIDs()
    {
        return new List<string>(inventoryItems);
    }

    // Method to spawn an item in the inventory UI
    public void SpawnInventoryItem(Item item)
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            if (inventorySlots[i].myItem == null)  // Find an empty slot
            {
                InventoryItem newItem = Instantiate(itemPrefab, inventorySlots[i].transform);
                newItem.Initialize(item, inventorySlots[i]);
                break;
            }
        }
    }

    // Equip item to a specific slot
    public void EquipItem(SlotTag tag, InventoryItem item = null)
    {
        switch (tag)
        {
            case SlotTag.Head:
                Debug.Log(item == null ? "Unequipped Head gear" : $"Equipped {item.myItem.name} on Head");
                break;
            case SlotTag.Chest:
                Debug.Log(item == null ? "Unequipped Chest gear" : $"Equipped {item.myItem.name} on Chest");
                break;
            case SlotTag.Legs:
                Debug.Log(item == null ? "Unequipped Leg gear" : $"Equipped {item.myItem.name} on Legs");
                break;
            case SlotTag.Feet:
                Debug.Log(item == null ? "Unequipped Feet gear" : $"Equipped {item.myItem.name} on Feet");
                break;
        }
    }

    // Set the carried item (for dragging purposes)
    public void SetCarriedItem(InventoryItem item)
    {
        if (carriedItem != null)
        {
            carriedItem.canvasGroup.blocksRaycasts = true;
        }

        carriedItem = item;
        carriedItem.canvasGroup.blocksRaycasts = false;
        item.transform.SetParent(transform);  // Dragging item
    }
}

using UnityEngine;
using Unity.VisualScripting;

public class ItemEquipper : MonoBehaviour
{
    public Transform handTransform; // Assign the "Hand" position in Inspector
    public GameObject currentEquippedItem;

    private int selectedSlotIndex = -1; // Track the selected hotbar slot

    void Update()
    {
        // Hotbar number keys (1 to 9)
        for (int i = 1; i <= 9; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha0 + i))
            {
                EquipItemFromSlot(i - 1);
                selectedSlotIndex = i - 1;
                break;
            }
        }

        // Use currently equipped item on right-click
        if (Input.GetMouseButtonDown(1)) // Right-click
        {
            if (currentEquippedItem != null)
            {
                IUsable usable = currentEquippedItem.GetComponent<IUsable>();
                if (usable != null)
                {
                    usable.Use();

                    // Remove item from the hotbar UI slot after use
                    if (selectedSlotIndex >= 0 && selectedSlotIndex < InventorySystem.Instance.slotList.Count)
                    {
                        Transform slot = InventorySystem.Instance.slotList[selectedSlotIndex].transform;
                        if (slot.childCount > 0)
                        {
                            Destroy(slot.GetChild(0).gameObject); // Remove from UI
                        }
                    }

                    Destroy(currentEquippedItem); // Remove from hand
                    currentEquippedItem = null;
                    selectedSlotIndex = -1;
                }
            }
        }
    }

    void EquipItemFromSlot(int slotIndex)
    {
        if (InventorySystem.Instance.slotList.Count > slotIndex)
        {
            Transform slot = InventorySystem.Instance.slotList[slotIndex].transform;

            if (slot.childCount > 0)
            {
                GameObject itemPrefab = Resources.Load<GameObject>(InventorySystem.Instance.itemList[slotIndex]);

                if (itemPrefab != null)
                {
                    if (currentEquippedItem != null)
                    {
                        Destroy(currentEquippedItem); // Unequip previous
                    }

                    currentEquippedItem = Instantiate(itemPrefab, handTransform.position, handTransform.rotation);
                    currentEquippedItem.transform.SetParent(handTransform);
                    currentEquippedItem.transform.localPosition = Vector3.zero;
                    currentEquippedItem.transform.localRotation = Quaternion.identity;

                    Debug.Log("Equipped item: " + itemPrefab.name);
                }
                else
                {
                    Debug.LogWarning("Item prefab not found in Resources.");
                }
            }
            else
            {
                Debug.Log("No item in slot " + slotIndex);
            }
        }
    }
}

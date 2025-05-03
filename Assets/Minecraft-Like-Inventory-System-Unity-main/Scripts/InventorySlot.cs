using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IPointerClickHandler
{
    public InventoryItem myItem { get; set; }
    public SlotTag myTag;

    // Handles item interactions (e.g., equipping items)
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (Inventory.carriedItem == null) return;

            if (myTag != SlotTag.None && Inventory.carriedItem.myItem.itemTag != myTag) return;

            SetItem(Inventory.carriedItem);
        }
    }

    // Set the item to this slot
    public void SetItem(InventoryItem item)
    {
        if (item == null) return;

        Inventory.carriedItem = null;
        item.activeSlot.myItem = null;

        myItem = item;
        myItem.activeSlot = this;
        myItem.transform.SetParent(transform);
        myItem.canvasGroup.blocksRaycasts = true;

        if (myTag != SlotTag.None)
        {
            Inventory.Singleton.EquipItem(myTag, myItem);
        }
    }
}

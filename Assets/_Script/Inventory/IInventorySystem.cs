public interface IInventorySystem
{
    void AddToInventory(string itemName);
    bool CheckIfFull();
}
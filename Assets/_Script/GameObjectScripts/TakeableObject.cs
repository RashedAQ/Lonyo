using Unity.VisualScripting;
using UnityEngine;

public class TakeableObject : MonoBehaviour 
{

    public string itemName;
    public float pickupRange = 1.6f; 
    private Camera playerCamera;
    private void Start()
    {
        playerCamera = Camera.main;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, pickupRange))
            {
                if (hit.collider.gameObject == gameObject)
                {
                    if (!InventorySystem.Instance.CheckIfFull())
                    {
                        GoldenApple goldenApple = GetComponent<GoldenApple>();
                        if (goldenApple != null)
                        {
                            goldenApple.Use();
                            InventorySystem.Instance.AddToInventory(itemName);
                            Debug.Log("Golden apple used and added to inventory");
                        }
                        else
                        {
                            InventorySystem.Instance.AddToInventory(itemName);
                            Debug.Log("Item added to inventory");
                            Destroy(gameObject);
                        }
                    }
                    else
                    {
                        Debug.Log("Inventory is full");
                    }
                }
            }
        }
    }
}

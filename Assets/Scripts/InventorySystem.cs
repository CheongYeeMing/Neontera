using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySystem : MonoBehaviour
{
    [SerializeField] Inventory inventory;
    [Header("General Fields")]
    // List of items picked up
    //public List<GameObject> items = new List<GameObject>();
    // Flag indicates is the inventory is open or not
    public bool isOpen;
    [Header("UI Items Section")]
    // Inventory System Window
    public GameObject ui_Window;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleInventory();
        }
    }

    public void ToggleInventory()
    {
        isOpen = !isOpen;
        ui_Window.SetActive(isOpen);
    }

    // Add item to items list
    public void PickUp(GameObject item)
    {
        Item item2 = item.GetComponent<Item>();
        if (item2.itemType == Item.ItemType.Currency)
        {
            FindObjectOfType<CharacterWallet>().AddGold(1);
        }
        else
        {
            inventory.AddItem(item2);
        }
    }
}

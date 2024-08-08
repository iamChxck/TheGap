using System.Collections.Generic;
using UnityEngine;

public class InventoryPool : Singleton<InventoryPool>
{
    public List<GameObject> inventoryPool = new List<GameObject>();

    public Inventory inventory;

    private void Start()
    {
        isPersist = true;
        inventory = FindObjectOfType<Inventory>();
    }

    //Gives Item to player's inventory
    public void GiveItem()
    {
       
        //Simulates player getting item with spacebar
        int rand = Random.Range(0, 3);
        switch (rand)
        {
            case 0:
                inventoryPool[0].gameObject.GetComponent<Item>().descriptionText = "+ Attack";
                //player
                inventory.ObtainItem(inventoryPool[0]);
                break;
            case 1:
                inventoryPool[1].gameObject.GetComponent<Item>().descriptionText = "+ Health";
                inventory.ObtainItem(inventoryPool[1]);
                break;
            case 2:
                inventoryPool[2].gameObject.GetComponent<Item>().descriptionText = "+ Speed";
                inventory.ObtainItem(inventoryPool[2]);
                break;
        }
        Debug.Log("added item to inventory of player!");
        
    }
}

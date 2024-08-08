using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chore : MonoBehaviour
{
    DialogueManager dialogue;
    Inventory inventory;
    InventoryPool inventoryPool;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChoreCompletion()
    {
        dialogue = FindObjectOfType<DialogueManager>();
        inventory = FindObjectOfType<Inventory>();

        if (!dialogue.inventoryGuide)
        {
            StartCoroutine(dialogue.InventoryGuide());
        }

        if (!dialogue.firstChore && dialogue.playerData.second)
        {
            dialogue.firstChore = true;
            //CHORE DIALOG FOR THE FIRST CHORE DONE IN SECOND YEAR
            dialogue.FirstChoreDialogue();
        }
            
        else if(!dialogue.firstChore && dialogue.playerData.third)
        {
            dialogue.firstChore = true;
            //CHORE DIALOG FOR THE FIRST CHORE DONE IN THIRD YEAR
            dialogue.SecondChoreDialogue();
        }
            

        Debug.Log("Chore is done!");
        ItemRandomizer();
        Destroy(gameObject);
    }

    private void ItemRandomizer()
    {
        inventoryPool = FindObjectOfType<InventoryPool>();
        inventoryPool.GiveItem();

    }
}

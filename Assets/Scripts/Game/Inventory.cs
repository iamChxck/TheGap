using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public List<GameObject> items = new List<GameObject>();

    public bool isOpen;

    [Header("UI Items Section")]
    //public GameObject ui_Window;
    public Image[] items_images;

    [Header("UI Items Description")]
    public GameObject ui_Description_Window;
    public Image description_Image;
    public TMP_Text description_Title;
    public TMP_Text description_Item;
    public GameObject ui_Item_BG;
    public GameObject inventory_Window;

    private bool itemSelected;

    [SerializeField]
    PlayerData playerData;


    private void Start()
    {
        playerData = Resources.Load<PlayerData>("Prefabs/PlayerData");
        inventory_Window.transform.localScale = Vector3.zero;
        HideDescription();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleInventory();
        }
    }

    public void ObtainItem(GameObject item)
    {
        if(items.Count <= 16)
        {
            items.Add(item);
            UpdateUI();
            playerData.SaveItems(items);
        }
        else
        {
            Debug.Log("INVENTORY IS FULL");
        }
       
    }

    #region INVENTORY UI 
    IEnumerator InventoryToggle()
    {
        isOpen = !isOpen;

        if (isOpen == true)
        {
            InventoryExpand(inventory_Window);
            UpdateUI();
        }
            

        if (isOpen == false)
        {
            InventoryContract(inventory_Window);
            
            yield return new WaitForSeconds(0.2f);
        }

        inventory_Window.SetActive(isOpen);

    }

    void ToggleInventory()
    {
        StartCoroutine("InventoryToggle");
    }

  

    public void UpdateUI()
    {
        HideAll();
        for (int i = 0; i < items.Count; i++)
        {
            items_images[i].sprite = items[i].GetComponent<SpriteRenderer>().sprite;
            items_images[i].gameObject.SetActive(true);
        }
    }


    void HideAll() { foreach (var i in items_images) { i.gameObject.SetActive(false); } }


    //LeanTween Animation for inventory item description expanding
    public void UIItemBGExpand(GameObject bg)
    {
        LeanTween.scale(bg.GetComponent<RectTransform>(), new Vector3(2, 2.6f, 1), 0.5f);
        LeanTween.move(bg.GetComponent<RectTransform>(), new Vector3(-187, -25, 0), 0.5f);
    }

    //LeanTween Animation for inventory item description contracting
    public void UIItemBGContract(GameObject bg)
    {
        LeanTween.scale(bg.GetComponent<RectTransform>(), new Vector3(1.45f, 1.45f, 1.45f), 0.5f);
        LeanTween.move(bg.GetComponent<RectTransform>(), new Vector3(-187.641f, 14, 0), 0.5f);
    }

    public void InventoryExpand(GameObject inv)
    {
        LeanTween.scale(inv.GetComponent<RectTransform>(), Vector3.one, 0.2f);

    }


    public void InventoryContract(GameObject inv)
    {
        LeanTween.scale(inv.GetComponent<RectTransform>(), Vector3.zero, 0.2f);

    }

    IEnumerator ShowClick(int id)
    {
        UIItemBGExpand(ui_Item_BG);
        description_Image.sprite = items_images[id].sprite;
        description_Title.text = items[id].name;
        description_Item.text = items[id].gameObject.GetComponent<Item>().descriptionText;


        description_Image.gameObject.SetActive(true);


        yield return new WaitForSeconds(0.5f);

        description_Title.gameObject.SetActive(true);
        description_Item.gameObject.SetActive(true);

    }

    //Show description of clicked item
    public void ShowDescriptionClicked(int id)
    {
        StartCoroutine("ShowClick", id);
    }
    public void ShowDescriptionHovered(int id)
    {
        description_Image.sprite = items_images[id].sprite;
        description_Title.text = items[id].name;
        description_Item.text = items[id].gameObject.GetComponent<Item>().descriptionText;


        description_Image.gameObject.SetActive(true);
    }
    //hides item description when not hovering on any item in the inventory
    public void HideDescription()
    {
        UIItemBGContract(ui_Item_BG);
        description_Image.gameObject.SetActive(false);
        description_Title.gameObject.SetActive(false);
        description_Item.gameObject.SetActive(false);
    }
    #endregion
}

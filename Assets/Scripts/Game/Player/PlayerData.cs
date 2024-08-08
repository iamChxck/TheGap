using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/PlayerData")]
public class PlayerData : ScriptableObject
{
    public List<GameObject> items = new List<GameObject>();
    public bool isPersist,
                first,
                second,
                third,
                fourth,
                graduate;

    public void SaveItems(List<GameObject> itemlist)
    {
        if (!isPersist)
        {
            items.Clear();
            isPersist = true;
        }

        if (items.Capacity != itemlist.Capacity)
            items.Clear();

        foreach (GameObject item in itemlist)
        {
            items = new List<GameObject>(itemlist);
            Debug.Log("items saved");
        }

    }

    public void LoadItems(List<GameObject> itemlist)
    {

        itemlist.Clear();
        foreach (GameObject item in items)
        {
            itemlist.Add(item);
            Debug.Log("items loaded");
        }
    }

    public void ResetYears()
    {
        first = false;
        second = false;
        third = false;
        fourth = false;
        graduate = false;
    }
}

// Date   : 30.01.2016 10:09
// Project: VoodooGame

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour
{

    private int border_padding = 18;
    private int horizontal_padding = 3;
    private int vertical_padding = 3;
    private int gridMemberSize = 100;

    private List<Ingredient> items = new List<Ingredient>();

    [SerializeField]
    private InventoryItem inventoryItemPrefab;

    [SerializeField]
    private Transform inventoryParent;

    private int level = 0;

    [SerializeField]
    private int inventoryLimit = 9;

    public void AddItem(Ingredient itemToAdd)
    {
        if (items.Count >= inventoryLimit)
        {
            // inventory is full!
        }
        else
        {

            int factor = items.Count;
            if (factor > 2)
            {
                level = factor / 3;
                factor = factor % 3;
            }

            InventoryItem newItem = Instantiate(
                inventoryItemPrefab,
                new Vector3(
                    factor * gridMemberSize + factor * horizontal_padding + border_padding,
                    -(level * gridMemberSize + level * vertical_padding + border_padding),
                    0f
                ),
                Quaternion.identity) as InventoryItem;
            newItem.transform.SetParent(inventoryParent, false);
            newItem.Init(itemToAdd);
            items.Add(itemToAdd);
        }

    }

    public void ClearInventory()
    {
        // animate?
        items.Clear();
    }

    public List<Ingredient> GetInventoryContents()
    {
        return items;
    }
}

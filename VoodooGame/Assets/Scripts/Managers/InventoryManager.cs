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

    private List<InventoryItem> items = new List<InventoryItem>();

    [SerializeField]
    private InventoryItem inventoryItemPrefab;

    [SerializeField]
    private Transform inventoryParent;

    private int level = 0;

    [SerializeField]
    private int inventoryLimit = 9;

    private int running = 0;

    [SerializeField]
    private WorldDialog worldDialog;

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
            items.Add(newItem);
        }

    }


    public void AddItem(Ingredient itemToAdd, GameObject pickup, bool isButton = false)
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
            newItem.Init(itemToAdd, pickup);
            newItem.name = "item " + running++;
            items.Add(newItem);

            if (!isButton)
            {
                newItem.GetComponent<Button>().enabled = false;
            }
        }

    }

    public void Remove(InventoryItem ingredientToRemove)
    {
        items.Remove(ingredientToRemove);
        if (worldDialog != null && items.Count == 0)
        {
            worldDialog.Hide();
        }
    }

    public void Show()
    {
        foreach (Transform child in transform)
        {
            if (child.GetComponent<Image>() != null)
            {
                child.GetComponent<Image>().enabled = true;
            }
            else if (child.GetComponent<Text>() != null)
            {
                child.GetComponent<Text>().enabled = true;
            }
        }
    }

    public void Hide()
    {
        foreach (Transform child in transform)
        {
            if (child.GetComponent<Image>() != null)
            {
                child.GetComponent<Image>().enabled = false;
            }
            else if (child.GetComponent<Text>() != null)
            {
                child.GetComponent<Text>().enabled = false;
            }
        }
    }

    public void Remove(Ingredient ingredientToRemove)
    {
        for (int i = 0; i < items.Count; i++)
        {
            InventoryItem item = items[i];
            if (item.Ingredient == ingredientToRemove)
            {
                Remove(item);
                Destroy(item.gameObject);
            }
        }
    }

    public void ClearInventory()
    {
        // animate?
        for (int i = 0; i < items.Count; i++)
        {
            items[i].Kill();
        }
        items.Clear();
    }

    public List<InventoryItem> GetInventoryContents()
    {
        return items;
    }
}

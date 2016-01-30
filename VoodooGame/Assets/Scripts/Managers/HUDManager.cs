// Date   : 29.01.2016 23:25
// Project: VoodooGame

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class HUDManager : MonoBehaviour
{

    public static HUDManager main;

    [SerializeField]
    private Transform screenParent;
    [SerializeField]
    private Transform worldParent;

    [SerializeField]
    private InventoryManager inventoryManager;

    [SerializeField]
    [Range(10, 50)]
    private int poolSize = 10;

    [SerializeField]
    private Dialog dialogPrefab;

    [SerializeField]
    private WorldDialog worldDialogPrefab;

    private List<Dialog> dialogList = null;

    int num = 0;

    void Awake()
    {

        if (GameObject.FindGameObjectsWithTag("HUDManager").Length > 0)
        {
            Destroy(gameObject);
        }
        else
        {
            this.tag = "HUDManager";
            main = this;
        }

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {

    }

    void Update()
    {

    }

    public void ShowWorldDialog(string message, Vector3 position, WorldDialogType dialogType)
    {
        WorldDialog dialog = Instantiate<WorldDialog>(worldDialogPrefab) as WorldDialog;
        dialog.transform.position = position;
        dialog.Init(message, dialogType);
        dialog.gameObject.transform.SetParent(worldParent, false);
    }

    public void ShowDialog(string message, Vector3 position, DialogType dialogType)
    {
        Dialog dialog = GetDialog();
        dialog.gameObject.SetActive(true);
        dialog.Show(message, position, transform, dialogType);
    }

    private void InitDialogPool(int size, Dialog dialogPrefab)
    {
        dialogList = new List<Dialog>();
        for (int i = 0; i < size; i++)
        {
            Dialog dialog = Instantiate(dialogPrefab);
            dialog.gameObject.transform.SetParent(screenParent, false);
            dialogList.Add(dialog);
        }
    }

    public Dialog GetDialog()
    {
        if (dialogList.Count > 0)
        {
            Dialog dialog = dialogList[0];
            dialogList.RemoveAt(0);
            return dialog;
        }
        return null;
    }

    public void DestroyDialog(Dialog dialog)
    {
        dialogList.Add(dialog);
    }

    public void ClearPool()
    {
        for (int i = dialogList.Count - 1; i < 0; i--)
        {
            dialogList[i].Clear();
        }
        foreach (Transform child in screenParent)
        {
            print(child.name);
            if (child.GetComponent<Dialog>() != null)
            {

                child.GetComponent<Dialog>().Clear();
            }
        }
        foreach (Transform child in worldParent)
        {
            if (child.GetComponent<Dialog>() != null)
            {
                child.GetComponent<Dialog>().Clear();
            }
        }
    }

    public void AddToInventory(Ingredient ingredient)
    {
        inventoryManager.AddItem(ingredient);
    }

    public List<Ingredient> GetInventoryContents()
    {
        return inventoryManager.GetInventoryContents();
    }
}
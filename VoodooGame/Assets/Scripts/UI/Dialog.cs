// Date   : 30.01.2016 01:05
// Project: VoodooGame

using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public enum DialogType { SimpleDialog }

public class Dialog : MonoBehaviour {

    [SerializeField]
    private Text txtComponent;

    [SerializeField]
    private Animator animator;

    public void Clear()
    {
        HUDManager.main.DestroyDialog(this);
        txtComponent.text = "";
        transform.position = Vector3.zero;
        gameObject.SetActive(false);
    }

    public void FinishedHide()
    {

    }

    public void Show(string message, Vector3 position, Transform parent, DialogType dialogType)
    {
        txtComponent.text = message;

        animator.SetTrigger("Show");
    }

    public void Hide()
    {
        animator.SetTrigger("Hide");
    }

}

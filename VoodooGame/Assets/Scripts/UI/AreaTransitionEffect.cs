// Date   : 31.01.2016 01:14
// Project: VoodooGame

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AreaTransitionEffect : MonoBehaviour {

    [SerializeField]
    private Text txtComponent;
    [SerializeField]
    private Color colorVariable;
    [SerializeField]
    private Image imgComponent;

    [SerializeField]
    private Animator animator;

    void Start () {
    
    }

    void Update () {
    
    }

    public void Show()
    {
        animator.SetTrigger("Show");
        GameManager.main.Player.GetComponent<PCMovement>().Stop();
        //Time.timeScale = 0f;
    }

    public void Hide()
    {
        animator.SetTrigger("Hide");
    }

    public void AfterShow()
    {
        GameManager.main.OpenNextArea();
        
        //Time.timeScale = 1.0f;
        Hide();
    }

}

using UnityEngine;
using UnityEngine.UI;

public class WeaponWheelButtonController : MonoBehaviour
{

    public int ID;
    private Animator anim;
    public Image selectedItem;
    private bool selected = false;
    private bool hovered = false;
    public Sprite icon;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hovered)
        {
            selectedItem.sprite = icon;
            Debug.Log("hover");
        }

        if (selected)
        {
            // selectedItem.sprite = icon;
            Debug.Log("selected item, something happen");
            selected = false;
        }
    }

    public void Selected()
    {
        selected = true;
        WeaponWheelController.weaponID = ID;
    }
    
    public void Deselected()
    {
        selected = false;
        WeaponWheelController.weaponID = 0;
    }

    public void HoverEnter()
    {
        anim.SetBool("Hover", true);
        hovered = true;
        // WeaponWheelController.weaponID = ID;
    }

    public void HoverExit()
    {
        anim.SetBool("Hover", false);
        hovered = false;
        // WeaponWheelController.weaponID = 0;
    }
}

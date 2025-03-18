using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEngine.UI;

public class WeaponWheelController : MonoBehaviour
{
    public Animator anim;
    private bool weaponWheelSelected = false;
    public Image selectedItem;
    public Sprite noImage;
    public static int weaponID;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            weaponWheelSelected = !weaponWheelSelected;
            Debug.Log("wheel toggled");
        }

        if (weaponWheelSelected)
        {
            anim.SetBool("OpenWeaponWheel", true);
        }
        else
        {
            anim.SetBool("OpenWeaponWheel", false);
        }

        switch (weaponID)
        {
            case 0:
                // selectedItem.sprite = noImage;
                break;

            case 1:
                Debug.Log("purple");
                break;
            
            case 2:
                Debug.Log("blue short");
                break;

            case 3:
                Debug.Log("blue poofy");
                break;

            case 4:
                Debug.Log("red");
                break;
        }
    }
}

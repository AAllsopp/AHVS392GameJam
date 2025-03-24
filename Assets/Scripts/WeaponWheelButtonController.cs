using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WeaponWheelButtonController : MonoBehaviour
{

    public int ID;
    private Animator anim;
    public Image selectedItem;
    private bool selected = false;
    // private bool hovered = false;
    public Sprite selectIcon;
    public Sprite noImage;
    private Coroutine fadeCoroutine;
    private enum FadeState { None, FadingIn, FadingOut }
    private FadeState currentFadeState;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
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
        currentFadeState = FadeState.FadingIn;
        Debug.Log("enter");
        StartFade(selectIcon, true);
        // selectedItem.sprite = selectIcon;
        // hovered = true;
        // WeaponWheelController.weaponID = ID;
    }

    public void HoverExit()
    {
        anim.SetBool("Hover", false);
        currentFadeState = FadeState.FadingOut;
        Debug.Log("exit");
        // selectedItem.sprite = noImage;
        StartFade(noImage, false);
        // hovered = false;
        // WeaponWheelController.weaponID = 0;
    }

    private void StartFade(Sprite targetSprite, bool fadeIn)
    {
        if (fadeCoroutine != null)
            StopCoroutine(fadeCoroutine);
            fadeCoroutine = null;


        if (fadeIn)
        {
            // StopAllCoroutines();
            fadeCoroutine = StartCoroutine(FadeInAfterFadeOut(selectedItem, targetSprite));
        }
        else
        {
            // StopAllCoroutines();
            fadeCoroutine = StartCoroutine(FadeOut(selectedItem));
        }
    }

    private IEnumerator FadeInAfterFadeOut(Image image, Sprite newSprite)
    {
        // Wait for fade out to finish
        while (currentFadeState == FadeState.FadingOut)
        {
            yield return null;
        }

        // Now start fading in
        currentFadeState = FadeState.FadingIn;
        yield return StartCoroutine(FadeIn(image, newSprite));
    }


    private IEnumerator FadeIn(Image image, Sprite newSprite, float duration = 0.1f)
    {
        Debug.Log("start fade in");
        image.sprite = newSprite;
        image.color = new Color(1f, 1f, 1f, 0f);

        float elapsed = 0f;
        while (elapsed < duration)
        {
            Debug.Log("fading in");
            elapsed += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsed / duration);
            image.color = new Color(1f, 1f, 1f, alpha);
            yield return null;
        }

        image.color = new Color(1f, 1f, 1f, 1f);
        fadeCoroutine = null;
        currentFadeState = FadeState.None;
        Debug.Log("finished fade in");
    }



    private IEnumerator FadeOut(Image image, float duration = 0.1f)
    {
        Debug.Log("start fade out");
        float elapsed = 0f;

        Color startColor = image.color;
        float startAlpha = image.color.a;

        while (elapsed < duration)
        {
            if (currentFadeState != FadeState.FadingOut)
            {
                Debug.Log("fade out interrupted");
                yield break;
            }

            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, 0f, elapsed / duration);
            image.color = new Color(startColor.r, startColor.g, startColor.b, alpha);
            yield return null;
        }

        image.color = new Color(startColor.r, startColor.g, startColor.b, 0f);
        fadeCoroutine = null;
        currentFadeState = FadeState.None;
        Debug.Log("finished fade out");
    }



}


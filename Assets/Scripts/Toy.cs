using UnityEngine;
using UnityEngine.Rendering.Universal;
using System.Collections.Generic;
using System.Linq;

public class script : MonoBehaviour
{

    public bool IsFemaleToy = true;
    public float GlowIntensity = 0.8f;
    public bool GlowOn = false;
    Light2D glow;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private static List<Light2D> maleToyGlows = new List<Light2D>();
    private static List<Light2D> femaleToyGlows = new List<Light2D>();

    void Start()
    {
        glow = GetComponentInChildren<Light2D>();
        glow.intensity = GlowIntensity;
        glow.enabled = GlowOn;
        glow.color = IsFemaleToy ? Color.magenta : Color.blue;

        (IsFemaleToy ? femaleToyGlows : maleToyGlows).Add(glow);
        // if (glow != null)
        // {
        //     Debug.Log("Light2D found: " + glow.name);
        // }
        // else
        // {
        //     Debug.LogError("No Light2D found in children!");
        // }
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Debug.Log("object entered: " + collision.gameObject.name);
        if (collision.gameObject.CompareTag("Player")) {
            glow.enabled = !glow.enabled;
            if (IsFemaleToy) {
                DisableMaleToyLights();
            }
            if (maleToyGlows.All(light => light.enabled) && femaleToyGlows.All(light => !light.enabled)) {
                Debug.Log("You win!");
            }
        }
    }


    private void DisableMaleToyLights()
    {
        foreach (Light2D maleGlow in maleToyGlows) {
            maleGlow.enabled = false;
        }

        Debug.Log("All male toy lights disabled!");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

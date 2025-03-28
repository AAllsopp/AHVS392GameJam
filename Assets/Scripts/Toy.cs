using UnityEngine;
using UnityEngine.Rendering.Universal;
using System.Collections.Generic;
using System.Linq;

public class script : MonoBehaviour
{

    public bool IsMaleToy = true;
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
        glow.color = IsMaleToy ? Color.blue : Color.magenta;

        (IsMaleToy ? maleToyGlows : femaleToyGlows).Add(glow);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Debug.Log("object entered: " + collision.gameObject.name);
        if (collision.gameObject.CompareTag("Player")) {
            glow.enabled = !glow.enabled;
            if (IsMaleToy) {
                DisableFemaleToyLights();
            }
            if (maleToyGlows.All(light => !light.enabled) && femaleToyGlows.All(light => light.enabled)) {
                Debug.Log("You win!");
            }
        }
    }


    private void DisableFemaleToyLights()
    {
        foreach (Light2D femaleGlow in femaleToyGlows) {
            femaleGlow.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

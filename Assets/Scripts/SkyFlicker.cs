using UnityEngine;
using UnityEngine.Rendering.Universal; // Required for Light2D

public class SkyFlicker : MonoBehaviour
{
    private Light2D light2D;
    public float flickerSpeed = 5f; // Speed of flicker
    public float minFalloff = 0.3f; // Minimum falloff intensity
    public float maxFalloff = 1.5f; // Maximum falloff intensity

    void Start()
    {
        light2D = GetComponent<Light2D>();

        if (light2D == null)
        {
            Debug.LogError("No Light2D component found on " + gameObject.name);
        }
    }

    void Update()
    {
        if (light2D != null)
        {
            // Smooth flickering using Perlin Noise
            float flickerValue = Mathf.PerlinNoise(Time.time * flickerSpeed, 0f);
            light2D.falloffIntensity = Mathf.Lerp(minFalloff, maxFalloff, flickerValue);
        }
    }
}

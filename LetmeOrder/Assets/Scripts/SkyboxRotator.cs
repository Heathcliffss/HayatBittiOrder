using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

public class SkyboxRotator : MonoBehaviour
{
    public Volume skyVolume;
    public float rotationSpeed = 1f;

    private HDRISky hdriSky;

    void Start()
    {
        if (skyVolume != null && skyVolume.profile.TryGet(out hdriSky))
        {
            // Baþlangýç ayarý yapýlabilir
        }
        else
        {
            Debug.LogError("HDRI Sky bulunamadý! Volume veya HDRISky eksik olabilir.");
        }
    }

    void Update()
    {
        if (hdriSky != null)
        {
            hdriSky.rotation.value += rotationSpeed * Time.deltaTime;
            // 360 üstüne çýkmasýn
            if (hdriSky.rotation.value > 360f)
                hdriSky.rotation.value -= 360f;
        }
    }
}
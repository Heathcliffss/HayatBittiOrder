using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

public class filmgrainbooster : MonoBehaviour
{
  public Volume globalVolume;
  
    public float hedefIntensity = 1f;
    public float hiz = 0.5f;

    private FilmGrain filmGrain;
    private bool aktif = false;

    void Start()
    {
        if (globalVolume != null && globalVolume.profile != null)
        {
            if (globalVolume.profile.TryGet<FilmGrain>(out filmGrain))
            {
                filmGrain.active = true;
            }
            else
            {
                Debug.LogWarning("FilmGrain bileşeni Volume profiline eklenmemiş.");
            }
        }
    }

    void Update()
    {
        if (aktif && filmGrain != null)
        {
            filmGrain.intensity.value = Mathf.MoveTowards(filmGrain.intensity.value, hedefIntensity, hiz * Time.deltaTime);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            aktif = true;
        }
    }
}

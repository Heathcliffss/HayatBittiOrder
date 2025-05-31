using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

public class parkurpostproses : MonoBehaviour
{
   public Volume globalVolume1;

    private FilmGrain filmGrain;
    private Exposure exposure;

    public float exposureBaslangic = 10f;
    public float exposureHedef = 0f;
    public float grainBaslangic = 1f;
    public float grainHedef = 0.1f;
    public float gecisHizi = 1f;

    void Start()
    {
        if (globalVolume1 != null && globalVolume1.profile != null)
        {
            // FilmGrain ve Exposure bileşenlerine eriş
            globalVolume1.profile.TryGet(out filmGrain);
            globalVolume1.profile.TryGet(out exposure);

            if (exposure != null)
                exposure.fixedExposure.value = exposureBaslangic;

            if (filmGrain != null)
                filmGrain.intensity.value = grainBaslangic;
        }
    }

    void Update()
    {
        if (exposure != null)
        {
            exposure.fixedExposure.value = Mathf.MoveTowards(
                exposure.fixedExposure.value,
                exposureHedef,
                gecisHizi * Time.deltaTime
            );
        }

        if (filmGrain != null)
        {
            filmGrain.intensity.value = Mathf.MoveTowards(
                filmGrain.intensity.value,
                grainHedef,
                gecisHizi * Time.deltaTime
            );
        }
    }
}

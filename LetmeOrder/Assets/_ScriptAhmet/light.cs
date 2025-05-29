using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class light : MonoBehaviour
{
    public Light sahneIsigi;                // Sahnedeki ýþýk
    public int kacKezFlesYapsin = 5;       // Kaç kere yanýp sönecek
    public float acikSuresi = 0.2f;        // Iþýk açýk kalma süresi
    public float kapaliSuresi = 0.2f;      // Iþýk kapalý kalma süresi

    public yaratýk yaratigiBaslat;         // Yaratýk script referansý

    void Start()
    {
        StartCoroutine(IsikYanipSonme());
    }

    IEnumerator IsikYanipSonme()
    {
        for (int i = 0; i < kacKezFlesYapsin; i++)
        {
            sahneIsigi.enabled = true;
            yield return new WaitForSeconds(acikSuresi);

            sahneIsigi.enabled = false;
            yield return new WaitForSeconds(kapaliSuresi);
        }

        // Sonunda ýþýk açýk kalsýn
        sahneIsigi.enabled = true;

        // Kovalamayý baþlat
        if (yaratigiBaslat != null)
        {
            yaratigiBaslat.kovalamayaBaslasin = true;
        }
    }
}

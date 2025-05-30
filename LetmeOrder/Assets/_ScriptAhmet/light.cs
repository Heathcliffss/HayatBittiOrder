using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class light : MonoBehaviour
{
    public Light isik; // Sahnedeki ýþýk
    public int kacKezFlesYapsin = 5;
    public float acikSuresi = 0.2f;
    public float kapaliSuresi = 0.2f;

    public Yaratik yaratigiBaslat; // Yaratýk scriptini buraya atayacaðýz

    void Start()
    {
        StartCoroutine(IsikYanipSonme());
    }

    System.Collections.IEnumerator IsikYanipSonme()
    {
        for (int i = 0; i < kacKezFlesYapsin; i++)
        {
            isik.enabled = true;
            yield return new WaitForSeconds(acikSuresi);

            isik.enabled = false;
            yield return new WaitForSeconds(kapaliSuresi);
        }

        // Iþýk kalýcý olarak açýlsýn
        isik.enabled = true;

        // Yaratýk kovalamaya baþlasýn
        if (yaratigiBaslat != null)
        {
            yaratigiBaslat.kovalamayaBaslasin = true;
        }
    }
}

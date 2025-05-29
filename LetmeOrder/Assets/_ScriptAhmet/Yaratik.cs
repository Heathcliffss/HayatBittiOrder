using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class yaratık : MonoBehaviour
{
    public Transform oyuncu;               // Oyuncu objesi
    public float hareketHizi = 4f;         // Yaratığın hareket hızı
    public float kovalamayaBaslamaMesafesi = 10f; // Oyuncuya olan mesafe bu değerden küçükse kovalamaya başlar

    [HideInInspector]
    public bool kovalamayaBaslasin = false; // Işık tarafından aktif edilir

    void Update()
    {
        if (kovalamayaBaslasin && oyuncu != null)
        {
            float mesafe = Vector3.Distance(transform.position, oyuncu.position);

            // Eğer mesafe yeterince yakınsa hareket et
            if (mesafe < kovalamayaBaslamaMesafesi)
            {
                Vector3 yon = (oyuncu.position - transform.position).normalized;
                transform.position += yon * hareketHizi * Time.deltaTime;
                transform.LookAt(oyuncu);
            }
        }
    }
}


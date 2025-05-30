using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Yaratik : MonoBehaviour
{
    public Transform oyuncu; // Takip edilecek oyuncu
    public float hiz = 4f; // Hareket hızı
    public float takipMesafesi = 15f; // Takip etmeye başlama mesafesi
    public float durmaMesafesi = 5f; // Yaratığın duracağı mesafe
    public Light ortamIsigi; // Işık objesi
    public bool kovalamayaBaslasin = false; // Kovalamaya başlama durumu

    void Update()
    {
        if (!kovalamayaBaslasin || ortamIsigi == null || !ortamIsigi.enabled || oyuncu == null)
            return;

        float mesafe = Vector3.Distance(transform.position, oyuncu.position);

        if (mesafe <= takipMesafesi && mesafe > durmaMesafesi)
        {
            Vector3 yon = (oyuncu.position - transform.position).normalized;
            Vector3 yeniPozisyon = transform.position + yon * hiz * Time.deltaTime;

            // Yaratığın yeni pozisyonu oyuncuya çok yaklaşmasın diye sınırla
            float yeniMesafe = Vector3.Distance(yeniPozisyon, oyuncu.position);
            if (yeniMesafe < durmaMesafesi)
            {
                // Yeni pozisyon oyuncuya çok yakınsa durma mesafesine göre pozisyon ayarla
                yeniPozisyon = oyuncu.position - yon * durmaMesafesi;
            }

            transform.position = yeniPozisyon;
            transform.LookAt(oyuncu);
        }
        else if (mesafe <= durmaMesafesi)
        {
            // Çok yaklaştıysa sadece oyuncuya bak
            transform.LookAt(oyuncu);
        }
    }
}


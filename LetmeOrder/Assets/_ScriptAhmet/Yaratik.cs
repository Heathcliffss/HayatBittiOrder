using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
            // MoveTowards ile kontrollü şekilde yaklaş
            transform.position = Vector3.MoveTowards(
                transform.position,
                oyuncu.position,
                hiz * Time.deltaTime
            );

            transform.LookAt(oyuncu);
        }
        else if (mesafe <= durmaMesafesi)
        {
            // Çok yaklaştıysa sadece oyuncuya bak
            transform.LookAt(oyuncu);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Aktif sahneyi yeniden yükle
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}

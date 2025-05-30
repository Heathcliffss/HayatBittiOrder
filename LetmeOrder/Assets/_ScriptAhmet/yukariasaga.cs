using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class yukariasaga : MonoBehaviour
{
    [Header("Hareket Ayarlarý")]
    public float yukseklik = 0.2f;    // Dalga yüksekliði (ne kadar yukarý-aþaðý gitsin)
    public float hiz = 0.8f;          // Dalga hýzý (ne kadar hýzlý hareket etsin)

    private Vector3 baslangicPozisyonu;
    private float faz;

    void Start()
    {
        baslangicPozisyonu = transform.position;
        faz = Random.Range(0f, Mathf.PI * 2); // Her obje farklý fazla baþlasýn
    }

    void Update()
    {
        float yeniY = Mathf.Sin(Time.time * hiz + faz) * yukseklik;
        transform.position = baslangicPozisyonu + new Vector3(0f, yeniY, 0f);
    }
}


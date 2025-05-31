using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tvkontrol : MonoBehaviour
{
    public GameObject pilObjesi;
    public GameObject kumandaObjesi;
    public GameObject tvObjesi;
    public AudioSource tvSes;
    public GameObject sonrakiOlayObjesi;

    public NesneYokOlma nesneYokOlmaScripti; // BURASI YENİ

    private bool pilAlindi = false;
    private bool kumandaAlindi = false;
    private bool tvAcildi = false;

    void Update()
    {
        if (Vector3.Distance(pilObjesi.transform.position, PlayerPozisyonu()) < 2f && Input.GetKeyDown(KeyCode.E))
        {
            pilAlindi = true;
            pilObjesi.SetActive(false);
        }

        if (pilAlindi && !kumandaAlindi &&
            Vector3.Distance(kumandaObjesi.transform.position, PlayerPozisyonu()) < 2f && Input.GetKeyDown(KeyCode.E))
        {
            kumandaAlindi = true;
            kumandaObjesi.SetActive(false);
        }

        if (kumandaAlindi && !tvAcildi && Input.GetKeyDown(KeyCode.H))
        {
            tvAcildi = true;
            tvObjesi.SetActive(true);
            if (tvSes != null) tvSes.Play();
            Invoke("TVSonrasiOlay", 10f); // 10 saniye sonra geçiş olacak
        }
    }

    void TVSonrasiOlay()
    {
        // Olayı tetikle
        if (nesneYokOlmaScripti != null)
        {
            StartCoroutine(nesneYokOlmaScripti.OdaDegisimiRutini()); // 🔥 BURASI ÇAĞRI
        }
    }

    Vector3 PlayerPozisyonu()
    {
        return Camera.main.transform.position;
    }
}

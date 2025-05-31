using UnityEngine;
using UnityEngine.Video;

public class Tvkontrol : MonoBehaviour
{
    public GameObject tv;
    public GameObject pilObjesi;
    public GameObject kumandaObjesi;
    public GameObject tvObjesi; // tv (1)
    public GameObject sonrakiOlayObjesi;

    public NesneYokOlma nesneYokOlmaScripti;

    private VideoPlayer tvVideo;
    private bool pilAlindi = false;
    private bool kumandaAlindi = false;
    private bool tvAcildi = false;

    void Start()
    {
        tv.SetActive(false);
        tvVideo = tvObjesi.GetComponentInChildren<VideoPlayer>();

        if (tvVideo != null)
        {
            tvVideo.Stop(); // Başlangıçta durdur
        }


    }

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

            if (tvVideo != null)
            {
                tv.SetActive(true);
                tvVideo.Play(); // 🎬 Video başlat
            }



            Invoke("TVSonrasiOlay", 12f); // 10 saniye sonra olay başlat
        }
    }

    void TVSonrasiOlay()
    {
        if (nesneYokOlmaScripti != null)
        {
            StartCoroutine(nesneYokOlmaScripti.OdaDegisimiRutini());
        }
    }

    Vector3 PlayerPozisyonu()
    {
        return Camera.main.transform.position;
    }
}

using UnityEngine;
using UnityEngine.Video;

public class Tvkontrol : MonoBehaviour
{
    public GameObject tv;
    public GameObject pilObjesi;
    public GameObject kumandaObjesi;
    public GameObject tvObjesi; // TV (1)
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
        Vector3 playerPos = PlayerPozisyonu();

        // Pil alma (menzil 4f)
        if (Vector3.Distance(pilObjesi.transform.position, playerPos) < 4f && Input.GetKeyDown(KeyCode.E))
        {
            pilAlindi = true;
            pilObjesi.SetActive(false);
        }

        // Kumanda alma (menzil 4f)
        if (pilAlindi && !kumandaAlindi &&
            Vector3.Distance(kumandaObjesi.transform.position, playerPos) < 4f && Input.GetKeyDown(KeyCode.E))
        {
            kumandaAlindi = true;
            kumandaObjesi.SetActive(false);
        }

        // TV açma: kumanda alındı, TV kapalı, E tuşu ve TV’ye bakılıyor
        if (kumandaAlindi && !tvAcildi && Input.GetKeyDown(KeyCode.E))
        {
            if (BakiyorMu(tvObjesi.transform, Camera.main.transform, 30f))  // 30 derece açı eşiği
            {
                tvAcildi = true;

                if (tvVideo != null)
                {
                    tv.SetActive(true);
                    tvVideo.Play(); // 🎬 Video başlat
                }

                Invoke("TVSonrasiOlay", 12f);
            }
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

    bool BakiyorMu(Transform hedef, Transform bakisNoktasi, float maxAci)
    {
        Vector3 yon = (hedef.position - bakisNoktasi.position).normalized;
        float aci = Vector3.Angle(bakisNoktasi.forward, yon);
        return aci <= maxAci;
    }
}

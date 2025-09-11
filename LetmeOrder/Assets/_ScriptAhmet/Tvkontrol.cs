using UnityEngine;
using UnityEngine.Video;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;


public class TvKontrol : MonoBehaviour
{
    public GameObject tv;
    public GameObject pilObjesi;
    public GameObject kumandaObjesi;
    public GameObject tvObjesi;
    public GameObject sonrakiOlayObjesi;

    public NesneYokOlma nesneYokOlmaScripti;

    private VideoPlayer tvVideo;
    private bool pilTakildi = false;
    private bool kumandaAlindi = false;
    private bool tvAcildi = false;

    // XR için etkileşim
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable pilGrab;
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable kumandaGrab;

    void Start()
    {
        tv.SetActive(false);
        tvVideo = tvObjesi.GetComponentInChildren<VideoPlayer>();

        if (tvVideo != null)
        {
            tvVideo.Stop();
        }

        pilGrab = pilObjesi.GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
        kumandaGrab = kumandaObjesi.GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();

        // Pil bırakıldığında kumandaya yerleştirilmiş mi kontrol et
        if (pilGrab != null)
        {
            pilGrab.selectExited.AddListener(OnPilBırakıldı);
        }

        if (kumandaGrab != null)
        {
            kumandaGrab.selectEntered.AddListener(OnKumandaAlindi);
        }
    }

    private void OnDestroy()
    {
        // Event temizliği
        if (pilGrab != null)
            pilGrab.selectExited.RemoveListener(OnPilBırakıldı);

        if (kumandaGrab != null)
            kumandaGrab.selectEntered.RemoveListener(OnKumandaAlindi);
    }

    private void OnPilBırakıldı(SelectExitEventArgs args)
    {
        // Eğer pil kumandanın trigger alanına girerse pil takıldı say
        if (Vector3.Distance(pilObjesi.transform.position, kumandaObjesi.transform.position) < 0.3f)
        {
            pilTakildi = true;
            pilObjesi.SetActive(false); // pili yok et
        }
    }

    private void OnKumandaAlindi(SelectEnterEventArgs args)
    {
        if (pilTakildi && !kumandaAlindi)
        {
            kumandaAlindi = true;
        }
    }

    void Update()
    {
        if (kumandaAlindi && !tvAcildi && GripTusunaBasildiMi())
        {
            if (BakiyorMu(tvObjesi.transform, Camera.main.transform, 30f))
            {
                tvAcildi = true;

                if (tvVideo != null)
                {
                    tv.SetActive(true);
                    tvVideo.Play();
                }

                Invoke("TVSonrasiOlay", 12f);
            }
        }
    }

    bool GripTusunaBasildiMi()
    {
        // Her iki el için XR controller input'ları kontrol edilir
        return InputHelpers.IsPressed(InputDevices.GetDeviceAtXRNode(XRNode.RightHand), InputHelpers.Button.Grip, out bool gripBasili, 0.1f) && gripBasili;
    }

    void TVSonrasiOlay()
    {
        if (nesneYokOlmaScripti != null)
        {
            StartCoroutine(nesneYokOlmaScripti.OdaDegisimiRutini());
        }
    }

    bool BakiyorMu(Transform hedef, Transform bakisNoktasi, float maxAci)
    {
        Vector3 yon = (hedef.position - bakisNoktasi.position).normalized;
        float aci = Vector3.Angle(bakisNoktasi.forward, yon);
        return aci <= maxAci;
    }
}

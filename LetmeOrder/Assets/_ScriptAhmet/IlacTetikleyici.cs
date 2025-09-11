using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class IlacAlmaVR : MonoBehaviour
{
    [Header("Havada süzülen nesneler")]
    public List<GameObject> havada;  // Havada süzülen nesneler listesi

    [Header("Sahne objeleri")]
    public GameObject engel;
    public GameObject merdivenfly;
    public GameObject merdivenfall;
    public GameObject soundeffect;
    public GameObject soundeffect2;
    public GameObject horrorscene;

    [Header("Ses")]
    public AudioSource fall;

    [Header("XR")]
    public UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable; // isteğe bağlı, otomatik atanır

    private bool ilacAlindi = false;  // İlacın alınıp alınmadığını kontrol için

    void Awake()
    {
        // Eğer inspector'dan atamadıysan, aynı GameObject'ten almayı dene
        if (grabInteractable == null)
            grabInteractable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
    }

    void Start()
    {
        merdivenfly.SetActive(true);
        merdivenfall.SetActive(false);
        horrorscene.SetActive(false);

        if (grabInteractable != null)
        {
            // İlk seçme (tutma) için listener
            grabInteractable.selectEntered.AddListener(OnSelectEntered);

            // Eğer "activate" olayını (ör. tetik tuşu ile kullanma) da desteklemek istersen:
            grabInteractable.activated.AddListener(OnActivated);
        }
        else
        {
            Debug.LogWarning("IlacAlmaVR: XRGrabInteractable bulunamadı! Lütfen bileşeni ekleyin veya inspector'dan atayın.");
        }
    }

    // selectEntered ile tetik: nesne ilk defa tutulduğunda çalışır
    private void OnSelectEntered(SelectEnterEventArgs args)
    {
        TryTakeIlac();
    }

    // activated ile tetik: interactor'ın "activate" tuşuna basıldığında çalışır
    private void OnActivated(ActivateEventArgs args)
    {
        TryTakeIlac();
    }

    // İlacı alma mantığı tek bir yerde toplanmış
    private void TryTakeIlac()
    {
        if (ilacAlindi) return;
        ilacAlindi = true;

        // Olayları ayarla / objeleri değiştir
        if (engel != null) engel.SetActive(false);
        if (merdivenfly != null) merdivenfly.SetActive(false);
        if (merdivenfall != null) merdivenfall.SetActive(true);
        if (soundeffect != null) soundeffect.SetActive(false);
        if (soundeffect2 != null) soundeffect2.SetActive(true);
        if (horrorscene != null) horrorscene.SetActive(true);
        if (fall != null) fall.Play();

        // Havada süzülen nesneleri fizik etkileşimine al
        foreach (GameObject nesne in havada)
        {
            if (nesne == null) continue;

            Rigidbody rb = nesne.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false;
                rb.useGravity = true;
            }

            // Eğer yukariasaga scripti varsa onu devre dışı bırak
            yukariasaga hareketScripti = nesne.GetComponent<yukariasaga>();
            if (hareketScripti != null)
                hareketScripti.enabled = false;
        }

        // İlaç objesini sahneden kaldır (istersen Destroy yerine SetActive(false) kullan)
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        // Temizlik: listener'ları kaldır
        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.RemoveListener(OnSelectEntered);
            grabInteractable.activated.RemoveListener(OnActivated);
        }
    }
}

using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class Kapi1Degisim : MonoBehaviour
{
    [Header("Kapı ve Sahne Objeleri")]
    public GameObject Kapi1;
    public GameObject Trigger;
    public GameObject degisim;
    public GameObject Corridor2;
    public GameObject Corridor1;
    public GameObject ilksahne;
    public GameObject kornis;

    [Header("Bileşenler")]
    public XRGrabInteractable grab;
    public Animator kapikulpacik;
    public AudioSource ses;

    [Header("Ray Ayarları")]
    public float rayUzaklik3 = 6f;

    void Start()
    {
        Trigger.SetActive(false);
        Corridor2.SetActive(false);
        ilksahne.SetActive(true);

        // Eğer bu objede XRGrabInteractable yoksa, eklemeyi unutma
        if (grab == null)
            grab = GetComponent<XRGrabInteractable>();

        // Grab başladığında çalışır
        grab.selectEntered.AddListener(OnGrab);

        // Eğer bırakıldığında da bir şey olmasını istiyorsan:
        // grab.selectExited.AddListener(OnRelease);
    }

    // Kapı ile ilk temas (Grab başladığında)
    void OnGrab(SelectEnterEventArgs args)
    {
        Debug.Log("Kapı ile etkileşim başladı!");

        // Ses çal
        if (ses != null) ses.Play();

        // Kapı animasyonu oynat
        if (kapikulpacik != null)
            kapikulpacik.Play("kapikulpacik");

        // Trigger ve sahne değişimleri
        Trigger.SetActive(true);
        Corridor1.SetActive(false);
        Corridor2.SetActive(true);

        // Animasyon tekrar için coroutine
        StartCoroutine(AnimasyonuTekrarla());
    }

    // Eğer bırakıldığında tetiklenecek bir şey varsa
    void OnRelease(SelectExitEventArgs args)
    {
        Debug.Log("Kapı bırakıldı.");
    }

    void Update()
    {
        // Raycast ile sahne geçiş kontrolü
        Vector3 origin2 = transform.position;
        Vector3 direction2 = transform.forward;
        Ray ray2 = new Ray(origin2, direction2);
        RaycastHit hit2;

        if (Physics.Raycast(ray2, out hit2, rayUzaklik3))
        {
            if (hit2.collider.gameObject == Trigger)
            {
                Debug.Log("SahneKapandi");
                degisim.SetActive(false);
                Trigger.SetActive(false);
                kornis.SetActive(true);
            }
        }
    }

    IEnumerator AnimasyonuTekrarla()
    {
        while (true)
        {
            yield return new WaitForSeconds(2f);
            if (kapikulpacik != null)
                kapikulpacik.Play("kapikulpidle");
        }
    }
}

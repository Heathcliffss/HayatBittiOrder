using System.Collections.Generic;
using UnityEngine;

public class IlacAlma : MonoBehaviour
{
    public List<GameObject> havada;  // Havada süzülen nesneler listesi
    public GameObject engel;
    public GameObject merdivenfly;
    public GameObject merdivenfall;
    public GameObject soundeffect;
    public GameObject soundeffect2;
    public GameObject horrorscene;

    public AudioSource fall;

    private bool ilacAlindi = false;  // İlacın alınıp alınmadığını kontrol için
    void Start()
    {
        merdivenfly.SetActive(true);
        merdivenfall.SetActive(false);
        horrorscene.SetActive(false);
    }

    void Update()
    {
        if (!ilacAlindi && Input.GetKeyDown(KeyCode.E))
        {
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 3f)) // 3 birimlik mesafede kontrol
            {
                IlacAlma ilac = hit.collider.GetComponent<IlacAlma>();
                if (ilac != null && ilac == this)
                {
                    ilacAlindi = true;

                    foreach (GameObject nesne in havada)
                    {
                        Rigidbody rb = nesne.GetComponent<Rigidbody>();
                        if (rb != null)
                        {
                            rb.isKinematic = false;  // Fizik etkileşimine izin ver
                            rb.useGravity = true;    // Yerçekimini aktif et
                            engel.SetActive(false);
                            merdivenfly.SetActive(false);
                            merdivenfall.SetActive(true);
                            soundeffect.SetActive(false);
                            soundeffect2.SetActive(true);
                            horrorscene.SetActive(true);
                            fall.Play();

                        }

                        yukariasaga hareketScripti = nesne.GetComponent<yukariasaga>();
                        if (hareketScripti != null)
                        {
                            hareketScripti.enabled = false;  // Yukarı-aşağı hareketi durdur
                        }
                    }

                    Destroy(gameObject);
                }
            }
        }
    }
}
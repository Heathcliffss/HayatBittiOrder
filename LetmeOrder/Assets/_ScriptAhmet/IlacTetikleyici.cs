using System.Collections.Generic;
using UnityEngine;

public class IlacAlma : MonoBehaviour
{
    public List<GameObject> havada;  // Havada süzülen nesneler listesi

    private bool ilacAlindi = false;  // İlacın alınıp alınmadığını kontrol için

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
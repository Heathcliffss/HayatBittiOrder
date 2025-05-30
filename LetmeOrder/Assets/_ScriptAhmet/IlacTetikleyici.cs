using System.Collections.Generic;
using UnityEngine;

public class IlacAlma : MonoBehaviour
{
    public List<GameObject> havada;  // Havada süzülen nesneler listesi

    private bool ilacAlindi = false;  // Ýlacýn alýnýp alýnmadýðýný kontrol için

    void Update()
    {
        if (!ilacAlindi && Input.GetKeyDown(KeyCode.E))
        {
            ilacAlindi = true;

            foreach (GameObject nesne in havada)
            {
                Rigidbody rb = nesne.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.isKinematic = false;  // Fizik etkileþimine izin ver
                    rb.useGravity = true;    // Yerçekimini aktif et
                }

                yukariasaga hareketScripti = nesne.GetComponent<yukariasaga>();
                if (hareketScripti != null)
                {
                    hareketScripti.enabled = false;  // Yukarý-aþaðý hareketi durdur
                }
            }

            // Bu scriptin baðlý olduðu nesne (ilac) yok edilir
            Destroy(gameObject);
        }
    }
}

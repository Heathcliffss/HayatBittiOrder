using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NesneYokOlma : MonoBehaviour
{
    public Transform oyuncu;
    public GameObject oda1;
    public GameObject oda2;

   

    [Header("Zamanlamalar")]
    public float odadaKalmaSuresi = 3f;
    public float gecisSuresi = 2f;

    private bool gecisBasladi = false;
    private List<Material> oda1Materyaller = new List<Material>();
    private List<Material> oda2Materyaller = new List<Material>();

    void Start()
    {
        if (oyuncu == null)
            oyuncu = Camera.main?.transform;

        if (oda1 == null || oda2 == null)
        {
            Debug.LogError("Odalar atanmadý!", this);
            enabled = false;
            return;
        }
       

        // Malzemeleri topla (sadece oda1 için çünkü oda2 henüz aktif deðil)
        oda1Materyaller = GetAllMaterials(oda1);

        // Oda2'yi tamamen devre dýþý býrak
        oda2.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform == oyuncu && !gecisBasladi)
        {
            StartCoroutine(OdaDegisimiRutini());
        }
    }

    public IEnumerator OdaDegisimiRutini()
    {
        
        gecisBasladi = true;
        yield return new WaitForSeconds(odadaKalmaSuresi);

        // Oda2'yi aktif et ve materyallerini topla
        oda2.SetActive(true);
        oda2Materyaller = GetAllMaterials(oda2);

        // Oda2 materyallerini tamamen þeffaf baþlat
        foreach (var mat in oda2Materyaller)
        {
            if (mat.HasProperty("_Color"))
            {
                Color c = mat.color;
                c.a = 0f;
                mat.color = c;
            }
        }

        float zaman = 0f;

        while (zaman < gecisSuresi)
        {
            float oran = zaman / gecisSuresi;

            foreach (var mat in oda1Materyaller)
            {
                if (mat.HasProperty("_Color"))
                {
                    Color c = mat.color;
                    c.a = Mathf.Lerp(1f, 0f, oran);
                    mat.color = c;
                }
            }

            foreach (var mat in oda2Materyaller)
            {
                if (mat.HasProperty("_Color"))
                {
                    Color c = mat.color;
                    c.a = Mathf.Lerp(0f, 1f, oran);
                    mat.color = c;
                }
            }

            zaman += Time.deltaTime;
            yield return null;
        }

        // Geçiþ tamamlandýktan sonra oda1'i yok et
        oda1.SetActive(false);
    }

    List<Material> GetAllMaterials(GameObject obj)
    {
        List<Material> materials = new List<Material>();
        Renderer[] renderers = obj.GetComponentsInChildren<Renderer>();

        foreach (Renderer renderer in renderers)
        {
            materials.AddRange(renderer.materials);
        }

        return materials;
    }

    
}

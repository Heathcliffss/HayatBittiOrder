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

        // Malzemeleri topla
        oda1Materyaller = GetAllMaterials(oda1);
        oda2Materyaller = GetAllMaterials(oda2);

        // Oda2'yi baþlangýçta þeffaf yap
        foreach (var mat in oda2Materyaller)
        {
            Color renk = mat.color;
            renk.a = 0f;
            mat.color = renk;
        }
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

        float zaman = 0f;

        while (zaman < gecisSuresi)
        {
            float oran = zaman / gecisSuresi;

            foreach (var mat in oda1Materyaller)
            {
                Color c = mat.color;
                c.a = Mathf.Lerp(1f, 0f, oran);
                mat.color = c;
            }

            foreach (var mat in oda2Materyaller)
            {
                Color c = mat.color;
                c.a = Mathf.Lerp(0f, 1f, oran);
                mat.color = c;
            }

            zaman += Time.deltaTime;
            yield return null;
        }

        // Tamamen geçiþ yaptýktan sonra oda1 yok edilebilir (isteðe baðlý)
        Destroy(oda1);
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
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Collider))]
public class VurVeYokEt : MonoBehaviour
{
    [Header("Hedefler (Prefab bazl� e�le�me)")]
    [Tooltip("�stedi�in hedef prefab(lar)� buraya ekle. Kar��la��lan objenin ad� prefab ad�yla e�le�iyorsa yok edilecektir.")]
    public List<GameObject> targetPrefabs = new List<GameObject>();

    [Header("Alternatif: Tag ile e�le�me (opsiyonel)")]
    [Tooltip("E�er bir targetTag girersen, �arp��an objenin tag'i ile e�le�me yap�l�r ve yok edilir.")]
    public string targetTag = "";

    [Header("Davran��")]
    [Tooltip("Collider trigger ise true yap. Aksi halde fiziksel �arp��ma (OnCollisionEnter) kullan�l�r.")]
    public bool useTrigger = false;

    [Tooltip("Vuruldu�unda efekt/ ses �almak istersen buraya ba�la (opsiyonel).")]
    public GameObject hitEffectPrefab;

    [Tooltip("Hedef yok edildi�inde virg�l ogrn� olarak i�lem yap�lmas�n� istiyorsan beklemeden yok et. E�er false ise yok etmeden �nce efekte izin verir.")]
    public bool immediateDestroy = true;

    // Opsiyonel: e�er bu nesne ayn� zamanda XRGrabInteractable ise, burada referans tutulur
    public UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable;

    void Awake()
    {
        if (grabInteractable == null)
            grabInteractable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
    }

    void OnEnable()
    {
        // collider tipine g�re Unity event'leri tetiklenecek
    }

    void OnTriggerEnter(Collider other)
    {
        if (!useTrigger) return;
        HandleHit(other.gameObject);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (useTrigger) return;
        HandleHit(collision.gameObject);
    }

    void HandleHit(GameObject other)
    {
        if (other == null) return;

        // 1) Tag ile kontrol (�ncelikli)
        if (!string.IsNullOrEmpty(targetTag))
        {
            if (other.CompareTag(targetTag))
            {
                DestroyTarget(other);
                return;
            }
        }

        // 2) Prefab ismine g�re kontrol
        string otherName = other.name;
        // Unity runtime'da prefab instancelar�n adlar� genelde "PrefabName(Clone)",
        // bu y�zden "(Clone)" k�sm�n� temizleyelim
        if (otherName.EndsWith("(Clone)"))
            otherName = otherName.Replace("(Clone)", "").Trim();

        foreach (GameObject prefab in targetPrefabs)
        {
            if (prefab == null) continue;
            if (prefab.name == otherName)
            {
                DestroyTarget(other);
                return;
            }
        }
    }

    void DestroyTarget(GameObject target)
    {
        // Opsiyonel efekt spawn
        if (hitEffectPrefab != null)
        {
            Instantiate(hitEffectPrefab, target.transform.position, Quaternion.identity);
        }

        if (immediateDestroy)
        {
            Destroy(target);
        }
        else
        {
            // k�sa bir gecikme verip sonra yok et (efektin oynayabilmesi i�in)
            Destroy(target, 0.25f);
        }
    }
}

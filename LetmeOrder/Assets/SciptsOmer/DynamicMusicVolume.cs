using UnityEngine;

public class DynamicMusicVolume : MonoBehaviour
{
    public Transform player;           // Oyuncunun Transform'u (genelde kamera veya karakter)
    public float maxDistance = 20f;    // Maksimum mesafe (bu mesafeden sonra ses sýfýr olur)
    public float minDistance = 2f;     // Bu mesafeye kadar geldiðinde ses tam olur

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        // Ses seviyesini mesafeye göre lineer olarak ayarla
        float volume = Mathf.Clamp01(1 - (distance - minDistance) / (maxDistance - minDistance));
        audioSource.volume = volume;
    }
}

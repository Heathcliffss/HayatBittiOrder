using UnityEngine;

public class PlakCalici : MonoBehaviour
{
    public Transform placeholder;
    public string targetTag = "Plak"; // Ýçine girmesi gereken objeye bu tag ver
    public AudioClip playSound; // Çalacak ses
    private AudioSource audioSource;

    private bool inside = false;

    private void Start()
    {
        // AudioSource ekle ve hazýrla
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.clip = playSound;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            // Rigidbody'sini al
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = true; // Fizikle hareket etmesin
                rb.useGravity = false;
            }

            // Objeyi placeholder pozisyonuna sabitle
            other.transform.position = placeholder.position;
            other.transform.rotation = placeholder.rotation;
            // other.transform.SetParent(placeholder); // Ýstersen parent yapabilirsin

            inside = true;

            // Sesi çal
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
    }

    private void Update()
    {
        if (inside)
        {
            gameObject.transform.position = placeholder.transform.position;
        }
    }
}

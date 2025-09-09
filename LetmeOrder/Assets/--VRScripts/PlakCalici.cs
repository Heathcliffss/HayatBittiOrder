using UnityEngine;

public class PlakCalici : MonoBehaviour
{
    public AudioSource musicPlayer;  // Plak takýldýðýnda çalacak müzik
    public string targetTag = "Plak"; // Plak objesine bu tag'i ver

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            // Plak çalara býrakýldýðýnda müziði baþlat
            if (!musicPlayer.isPlaying)
            {
                musicPlayer.Play();
            }

            // Plak objesini sabitle (hareket etmesin)
            other.attachedRigidbody.isKinematic = true;
            other.transform.position = transform.position;
            other.transform.rotation = transform.rotation;
        }
    }
}

using UnityEngine;

public class PlakCalici : MonoBehaviour
{
    public Transform placeholder;
    public string targetTag = "Plak"; // Ýçine girmesi gereken objeye bu tag ver
    private bool inside = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            // Rigidbody'sini al
            Rigidbody rb = GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = true; // Fizikle hareket etmesin
                rb.useGravity = false;
            }

            // Objeyi placeholder pozisyonuna sabitle


            inside = true;
            // other.transform.rotation = placeholder.rotation;
            //other.transform.SetParent(placeholder); // isterse parent yapabilirsin
        }
    }

    public void Update()
    {
        if (inside)
        {
            gameObject.transform.position = placeholder.transform.position;
        }
        
    }

    void transform1() { 
    }
}

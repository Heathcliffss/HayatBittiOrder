using UnityEngine;

public class Sil : MonoBehaviour
{
    public Transform target; // A nesnesi

    void Start()
    {

        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (target != null)
        {
            transform.rotation = target.rotation;
        }
    }
}

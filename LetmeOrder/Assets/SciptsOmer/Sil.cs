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
            Cursor.lockState = CursorLockMode.Locked;
            Vector3 currentRotation = transform.eulerAngles;
            Vector3 targetRotation = target.eulerAngles;

            transform.rotation = Quaternion.Euler(currentRotation.x, targetRotation.y, currentRotation.z);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class karakter : MonoBehaviour
{
    [Header("Hareket Ayarlarý")]
    public float hareketHizi = 5f;
    public float fareHassasiyeti = 2f;

    [Header("Bileþenler")]
    public CharacterController kontrolcu;
    public Transform kamera; // Main Camera

    float dikeyDonus = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Fare hareketi ile kamera dönüþü
        float fareX = Input.GetAxis("Mouse X") * fareHassasiyeti;
        float fareY = Input.GetAxis("Mouse Y") * fareHassasiyeti;

        dikeyDonus -= fareY;
        dikeyDonus = Mathf.Clamp(dikeyDonus, -90f, 90f);

        kamera.localRotation = Quaternion.Euler(dikeyDonus, 0f, 0f); // Yukarý-aþaðý bakýþ
        transform.Rotate(Vector3.up * fareX); // Saða-sola dönüþ

        // Klavye hareketi
        float yatay = Input.GetAxis("Horizontal");
        float dikey = Input.GetAxis("Vertical");

        Vector3 hareket = transform.right * yatay + transform.forward * dikey;
        kontrolcu.Move(hareket * hareketHizi * Time.deltaTime);
    }
}


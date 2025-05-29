using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPick : MonoBehaviour
{
    public float rayUzaklik = 100;
    public GameObject Anahtar1;
    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Vector3 origin = transform.position;
            Vector3 direction = transform.forward;

            Ray ray = new Ray(origin, direction);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, rayUzaklik))
            {
                if (hit.collider.gameObject == Anahtar1)
                {
                    Debug.Log("Oldu");
                }
            }
        }
    }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kapi1Degisim : MonoBehaviour
{
     public float rayUzaklik2 = 4f;
     public float rayUzaklik3 = 6f;
      public GameObject Kapi1;
    public GameObject Trigger;
    public GameObject degisim;
    public GameObject Corridor1;

    void Start()
    {
        Trigger.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Vector3 origin = transform.position;
            Vector3 direction = transform.forward;

            Ray ray = new Ray(origin, direction);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, rayUzaklik2))
            {
                if (hit.collider.gameObject == Kapi1)
                {
                    Debug.Log("SahneKapandi");
                     Trigger.SetActive(true);
                    Corridor1.SetActive(false);
                   
                }
            }
        }
        
       
           Vector3 origin2 = transform.position;
    Vector3 direction2 = transform.forward; // ya da farklı bir yön gerekiyorsa değiştir
    Ray ray2 = new Ray(origin2, direction2);
    RaycastHit hit2;

    if (Physics.Raycast(ray2, out hit2, rayUzaklik3))
    {
            if (hit2.collider.gameObject == Trigger)
            {
                Debug.Log("SahneKapandi");
                degisim.SetActive(false);
                 Trigger.SetActive(false);
        }
    }
    }
}

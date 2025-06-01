using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGecis : MonoBehaviour
{
    public float interactionDistance = 3f;
    public string sceneToLoad = "YeniSahneAdi";

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, interactionDistance))
            {
                if (hit.collider.CompareTag("Door"))
                {
                    SceneManager.LoadScene(sceneToLoad);
                }
            }
        }
    }
}


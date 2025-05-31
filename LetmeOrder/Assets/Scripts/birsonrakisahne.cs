using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class birsonrakisahne : MonoBehaviour
{
 public string hedefSahneAdi;  

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(hedefSahneAdi);
        }
    }
}

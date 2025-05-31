using UnityEngine;

using UnityEngine.SceneManagement;


public class odagecis : MonoBehaviour
{
    
    public string hedefSahneAdi1;

  
     void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(hedefSahneAdi1);
        }
    }
}

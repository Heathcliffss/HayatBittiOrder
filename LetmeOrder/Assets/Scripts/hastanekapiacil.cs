using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hastanekapiacil : MonoBehaviour
{
    public Animator kapisol;
    public Animator kapisag;

    public float menzil1 = 5f;
   
    public bool kapidurum = false;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (kapidurum)
        {
            
            
        }
        else
        {
           
            
        }
       
       
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            kapisol.Play("hastanekapi1");
            kapisag.Play("hastanekapi2");
            kapidurum = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
             kapisol.Play("hastanekapi11");
            kapisag.Play("hastanekapi21");
            kapidurum = false;
        }
    }


}

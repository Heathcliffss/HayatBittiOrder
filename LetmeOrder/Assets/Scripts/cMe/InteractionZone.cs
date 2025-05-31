using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InteractionZone : MonoBehaviour
{
    public GameObject hintText; // TextMeshPro UI yazý objesi

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            hintText.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            hintText.SetActive(false);
        }
    }
}

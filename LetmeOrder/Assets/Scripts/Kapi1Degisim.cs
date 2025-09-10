using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
public class Kapi1Degisim : MonoBehaviour
{
    public float rayUzaklik2 = 4f;
    public float rayUzaklik3 = 6f;
    public GameObject Kapi1;
    public GameObject Trigger;
    public GameObject degisim;
    public GameObject Corridor2;
    public GameObject Corridor1;
    public GameObject ilksahne;
    public GameObject kornis;

    public XRGrabInteractable grab;

    public Animator kapikulpacik;


    public AudioSource ses;

    void Start()
    {
        Trigger.SetActive(false);
        Corridor2.SetActive(false);
        ilksahne.SetActive(true);

        grab = GetComponent<XRGrabInteractable>();
        grab.selectEntered.AddListener(OnGrab);
        //grab.selectExited.AddListener(OnRelaese);

        
    }

    void OnGrab(SelectEnterEventArgs args)
    {
        Debug.Log("AH");

    }



    // Update is called once per frame
    void Update()
    {
       /*if ()
        {
           

            

            

                    ses.Play();
                    kapikulpacik.Play("kapikulpacik");
                    StartCoroutine(AnimasyonuTekrarla());
                    Trigger.SetActive(true);
                    Corridor1.SetActive(false);
                    Corridor2.SetActive(true);


             
        }
*/

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
                kornis.SetActive(true);
            }
        }
    }

    IEnumerator AnimasyonuTekrarla()
    {
        while (true)
        {

            yield return new WaitForSeconds(2f);
            kapikulpacik.Play("kapikulpidle");
        }
    }
}

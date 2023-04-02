using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactions : MonoBehaviour
{
    public GameObject[] hiddenLedge;
    public float revealT = 10;
    //Add update incase animation is added
    void OnTriggerEnter(Collider interact)
    {
        if(interact.tag == "Player")
            StartCoroutine(RevealLedge(revealT));

    }

    private IEnumerator RevealLedge(float interval)
    {
        for(int i = 0; i< hiddenLedge.Length; i++)
        {
            hiddenLedge[i].SetActive(true);
        }
        yield return new WaitForSeconds(interval);
        for(int i = 0; i< hiddenLedge.Length; i++)
        {
            hiddenLedge[i].SetActive(false);
        }
    }
}

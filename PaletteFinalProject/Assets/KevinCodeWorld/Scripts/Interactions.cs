using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactions : MonoBehaviour
{
    public GameObject[] hiddenLedge;//ALSO WORKS FOR ENEMY SPAWN
    public float revealT = 10;
    public bool hiddenV = false;
    bool spawned = false;
    //Add update incase animation is added
    void OnTriggerEnter(Collider interact)
    {
        if(interact.tag == "Player" && this.tag == "LedgeReveal")
        {
            StartCoroutine(RevealLedge(revealT));
            
        }
        else if(interact.tag == "Player" && this.tag == "Spawner" && spawned == false)
        {
            for(int i = 0; i< hiddenLedge.Length; i++)
            {
                hiddenLedge[i].SetActive(true);
            }
            spawned = true;
        }
        

    }

    private IEnumerator RevealLedge(float interval)
    {
        hiddenV = true;
        for(int i = 0; i< hiddenLedge.Length; i++)
        {
            hiddenLedge[i].SetActive(true);
        }
        yield return new WaitForSeconds(interval);
        hiddenV = false;
        for(int i = 0; i< hiddenLedge.Length; i++)
        {
            hiddenLedge[i].SetActive(false);
        }
    }

}

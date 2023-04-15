using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactions : MonoBehaviour
{
    public GameObject[] hiddenLedge;//ALSO WORKS FOR ENEMY SPAWN
    public GameObject[] wave2;
    public GameObject[] wave3;
    public float revealT = 10;
    public float spawnTimer = 5;
    public bool hiddenV = false;
    int wave = 0;
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
        if(spawned == true && wave ==0)
        {
            StartCoroutine(Spawndanemy(spawnTimer));
        }
        else if(spawned == true && wave == 1)
        {
            StartCoroutine(Spawndanemy(spawnTimer));
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
    private IEnumerator Spawndanemy(float interval)
    {
        yield return new WaitForSeconds(interval);
        wave++;
        if(wave == 1)
        {
            for(int i = 0; i< hiddenLedge.Length; i++)
            {
                wave2[i].SetActive(true);
            }
        }
        else if(wave == 2)
        {
            for(int i = 0; i< hiddenLedge.Length; i++)
            {
                wave3[i].SetActive(true);
            }
        }
    }

}

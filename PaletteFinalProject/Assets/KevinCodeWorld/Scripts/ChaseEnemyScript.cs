using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaseEnemyScript : MonoBehaviour
{

    private NavMeshAgent enemy;
    public AudioSource bossM;
    public AudioClip music;

    public Transform PlayerTarget;
    bool wallStick = false;
    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<NavMeshAgent>();
        bossM.clip = music;
        bossM.Play();

    }

    // Update is called once per frame
    void Update()
    {
        if(wallStick == false)
            enemy.SetDestination(PlayerTarget.position);
        else
             enemy.isStopped = true;
             
        
    }
    public void StopChase()
    {
        wallStick = true;
       
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DController : MonoBehaviour
{
    private NavMeshAgent agent;
    private Transform player;
    
    [SerializeField]
    private Vector3 offset;

    private bool spawn;
    [SerializeField]
    private int spawnAnimStep = 5;
    private int step = 0;
    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("MainCamera").transform;
        agent = GetComponent<NavMeshAgent>();
        agent.enabled = false;//NavMeshのせいでfalseにしておかないと床より下にいけなくなる？
        this.gameObject.transform.position += offset;
        spawn = true;
    }

    void Update()
    {
        if(spawn)
        {
            if(step < spawnAnimStep)
            {
                this.gameObject.transform.position -=  1/(float)spawnAnimStep * offset;
                step += 1;
            }
            else
            {
                spawn = false; 
                agent.enabled = true;               
            }
        }
        else
        {
            agent.destination = player.position;
        }
    }

}

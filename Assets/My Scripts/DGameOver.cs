using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DGameOver : MonoBehaviour
{
    private GameObject mainCam;
    private Animator animator;
    private UnityEngine.AI.NavMeshAgent agent;
    private AudioSource audio;
    // Start is called before the first frame update
    void Start()
    {
        mainCam = GameObject.FindWithTag("MainCamera");
        animator = GetComponentInChildren<Animator>();
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!mainCam.GetComponent<Life>().active)
        {
            agent.speed = 0;
            animator.speed = 0;
            audio.Stop();
            transform.Find("allosaurus").gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }
}

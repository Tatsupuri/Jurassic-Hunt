using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Health : MonoBehaviour
{

    [SerializeField]
    public float startingHealth = 5.0f;

    public float currentHealth;
    private Animator animator;
    private NavMeshAgent agent;
    private AudioSource audio;

    private GameObject gameManager;

    private bool damaged;

    private int countor = 0;
    [SerializeField] private int coolTime = 50;


    void Update()
    {
        
    }

    //void OnEnable()//OnEnableはObjectができる度に実行されるのでリスポーンとか可能
    void Start()
    {   
        //animator = GetComponentInChildren<Animator>();
        currentHealth = startingHealth;
        animator = this.GetComponent<Animator>();
        agent = this.GetComponent<NavMeshAgent>();
        audio = this.GetComponent<AudioSource>();
        gameManager = GameObject.FindWithTag("GameController");
    }

    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;
        
        if(currentHealth <= 0)
        {
            if(agent != null)
            {
            animator.SetTrigger("Dead");
            audio.Stop();
            agent.speed = 0;

            BoxCollider[] boxes = GetComponents<BoxCollider>();
                for (int i = 0; i < boxes.Length; i++)
                {
                    boxes[i].enabled = false; 
                }

            transform.Find("allosaurus").gameObject.GetComponent<BoxCollider>().enabled = false;

            gameManager.GetComponent<GameManager>().Kill();
            
            Destroy(this.gameObject, 10.0f);
            }
            else
            {
            Destroy(this.gameObject);
            }
        }

        //animator.SetTrigger("Damage");
        damaged  = true;
    }

    public bool Dead()
    {   
        if(currentHealth <= 0)
        {
            return true;
        }else{
            return false;
        }
    }

    public bool Damaged()
    {
        if(damaged){
           return true;
        }else{
            return false;
        }         
    }

    public float StartHealth()
    {
        return startingHealth;
    }

    public float Ref()
    {
        return currentHealth;
    }

}

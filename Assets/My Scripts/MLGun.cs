using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.XR.MagicLeap;

public class MLGun : MonoBehaviour
{

    private MLInput.Controller controller;

     [SerializeField]
    public int  initialBullets = 6;

    public int bullets;
    

    //public bool sniperMode;

    [SerializeField]
    [Range(0.1f,1.5f)]
    private  float fireRate = 1;

    [SerializeField]
    [Range(1,10)]
    private int damage = 1;

    [SerializeField]
    private float impactForce = 30f;

    [SerializeField]
    private Transform firePoint;

    [SerializeField]
    private ParticleSystem muzzleParticle;

    [SerializeField]
    private ParticleSystem blood;

    [SerializeField]
    private AudioSource gunFireSource;

    [SerializeField]
    private AudioSource noBullets;

    private Animator animator;

    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        MLInput.Start();
        //MLInput.OnControllerButtonDown += OnButtonDown;
        //MLInput.OnControllerButtonUp += OnButtonUp;
        controller = MLInput.GetController(MLInput.Hand.Left);
    }

    void OnDestroy()
    {
        //MLInput.OnControllerButtonDown -= OnButtonDown;
        //MLInput.OnControllerButtonUp -= OnButtonUp;
        MLInput.Stop();
    }

    private void FireGun()
    {
       Debug.DrawRay(firePoint.position, firePoint.forward * 100, Color.red, 2f); 
       
       muzzleParticle.Play();
       gunFireSource.Play();

       Ray ray = new Ray(firePoint.position, firePoint.forward);
       RaycastHit hitInfo;

       if(Physics.Raycast(ray, out hitInfo, 100))
       {
           var health = hitInfo.collider.GetComponent<Health>();
           var navMesh = hitInfo.collider.GetComponent<NavMeshAgent>();
           if(health != null)//敵以外のobjectはhealthを持っていないのでなにも起きないようにする
           {
                health.TakeDamage(damage);
                if(navMesh != null)
                {
                    ParticleSystem bloodInstanse = Instantiate(blood, hitInfo.point, Quaternion.Euler(hitInfo.normal));
                    Destroy(bloodInstanse.gameObject, 1.0f);
                }
            }
       }
    }



    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if(timer >= fireRate)
        {
            if(controller.TriggerValue > 0.2f)
            {
            timer = 0f;
            Debug.Log("shoot");
            FireGun();
            }

        }
    }
}

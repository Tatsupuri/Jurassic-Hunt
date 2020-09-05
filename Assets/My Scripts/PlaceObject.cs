using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.MagicLeap;

public class PlaceObject : MonoBehaviour
{

    public GameObject ObjectToPlace;
    private MLInput.Controller controller;

    [SerializeField]
    [Range(0.1f,1.5f)]
    private  float rate = 1;

    private float timer;

    void Start()
    {
        MLInput.Start();
        controller = MLInput.GetController(MLInput.Hand.Left);
    }

    void OnDestroy()
    {
        MLInput.Stop();
    }

    private void Place()
    {
       RaycastHit hitInfo;
       if(Physics.Raycast(transform.position, transform.forward, out hitInfo))
       {
           GameObject placeObject = Instantiate(ObjectToPlace, hitInfo.point, Quaternion.Euler(hitInfo.normal));
       }
    }

    void Update()
    {   
        timer += Time.deltaTime;

        if(timer >= rate)
        {
            if(controller.TriggerValue > 0.2f)
            {
            timer = 0f;
            Place();
            }

        }
    }
}

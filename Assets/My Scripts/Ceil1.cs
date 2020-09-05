using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.MagicLeap;

public class Ceil1 : MonoBehaviour
{
    public GameObject prefab;
    private bool active = false;

    private MLInput.Controller controller;

    void Start()
    {
         MLInput.Start();
         controller = MLInput.GetController(MLInput.Hand.Left);
    }

    void OnDestroy()
    {
        MLInput.Stop();
    }
    
    void Update()
    {
        if(!active)
        {
            Generator();
        }

    }

    void Generator()
    {

        RaycastHit hit;
        Debug.DrawRay(this.transform.position, Vector3.up, Color.green);
        if(Physics.Raycast(this.transform.position, Vector3.up, out hit))
       {
         Debug.Log("Hit to Something");
         if(hit.collider.gameObject.tag == "Environment" && hit.point.y > 0.0f && Mathf.Abs(Vector3.Dot(hit.normal,Vector3.right)) < 0.2)
         {
            Debug.Log("Hit to the ceil");
           GameObject needle = Instantiate(prefab, hit.point, Quaternion.Euler(0,0,0));
           needle.transform.SetParent(GameObject.FindWithTag("Ceil").transform);
           active = true;
           needle.SetActive(false);
         }
         
       }
    }


}

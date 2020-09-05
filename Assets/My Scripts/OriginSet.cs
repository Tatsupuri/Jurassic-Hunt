using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.MagicLeap;

public class OriginSet : MonoBehaviour
{
    public GameObject holePrefab;
    public GameObject origin;
    public Transform gates;

    public Transform meshes;

    private MLInput.Controller controller;

    private bool setTrigger;
    [SerializeField] private int holeNum = 5;
    private int count = 0;

    [SerializeField] Vector3 offset;

    [SerializeField]
    [Range(0.5f,1.0f)] 
    private float cutoff = 0.9f;

    void Start()
    {
        MLInput.Start();
        controller = MLInput.GetController(MLInput.Hand.Left);
        setTrigger = false;
    }

    void OnDestroy()
    {
        MLInput.Stop();
    }

    private void Set()
    {
       RaycastHit hitInfo;
       if(Physics.Raycast(transform.position, transform.forward, out hitInfo))
       {
           origin.transform.position = hitInfo.point;
       }
    }

    private int holeGenerator()
    {
      Vector3 randomXY = Random.insideUnitCircle;
      Vector3 randomXZ = new Vector3();

      randomXZ.x = randomXY.x;
      randomXZ.y = 0;
      randomXZ.z = randomXY.y;


      RaycastHit hit;
       if(Physics.Raycast(origin.transform.position + offset, randomXZ, out hit))
       {
         if(hit.collider.gameObject.tag == "Gate")
         {
           Debug.Log("Hit the gates");
           return 0;
         }
         else
         {
          if( (Vector3.Dot(hit.normal, offset.normalized) < 0.2 || Vector3.Dot(hit.normal, offset.normalized) > -0.2))//壁であるかの判定
          {
              GameObject gate = Instantiate(holePrefab, (1.0f-cutoff)*(origin.transform.position) -cutoff * offset + cutoff * hit.point, Quaternion.Euler(hit.normal.x, hit.normal.y, hit.normal.z));

              //上のベクトルの計算の途中過程
              //(origin.transform.position + offset) + cutoff * (hit.point - (origin.transform.position + offset)) - offset
              //(1.0f-cutoff)*(origin.transform.position) -cutoff * offset + cutoff * (hit.point)

                gate.transform.SetParent(gates);
                return 1;
          }
          else
          {
            return 0;
          }
        }
       }
       else
       {
         return 0;
       }
    }

    void Update()
    {
      if(!setTrigger){
        if(controller.TriggerValue > 0.2f && SceneManager.GetActiveScene().name == "Title")
        {
          Set();
          setTrigger = true;
          //SceneManager.LoadScene("Stage1");
        } 
      }else
      {
        if(count < holeNum)
        {
          count += holeGenerator();
          Debug.Log(count);
        }
        else
        {
          foreach(Transform mesh in meshes)
          {
            mesh.gameObject.GetComponent<ChangeMaterial>().ChangeMat();
          }

          SceneManager.LoadScene("Stage1");
        }
      }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinasaurGenerator : MonoBehaviour
{
    public GameObject dinasaurPrefab;

    private float timer;
    [SerializeField]
    private float timerInitial = 5.0f;

    [SerializeField]
    [Range(1f,60f)]
    private  float rate = 10f;

    private Quaternion dir; 

    void Start()
    {
        timer = timerInitial;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if(timer >= rate)
        {
            dir = Quaternion.LookRotation(GameObject.FindWithTag("MainCamera").transform.position - this.gameObject.transform.position, Vector3.up);
            timer = 0f;
            //,dir
            //GameObject dinasaur = Instantiate(dinasaurPrefab,gate.transform);
            GameObject dinasaur = Instantiate(dinasaurPrefab,this.transform.parent.transform.position, dir);
            //Debug.Log(dinasaur.transform.position);

        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fall : MonoBehaviour
{

    [SerializeField] Vector3 diff = 0.1f * Vector3.down;
    [SerializeField] float startTime = 20f;

    public GameObject generators;
    public Transform needles;

    [SerializeField]
    private AudioSource audio;

    private float timer = 0f;
    private bool active = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {   
        if(timer < startTime)
        {
            timer += Time.deltaTime;
            Debug.Log(timer);
        }
        else
        {
            if(!active)
            {
                active = true;
                audio.Play();
            }

            generators.SetActive(false);
            foreach (Transform needle in needles)
            {
                needle.gameObject.SetActive(true);
                needle.Translate(diff);
            }
        }
        

    }
}

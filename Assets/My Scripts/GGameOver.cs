using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GGameOver : MonoBehaviour
{
    private GameObject mainCam;
    private GameObject generator;
    private AudioSource audio;
    // Start is called before the first frame update
    void Start()
    {
        mainCam = GameObject.FindWithTag("MainCamera");
        generator = this.transform.GetChild(1).gameObject;
        audio = this.transform.GetChild(2).gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!mainCam.GetComponent<Life>().active)
        {
            generator.SetActive(false);
            audio.Stop();
        }
    }
}

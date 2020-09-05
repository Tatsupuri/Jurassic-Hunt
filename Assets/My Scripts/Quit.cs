using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.MagicLeap;
using UnityEngine.SceneManagement;

public class Quit : MonoBehaviour
{
    private MLInput.Controller controller;

    // Start is called before the first frame update
    void Start()
    {
         MLInput.Start();
        MLInput.OnControllerButtonDown += OnButtonDown;
        //MLInput.OnControllerButtonUp += OnButtonUp;
        controller = MLInput.GetController(MLInput.Hand.Left);
    }

     void OnDestroy() {
        MLInput.OnControllerButtonDown -= OnButtonDown;
        //MLInput.OnControllerButtonUp -= OnButtonUp;
        MLInput.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnButtonDown(byte controllerId, MLInput.Controller.Button button) 
    {
        if (button == MLInput.Controller.Button.HomeTap) 
        {
            Application.Quit();
        }
        else if(button == MLInput.Controller.Button.Bumper)
        {

        }
    }
}

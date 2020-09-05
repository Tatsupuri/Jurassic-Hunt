using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.MagicLeap;

public class ToolChange : MonoBehaviour
{
    private MLInput.Controller controller;
    //private MLInput.Controller.TouchpadGesture touchpadGesture;

    public GameObject up;
    public GameObject down;
    public GameObject right;
    public GameObject left;

    // Start is called before the first frame update
    void Start()
    {
        MLInput.Start();
        controller = MLInput.GetController(MLInput.Hand.Left);
        UnSelectAll();
        up.SetActive(true);
        }

    void OnDestroy()
    {
        MLInput.Stop();
    } 

    // Update is called once per frame
    void Update()
    {
        string gestureType = controller.CurrentTouchpadGesture.Type.ToString();
        string gestureDirection = controller.CurrentTouchpadGesture.Direction.ToString();
        string gestureState = controller.TouchpadGestureState.ToString();
        
        if(gestureType == "Swipe" && gestureState == "Start")
        {
            UnSelectAll();

            if(gestureDirection == "Up")
            {
                up.SetActive(true);
            }
            else if(gestureDirection == "Down")
            {
                down.SetActive(true);
            }
            else if(gestureDirection == "Right")
            {
                right.SetActive(true);
            }
            else if(gestureDirection == "Left")
            {
                left.SetActive(true);
            }
        }

    }

    void UnSelectAll()
    {
        up.SetActive(false);
        down.SetActive(false);
        right.SetActive(false);
        left.SetActive(false);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.MagicLeap;

public class DynamicBeam : MonoBehaviour
{
    public GameObject controller;
    private LineRenderer beamLine;
    public Color startColor;
    public Color endColor;

    // Start is called before the first frame update
    void Start()
    {
        beamLine = GetComponent<LineRenderer>();
        beamLine.startColor = startColor;
        beamLine.endColor = endColor;

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = controller.transform.position;
        transform.rotation = controller.transform.rotation;

        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.forward, out hit))
        {
            Debug.Log(transform.forward * 5);
            beamLine.useWorldSpace = true;
            beamLine.SetPosition(0, transform.position);//始点
            beamLine.SetPosition(1, hit.point);//終点
        }
        else
        {
            beamLine.useWorldSpace = true;
            beamLine.SetPosition(0, transform.position);
            beamLine.SetPosition(1, transform.position + transform.forward * 5);//始点からビームの向きに5m行ったところの位置ベクトル
            Debug.Log(transform.forward * 5);
            //beamLine.SetPosition(1, Vector3.forward * 5);
        }
    }
}

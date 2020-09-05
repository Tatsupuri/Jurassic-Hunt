using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceOnOrigin : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
     this.gameObject.transform.position = Origin.instance.transform.position;   
    }
}

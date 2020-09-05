using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateCollider : MonoBehaviour
{
   public bool active = true;
   
   private void OnTriggerEnter(Collider col)
   {
       if(col.gameObject.tag == "Gate")
       {
           active = false;
       }
   }
}

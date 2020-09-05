using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMaterial : MonoBehaviour
{
    [SerializeField] private Material newMaterial;

    public void ChangeMat()
    {
        this.GetComponent<Renderer>().material = newMaterial;
    }
}

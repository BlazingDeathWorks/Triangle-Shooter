using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinTri : MonoBehaviour
{
    private GameObject tri; 
    void Start()
    {
        tri = gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, 0, 1));
    }
}

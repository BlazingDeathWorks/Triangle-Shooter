using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinTri : MonoBehaviour
{
    [SerializeField] private float _speed = 1;
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, 0, _speed));
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle_Mover : MonoBehaviour
{
    Vector3 origin = new Vector3(0, 0, 0);
    Vector3 rotaxis = new Vector3(0, 1, 0);
    float prog = 0.0f;
    float rate = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        transform.SetPositionAndRotation(origin, new Quaternion());
    }
    // Update is called once per frame
    void Update()
    {
        prog = (prog + rate) % 360;
        // transform.RotateAround(origin, rotaxis, prog);
        transform.Rotate(new Vector3(0, 1, 0));
        transform.Translate(new Vector3(0, 0, 0.1f));
    }
}

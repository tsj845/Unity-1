using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controlled : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 v = new Vector3(Input.GetAxisRaw("Horizontal") * 0.125f, 0, Input.GetAxisRaw("Vertical") * 0.125f);
        transform.position += v;
    }
}

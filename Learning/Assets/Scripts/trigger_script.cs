using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trigger_script : MonoBehaviour
{
    Vector3 origin = new Vector3(6, 0, 0);
    // Start is called before the first frame update
    void Start()
    {
        //
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = origin;
    }
    private void OnTriggerEnter (Collider other) {
        Debug.Log("collision");
    }
}

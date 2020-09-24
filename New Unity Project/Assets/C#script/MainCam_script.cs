using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCam_script : MonoBehaviour
{
    public float Speed;
    // Start is called before the first frame update
    void Start()
    {
        Speed =0.5f;
        transform.position =new Vector3 (8f*1.7f,10,-10f);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey("left"))
        {
            transform.position = new Vector3(transform.position.x -Speed, transform.position.y, transform.position.z);
        }
        if(Input.GetKey("right"))
        {
            transform.position = new Vector3(transform.position.x +Speed, transform.position.y, transform.position.z);
        }
        if(Input.GetKey("up"))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z+Speed);
        }
        if(Input.GetKey("down")){
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z-Speed);
        }          
    }
}

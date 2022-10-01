using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float damage=1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetDirection(Vector3 dir,float speed)
    {
        GetComponent<Rigidbody2D>().velocity = dir*speed;
    }

}

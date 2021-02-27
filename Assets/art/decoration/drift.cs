using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class drift : MonoBehaviour
{
    // Start is called before the first frame update

    public float speed = 1f;
    public Vector2 dir = new Vector2(0,1);
  

    void Start()
    {
      speed= Random.Range(0.0001f, 0.1f);  
      dir = new Vector2(Random.Range(-1f,1f),Random.Range(-1f,1f));
      float r= Random.Range(1f,5f);
      transform.localScale = new Vector2(r,r);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x + dir.x * speed * Time.deltaTime, transform.position.y + dir.y * speed * Time.deltaTime, transform.position.z);
        //if(transform.position.x)
    }
}

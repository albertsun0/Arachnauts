using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hover : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
     void OnTriggerEnter2D(Collider2D coll)
    {
   
        anim.SetBool("hover", true);

    }
    void OnTriggerExit2D(Collider2D other)
    {
        anim.SetBool("hover", false);
    }
}

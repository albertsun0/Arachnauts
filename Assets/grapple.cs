using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grapple : MonoBehaviour
{
    public Camera mainCamera;
    public LineRenderer _lineRenderer;
    public DistanceJoint2D _distanceJoint;
    public DistanceJoint2D _distanceJoint2;
    public LineRenderer _lineRenderer2;

    //ground movement stuff

    private collision coll;

    enum State
    {
        idle = 0,
        walk = 1,
        jumping = 2,
        web = 3,
    }

    bool direction; //right  = true

    State currentState = State.idle;

    float horizontalInput = 0;
    float verticalInput = 0;
    float jumpForce = 3f;
    float speed =3f;
    Rigidbody2D rb;

    bool facingLeft;
    public float reelspeed = 1;
    
    //spider swap
    spiderswap swapScript;
    //animation
    private Animator anim;
    level_loader load;
    
    // Start is called before the first frame update
    void Start()
    {
        _distanceJoint.enabled = false;
        _distanceJoint2.enabled = false;
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<collision>();
        anim = GetComponent<Animator> ();
        
        GameObject levelLoad = GameObject.Find("level loader");
        load = levelLoad.GetComponent("level_loader") as level_loader;

        GameObject swapper = GameObject.Find("swap arrow");
        swapScript = swapper.GetComponent<spiderswap>();
    }

    Vector2 mousePos;
    Vector2 NormDirection;
    Vector2 NormDirection2;
    RaycastHit2D hit;
    RaycastHit2D hit2;

    GameObject connectedSpider1;
    GameObject connectedSpider2;
    Vector2 connectedOffset1;
    Vector2 connectedOffset2;
 
    
    void Update()
    {
        if (_distanceJoint.enabled) 
        {
            _lineRenderer.SetPosition(1, transform.position);
        }
        if (_distanceJoint2.enabled) 
        {
            _lineRenderer2.SetPosition(1, transform.position);
        }
        if(currentState == State.web){
            anim.SetInteger("state", 3);
        }
        else{
            anim.SetInteger("state", 0);
        }
        
        

        if(GameObject.Find(swapScript.selectedSpider).GetInstanceID() == this.gameObject.GetInstanceID()){ //if spider is selected
        
            horizontalInput = Input.GetAxis("Horizontal");
            verticalInput = Input.GetAxis("Vertical");
            
            if(currentState == State.web){
                anim.SetInteger("state", 3);
            }
            else if(currentState == State.jumping){
                anim.SetInteger("state", 2);
            }
            else if(rb.velocity.x != 0){
                    anim.SetInteger("state", 1);
            }
            else{
                anim.SetInteger("state", 0);
            }

            if (currentState == State.jumping && coll.onGround == true) //land
            {
                currentState = State.idle;
            }
            
            if(currentState != State.web){
                rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);
                
                if ((Input.GetKey(KeyCode.UpArrow)|| Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.Space)) && currentState != State.jumping && coll.onGround == true )  //jump
                {
                    rb.velocity = new Vector2(rb.velocity.x, 0);
                    rb.velocity += Vector2.up * jumpForce;
                    currentState = State.jumping;
                }
            }
            if (horizontalInput < 0 && !facingLeft) //flip to face right direction
                reverseImage();
            else if (horizontalInput > 0 && facingLeft)
                reverseImage();
            
            
            if (Input.GetMouseButtonDown(0)) //grapple one
            {
                if(!_distanceJoint.enabled){
                    mousePos = (Vector2) mainCamera.ScreenToWorldPoint(Input.mousePosition);            
                    Vector2 direction = new Vector2 (mousePos.x-transform.position.x, mousePos.y-transform.position.y);
                    hit = Physics2D.Raycast(transform.position, direction);
                    Debug.DrawRay(transform.position, direction, Color.red);
                    
                    float norm = Mathf.Sqrt(Mathf.Pow(direction.x,2)+ Mathf.Pow(direction.y,2));
                    NormDirection = new Vector2(direction.x/norm, direction.y/norm);
                    print(hit.collider.name);
                    if(Vector2.Distance(hit.point,transform.position) < 2 && hit.collider.name != "ice"){
                        
                        _distanceJoint.distance = Vector2.Distance(hit.point,transform.position);
                        _lineRenderer.SetPosition(0, new Vector2(hit.point.x, hit.point.y));
                        _lineRenderer.SetPosition(1, transform.position);
                        _distanceJoint.connectedAnchor = new Vector2(hit.point.x, hit.point.y);
                        _distanceJoint.enabled = true;
                        _lineRenderer.enabled = true;
                        currentState = State.web;
                    }
                    
                }
                else{
                    _distanceJoint.enabled = false;
                    _lineRenderer.SetPosition(0, new Vector2(0,0));
                    _lineRenderer.SetPosition(1, new Vector2(0,0));
                    if(!_distanceJoint.enabled && !_distanceJoint2.enabled){ //if not connected, fall
                    currentState = State.jumping;
                    }
                }
            }
            

            if (Input.GetMouseButtonDown(1)) //grapple 2
            { 
                if(!_distanceJoint2.enabled){
                    mousePos = (Vector2) mainCamera.ScreenToWorldPoint(Input.mousePosition);            
                    Vector2 direction = new Vector2 (mousePos.x-transform.position.x, mousePos.y-transform.position.y);
                    hit2 = Physics2D.Raycast(transform.position, direction);
                    Debug.DrawRay(transform.position, direction, Color.red);

                    float norm = Mathf.Sqrt(Mathf.Pow(direction.x,2)+ Mathf.Pow(direction.y,2));
                    NormDirection2 = new Vector2(direction.x/norm, direction.y/norm);

                    if(Vector2.Distance(hit2.point,transform.position) < 2 && hit.collider.name != "ice"){
                        _distanceJoint2.distance = Vector2.Distance(hit2.point,transform.position);
                        _lineRenderer2.SetPosition(0, new Vector2(hit2.point.x, hit2.point.y));
                        _lineRenderer2.SetPosition(1, transform.position);
                        _distanceJoint2.connectedAnchor = new Vector2(hit2.point.x, hit2.point.y);
                        _distanceJoint2.enabled = true;
                        _lineRenderer2.enabled = true;
                        currentState = State.web;
                    }
                }
                else{
                    _distanceJoint2.enabled = false;
                    _lineRenderer2.SetPosition(0, new Vector2(0,0));
                    _lineRenderer2.SetPosition(1, new Vector2(0,0));
                    if(!_distanceJoint.enabled && !_distanceJoint2.enabled){ //if not connected, fall
                        currentState = State.jumping;
                    }
                }

            }

            if(currentState == State.web && Input.GetKey(KeyCode.Space)){ //remove webs if jump while connected
                currentState = State.jumping;
                _distanceJoint2.enabled = false;
                _lineRenderer2.SetPosition(0, new Vector2(0,0));
                _lineRenderer2.SetPosition(1, new Vector2(0,0));
                _distanceJoint.enabled = false;
                _lineRenderer.SetPosition(0, new Vector2(0,0));
                _lineRenderer.SetPosition(1, new Vector2(0,0));
            }

            if(Input.GetKey(KeyCode.Q)){
                if(_distanceJoint.enabled){
                    if(_distanceJoint.distance > 0.1 && Vector2.Distance(hit.point,transform.position) > 0.6){
                        _distanceJoint.distance = _distanceJoint.distance-10*Time.deltaTime;
                        transform.position = new Vector2(transform.position.x + NormDirection.x * reelspeed * Time.deltaTime, transform.position.y + NormDirection.y * reelspeed * Time.deltaTime);
                    }
                }
            }
            if(Input.GetKey(KeyCode.E)){
                if(_distanceJoint2.enabled){
                    if(_distanceJoint2.distance > 0.1 && Vector2.Distance(hit2.point,transform.position) > 0.6){
                        _distanceJoint2.distance = _distanceJoint2.distance-10*Time.deltaTime;
                        transform.position = new Vector2(transform.position.x + NormDirection2.x * reelspeed * Time.deltaTime, transform.position.y + NormDirection2.y * reelspeed * Time.deltaTime);
                    }
                }
            }

        

        }
        else{
            if(currentState != State.web){
                rb.velocity = new Vector2(rb.velocity.x/2, rb.velocity.y);
            }
        }
        
    }
    
    void reverseImage()
    {
        // Switch the value of the Boolean
        facingLeft = !facingLeft;

        // Get and store the local scale of the RigidBody2D
        Vector2 theScale = rb.transform.localScale;

        // Flip it around the other way
        theScale.x *= -1;
        rb.transform.localScale = theScale;
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.name == "pipe_front")
        {
            rb.isKinematic = false;
           load.LoadNextLevel(); 
        }
    }
}


//terraria grappling hook
//player goes midway between hooks
//one button press

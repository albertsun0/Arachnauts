using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collision : MonoBehaviour
{

    [Header("Layers")]
    public LayerMask groundLayer;
    public LayerMask spiders;
    [Space]

    public bool onGround;

    [Space]

    [Header("Collision")]

    public float collisionRadius = 0.25f;
    public Vector2 bottomOffset;
    public Vector2 leftOffset;
    public Vector2 rightOffset;
    private Color debugCollisionColor = Color.red;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        onGround = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, collisionRadius, groundLayer) 
        ||  Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, collisionRadius, spiders)
        || Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, collisionRadius, spiders)
        || Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, collisionRadius, groundLayer)
        || Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, collisionRadius, groundLayer)
        || Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, collisionRadius, spiders);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        var positions = new Vector2[] { bottomOffset};

        Gizmos.DrawWireSphere((Vector2)transform.position + bottomOffset, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + leftOffset, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + rightOffset, collisionRadius);
    }
}
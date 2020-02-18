    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 7f;
    
    public Rigidbody2D rb;

    private Vector2 directionAiming;

    void Start(){
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        directionAiming.x = Input.GetAxisRaw("Horizontal");
        directionAiming.y = Input.GetAxisRaw("Vertical");
        directionAiming = directionAiming.normalized;
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + directionAiming * moveSpeed * Time.fixedDeltaTime);
    }
    public Vector2 GetDirection(){
        return directionAiming;
    }
}

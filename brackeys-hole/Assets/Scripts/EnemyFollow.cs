using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public float speed = 2.7f;
    public float stopDistance;
    public float sightDistance = 30;
    private Transform target;
    public Transform partToRotate;
    public float turnspeed = 10f;
    private Rigidbody2D rb;
    private Vector2 lastSeen;
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        lastSeen = target.position;
            rb = gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Vector2.Distance(transform.position, lastSeen) > stopDistance)
        {
            rb.AddRelativeForce(target.position.normalized * speed, ForceMode2D.Force);
            //rb.AddForce(-target.position * speed * Time.deltaTime);
            //transform.position.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
        RaycastHit hit;
        if(Physics.Raycast(transform.position, target.position, out hit, sightDistance)){
            if(hit.transform == target){ //I can see you bitch
                lastSeen = target.position;
            }
        }

        Vector3 dir = target.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + 270;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}

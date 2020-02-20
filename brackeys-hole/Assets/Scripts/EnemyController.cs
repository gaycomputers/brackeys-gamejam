using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public Transform firePoint;
    public GameObject projectilePrefab;
    public float timeBetweenShots = 1f;
    public float startTimeBetweenShots = 10f;

    public float projectileForce = 4f;
    public float speed = 2.7f;
    public float stopDistance;
    public float sightDistance = 30;
    private Transform target;
    public Transform partToRotate;
    public float turnspeed = 100f;
    private Rigidbody2D rb;
    private Vector2 lastSeen;

    void Start()
    {   
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        lastSeen = target.position;
        rb = gameObject.GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;       
    }

    // Update is called once per frame
    void Update()
    {
         if (Vector2.Distance(transform.position, lastSeen) > stopDistance)
        {
            rb.AddRelativeForce(target.position.normalized * speed, ForceMode2D.Force);
            //rb.AddForce(-target.position * speed * Time.deltaTime);
            //transform.position.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
        /*
        RaycastHit2D[] hit = Physics2D.RaycastAll(transform.position, target.position, sightDistance);
            bool see = true;
            for(int i = 0; i < hit.Length-1; i++){
                if(hit[i].collider.tag == "wall"){
                    see = false;
                    i = hit.Length;
                }
                else if(hit[i].collider.tag == "Player"){
                    Debug.Log("I see: " +hit[i].collider.name);
                    i = hit.Length;
                }
            }
            if(see){ //I can see you bitch
           */     lastSeen = target.position;
            /*    RangedAttack();
            }
            */
        Vector2 dir = lastSeen - new Vector2(transform.position.x, transform.position.y);
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + 270;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
       
    }

    void RangedAttack()
    {

        if (timeBetweenShots <= 0)
        {
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            rb.AddForce(firePoint.up * projectileForce, ForceMode2D.Impulse);
            timeBetweenShots = startTimeBetweenShots;
        }

        else
        {
            timeBetweenShots -= Time.deltaTime;
        } 
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;
    }
}

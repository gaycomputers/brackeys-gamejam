using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    public GameObject swordPrefab;
    public float throwStrength = .5f;
    public float gravityStrength = .1f;
    public float reach = 1f;
    public string rangedKey = "Fire2";
    public string meleeKey = "Fire1";

    private float meleeAttackTimeElapsed = 0f;
    private GameObject theLaunchedSword;
    private bool swordIsLaunched = false;
    private bool meleeIsSwinging = false;
   // private bool clockwiseGravity = false;
    private float lastAngle = 361;
    private float swordSpin = 10f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown(rangedKey) && !swordIsLaunched) RangedAttack();
        else if(Input.GetButtonDown(rangedKey) && !IsSwordClose()) GrabSword();
    }

    void FixedUpdate(){
        if(swordIsLaunched){
            ReturnSword();
        }
    }

    void OnCollisionEnter(Collision collision){
        Debug.Log(collision.gameObject.name);
        if(collision.gameObject.name == theLaunchedSword.name) {
            Destroy(theLaunchedSword);
            swordIsLaunched = false;
        }
    }
    void RangedAttack(){
        //PlayerMovement direction = gameObject.GetComponent<PlayerMovement>();
        //LaunchSword(gameObject.GetComponent<PlayerMovement>().GetDirection(), throwStrength);//probably change direction to where the mouse points?
        LaunchSword(gameObject.GetComponent<MouseController>().GetMouseDirection(), throwStrength);
    }

    void LaunchSword(Vector2 direction, float strength){
        if (!swordIsLaunched) swordIsLaunched = true;
        theLaunchedSword = Instantiate(swordPrefab, new Vector2(gameObject.transform.position.x + direction.normalized.x/10,gameObject.transform.position.y + direction.normalized.y/10), Quaternion.identity);//change direction to correct value
        theLaunchedSword.transform.up = direction;
        theLaunchedSword.GetComponent<Rigidbody2D>().AddForce(direction * strength, ForceMode2D.Impulse);
        //theLaunchedSword.GetComponent<BoxCollider2D>().isTrigger = false;
    }

    void ReturnSword(){
        Vector2 direction = (gameObject.transform.position - theLaunchedSword.transform.position).normalized;
        Vector2 orbit = Vector2.Perpendicular(direction);
        if(lastAngle == 361f){
            lastAngle = Vector2.Angle(gameObject.transform.position, theLaunchedSword.transform.position);
        }
        else {
            float angle = Vector2.Angle(gameObject.transform.position, theLaunchedSword.transform.position);
            if(angle > lastAngle){
                if(Vector2.Distance(theLaunchedSword.transform.position, gameObject.transform.position) < reach/2){
                    theLaunchedSword.GetComponent<Rigidbody2D>().AddForce(orbit, ForceMode2D.Impulse);
                    theLaunchedSword.GetComponent<Rigidbody2D>().AddTorque(-swordSpin);
                }
            }
            else if(angle < lastAngle){
                if(Vector2.Distance(theLaunchedSword.transform.position, gameObject.transform.position) < reach/2){
                    theLaunchedSword.GetComponent<Rigidbody2D>().AddForce(-orbit, ForceMode2D.Impulse);
                    theLaunchedSword.GetComponent<Rigidbody2D>().AddTorque(swordSpin);
                }
            }
            else  theLaunchedSword.GetComponent<Rigidbody2D>().AddTorque(swordSpin);
            lastAngle = angle;
            
        }
        //if (clockwiseGravity) orbit = -orbit;
        if(Vector2.Distance(theLaunchedSword.transform.position,gameObject.transform.position) >= reach/2)
        theLaunchedSword.GetComponent<Rigidbody2D>().AddForce(direction * gravityStrength, ForceMode2D.Impulse);
        
    }
    bool IsSwordClose(){
        Debug.Log(Vector2.Distance(theLaunchedSword.transform.position,gameObject.transform.position));
        return Vector2.Distance(theLaunchedSword.transform.position,gameObject.transform.position) >= reach;
    }
    void GrabSword(){
        Destroy(theLaunchedSword);
        swordIsLaunched = false;
    }
    void MeleeAttack(){
        if (!meleeIsSwinging) meleeIsSwinging = true;
        meleeAttackTimeElapsed += Time.deltaTime;
        NextMeleeAttackFrame();
    }

    void NextMeleeAttackFrame(){
        //animate through array based on time elapsed.
    }
}

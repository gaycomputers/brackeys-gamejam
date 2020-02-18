using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    public GameObject swordPrefab;
    public float throwStrength = 10f;
    public float gravityStrength = .1f;
    public string rangedKey = "Fire2";
    public string meleeKey = "Fire1";
    private float meleeAttackTimeElapsed = 0f;
    private GameObject theLaunchedSword;
    private bool swordIsLaunched = false;
    private bool meleeIsSwinging = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown(rangedKey) && !swordIsLaunched) RangedAttack();
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
        LaunchSword(gameObject.GetComponent<PlayerMovement>().GetDirection(), throwStrength);//probably change direction to where the mouse points?
    }

    void LaunchSword(Vector2 direction, float strength){
        if (!swordIsLaunched) swordIsLaunched = true;
        theLaunchedSword = Instantiate(swordPrefab, gameObject.transform.position, Quaternion.identity);//change direction to correct value
        theLaunchedSword.GetComponent<Rigidbody2D>().AddForce(direction * strength, ForceMode2D.Impulse);
    }

    void ReturnSword(){
        theLaunchedSword.GetComponent<Rigidbody2D>().AddForce((gameObject.transform.position - theLaunchedSword.transform.position).normalized * gravityStrength, ForceMode2D.Impulse);
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

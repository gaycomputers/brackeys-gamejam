using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoverScript : MonoBehaviour
{
    SpriteRenderer rend;
    private bool unfaded = true;
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        Color c = rend.material.color;
        c.a = .8f;
        rend.material.color = c;
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D col){
        if(col.tag == "Player" && unfaded){
            unfaded = false;
            StartCoroutine("FadeOut");
        }
    }
    IEnumerator FadeOut(){
        for (float f = 1f; f >= -0.3; f -= 0.3f){
            Color c = rend.material.color;
            c.a = f;
            rend.material.color = c;
            yield return new WaitForSeconds(0.05f);
        }
        
    }
}

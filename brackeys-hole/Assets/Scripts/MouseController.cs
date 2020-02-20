using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    
    //public Camera mainCam;
    //private Vector2 mousePointer;
    public Texture2D cursor;
    // Start is called before the first frame update
    private Vector3 mp;
    void Start()
    {
        //Cursor.visible = false;
        //cursor = (Texture2D)Resources.Load("Art/Cursor") ;
        //Cursor.SetCursor(cursor,new Vector2(0,0),CursorMode.Auto);
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 imp = Input.mousePosition;
        mp = Camera.main.ScreenToWorldPoint(imp);
    }

    public Vector2 GetMouseDirection(){
        Vector2 direction = new Vector2(mp.x - transform.position.x, mp.y - transform.position.y);
        return direction;
    }
}

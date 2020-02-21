using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterRoomScript : MonoBehaviour
{

    public GameObject cover;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(cover, transform.position, Quaternion.identity);
    }

    // Update is called once per frame
}

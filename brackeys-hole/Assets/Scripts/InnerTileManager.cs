using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InnerTileManager : MonoBehaviour
{
    public GameObject[] enemies;
    // Start is called before the first frame update
    private GameObject[] objects;
    void Start()
    {
        objects = Resources.LoadAll<GameObject>("Tiles\\");
        enemies =  Resources.LoadAll<GameObject>("Enemies\\");
        int randTile = Random.Range(0,objects.Length -1);
        int randEnemy = Random.Range(0, enemies.Length -1);
        int coin = Random.Range(0,48);
        if(coin == 1 || coin == 2) Instantiate(objects[randTile], transform.position, Quaternion.identity);
        if(coin == 3) Instantiate(enemies[randEnemy],transform.position,Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

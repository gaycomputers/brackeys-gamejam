using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GameObject player;
    public Transform[] startingPositions;
    public GameObject[] Rooms;
    public GameObject[] roomsDownLeft;//the rooms coming from a down and going to a left
    public GameObject[] roomsDownRight;
    public GameObject[] roomsLeftLeft;
    public GameObject[] roomsRightRight;
    public GameObject[] roomsUpLeft;
    public GameObject[] roomsUpRight;
    public GameObject[] roomsDownDown;
    public GameObject[] roomsRightDown;
    public GameObject[] roomsLeftDown;
    public GameObject[] roomsUpUp;
    public GameObject[] roomsRightUp;
    public GameObject[] roomsLeftUp;
    public GameObject lastRoom;
    public GameObject startRoom;
    public GameObject pathRoom;
    public GameObject[] finishingPositions;
    private List<Vector2> roomsVisited = new List<Vector2>();
    private int distanceBetweenCenters = 20;
    private float minX = 10f;
    private float minY = 10f;
    private float maxX = 190f;
    private float maxY = 190f;
    private int lastDirection = 6;
    // Start is called before the first frame update
    private int direction;
    private bool stopGen = false;
    private float timePerRoom = .1f;
    private float timePassed = 0;
    private GameObject previouslyMadeRoom;
    private List<GameObject> backRooms = new List<GameObject>();
    private bool cleaned;
    void Start()
    {
        int startingPosIndex = Random.Range(0, startingPositions.Length - 1);
        transform.position = startingPositions[startingPosIndex].position;
        player.transform.Translate(new Vector2(transform.position.x, transform.position.y)); //what the fuck is happening,
        //player.transform.position = transform.position;
        Instantiate(startRoom, transform.position, Quaternion.identity);
        roomsVisited.Add(new Vector2(transform.position.x, transform.position.y));
    }
    private void Move()
    {
        transform.position = GetDirection();
        if (!stopGen)
        {
            Destroy(previouslyMadeRoom);
            previouslyMadeRoom = Instantiate(pathRoom, transform.position, Quaternion.identity);
        }
        else
        {
            //this only gets called on the last one
            Destroy(previouslyMadeRoom);
            Instantiate(lastRoom, transform.position, Quaternion.identity); //this isnt a room its actually just a trigger that ends the level
            for (int i = 0; i < finishingPositions.Length; i++)
            {
                if (!roomsVisited.Contains(new Vector2(finishingPositions[i].transform.position.x, finishingPositions[i].transform.position.y)))
                {
                    int roomNum = Random.Range(0, Rooms.Length);
                    backRooms.Add(Instantiate(Rooms[roomNum], finishingPositions[i].transform.position, Quaternion.identity));
                }
            }
        }
    }
    private Vector2 GetDirection()
    {
        Vector2 right = new Vector2(transform.position.x + distanceBetweenCenters, transform.position.y);
        Vector2 left = new Vector2(transform.position.x - distanceBetweenCenters, transform.position.y);
        Vector2 down = new Vector2(transform.position.x, transform.position.y - distanceBetweenCenters);
        Vector2 up = new Vector2(transform.position.x, transform.position.y + distanceBetweenCenters);
        while (true)
        {
            direction = Random.Range(1, 7);
            if ((direction == 1 || direction == 2) && !roomsVisited.Contains(right) && (right.x <= maxX))
            {
                roomsVisited.Add(right);
                return right;
            }
            if ((direction == 3 || direction == 4) && !roomsVisited.Contains(left) && (left.x >= minX))
            {
                roomsVisited.Add(left);
                return left;
            }
            if (direction == 5 && !roomsVisited.Contains(down) && (down.y >= minY))
            {
                roomsVisited.Add(down);
                return down;
            }
            if (direction == 6 && !roomsVisited.Contains(up) && (up.y <= maxY))
            {
                roomsVisited.Add(up);
                return up;
            }
            if ((roomsVisited.Contains(up) || (up.y > maxY)) && (roomsVisited.Contains(down) || (down.y < minY)) && (roomsVisited.Contains(left) || (left.x < minX)) && (roomsVisited.Contains(right) || (right.x > maxX)))
            {
                stopGen = true;
                return new Vector2(left.x + distanceBetweenCenters, left.y);
            }
        }
    }
    /* //nightmare from perfect level attempt
    private void Move(){
        Vector2 right = new Vector2(transform.position.x + distanceBetweenCenters, transform.position.y);
        Vector2 left = new Vector2(transform.position.x - distanceBetweenCenters, transform.position.y);
        Vector2 down = new Vector2(transform.position.x, transform.position.y - distanceBetweenCenters) ;
        Vector2 up = new Vector2(transform.position.x, transform.position.y + distanceBetweenCenters) ;
        if((direction == 1 || direction == 2) ){ //right //check if not intruding on old square
            if(transform.position.x < maxX && !roomsVisited.Contains(right)){
            transform.position = right;
                if(lastDirection == 1|| lastDirection ==2){
        lastDirection = direction;
                    Instantiate(roomsRightRight[0], transform.position, Quaternion.identity);
                }
                else if(lastDirection == 3 || lastDirection == 4){
                    //cant exist
                }
                else if(lastDirection == 5){
        lastDirection = direction;
                    Instantiate(roomsDownRight[0], transform.position, Quaternion.identity);
                }
                else if(lastDirection == 6){
        lastDirection = direction;
                    Instantiate(roomsUpRight[0], transform.position, Quaternion.identity);
                }
            }
            else direction = 3;
        }
        else if((direction == 3 || direction == 4) ){ //left
            if(transform.position.x > minX && !roomsVisited.Contains(left)){
            transform.position = left;
                if(lastDirection == 1|| lastDirection ==2){
                    //cant exist
                }
                else if(lastDirection == 3 || lastDirection == 4){
        lastDirection = direction;
                    Instantiate(roomsLeftLeft[0], transform.position, Quaternion.identity);
                }
                else if(lastDirection == 5){
        lastDirection = direction;
                    Instantiate(roomsDownLeft[0], transform.position, Quaternion.identity);
                }
                else if(lastDirection == 6){
        lastDirection = direction;
                    Instantiate(roomsUpLeft[0], transform.position, Quaternion.identity);
                }
            }
            else direction = 5;
        }
        else if(direction == 5 ){ //down
            if(transform.position.y > minY && !roomsVisited.Contains(down)){
            transform.position = down;
                if(lastDirection == 1|| lastDirection ==2){
        lastDirection = direction;
                    Instantiate(roomsRightDown[0], transform.position, Quaternion.identity);
                }
                else if(lastDirection == 3 || lastDirection == 4){
        lastDirection = direction;
                    Instantiate(roomsLeftDown[0], transform.position, Quaternion.identity);
                }
                else if(lastDirection == 5){
        lastDirection = direction;
                    Instantiate(roomsDownDown[0], transform.position, Quaternion.identity);
                }
                else if(lastDirection == 6){
                    //cant exist
                }
            }
            else direction = 6;
        }
        else if(direction == 6 ){ //up
            if(transform.position.y < maxY && !roomsVisited.Contains(up)){
            transform.position = up;
                if(lastDirection == 1|| lastDirection ==2){
        lastDirection = direction;
                    Instantiate(roomsRightUp[0], transform.position, Quaternion.identity);
                }
                else if(lastDirection == 3 || lastDirection == 4){
        lastDirection = direction;
                    Instantiate(roomsLeftUp[0], transform.position, Quaternion.identity);
                }
                else if(lastDirection == 5){
                    //cantexist
                }
                else if(lastDirection == 6){
        lastDirection = direction;
                    Instantiate(roomsUpUp[0], transform.position, Quaternion.identity);
                }
            }
            else{
                stopGen = true;
                //oops we got stuck, make it the end, make a 
                Debug.Log("hit end of gen");
                    Instantiate(lastRoom,transform.position, Quaternion.identity);
            }
        }
        else if(!roomsVisited.Contains(up) && !roomsVisited.Contains(down) && !roomsVisited.Contains(left) && !roomsVisited.Contains(right)){
                stopGen = true;
                //oops we got stuck, make it the end, make a 
                Debug.Log("hit end of gen");
                    Instantiate(lastRoom,transform.position, Quaternion.identity);
        }
        else{
            Instantiate(lastRoom,transform.position, Quaternion.identity);
        }
        Debug.Log(transform.position);
        roomsVisited.Add(new Vector2(transform.position.x, transform.position.y));
        direction = Random.Range(1,6);
    }*/
    // Update is called once per frame
    void FixedUpdate()
    {
        if (!stopGen)
        {
            if (timePassed > timePerRoom && stopGen == false)
            {
                timePassed = 0f;
                Move();
            }
            timePassed += Time.deltaTime;
        }
        else
        {
            if (!cleaned)
            {
                foreach (GameObject room in backRooms)
                {
                    Destroy(room);
                }
                cleaned = true;
            }

        }
    }
}

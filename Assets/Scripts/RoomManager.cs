﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomManager : MonoBehaviour
{
    public enum Direction { up, down, left, right };
    public Direction direction;

    [Header("房间信息")]
    public GameObject roomPrefab;
    public GameObject[] enemyTiles;
    public GameObject[] npcTiles;
    public GameObject[] furnitureTiles;
    public GameObject[] foodTiles;
    public GameObject[] bossTiles;
    public GameObject[] exitTiles;
    public GameObject[] keyTiles;
    public int roomNumber;
    private GameObject endRoom;
    private GameObject bossRoom;
    List <Vector3> gridPositions = new List<Vector3>();

    [Header("位置控制")]
    public Transform managerPoint;
    public float xOffset;
    public float yOffset;
    public LayerMask roomLayer;
    public List<Room> rooms = new List <Room>();
    public Walltype walltype;

     public void SetupScene(int level)
    {
        rooms.Clear();
        managerPoint.position = new Vector3(0, 0, 0);

        InitialiseList();
        int enemyCount = (int)Mathf.Log(level, 2f);
        LayoutObjectAtRandom(npcTiles, 1, 1);

        for (int i = 0; i < roomNumber; i++)
        {
            rooms.Add(Instantiate(roomPrefab, managerPoint.position, Quaternion.identity).GetComponent<Room>());
            //在point上生成示例房间
            ChangePointPos();
            //改变point位置
            LayoutObjectAtRandom(enemyTiles, enemyCount + 2, enemyCount + 3);
            LayoutObjectAtRandom(foodTiles, 1, 1);
            LayoutObjectAtRandom(furnitureTiles, 5, 8);
            LayoutObjectAtRandom(keyTiles, 0, 1);
        }

        endRoom = rooms[0].gameObject;

        foreach (var room in rooms)
        {
            if (room.transform.position.sqrMagnitude > endRoom.transform.position.sqrMagnitude)
            {
                endRoom = room.gameObject;
            }
            //找到距离初始房间相对最远的房间
        }

        managerPoint.position = endRoom.transform.position;
        ChangePointPos();
        rooms.Add(Instantiate(roomPrefab, managerPoint.position, Quaternion.identity).GetComponent<Room>());
        bossRoom = rooms[roomNumber].gameObject;
        SetupRoom(endRoom.GetComponent<Room>(), endRoom.GetComponent<Room>().transform.position);
        LayoutObjectAtRandom(bossTiles, 1, 1);
        LayoutObjectAtRandom(exitTiles, 1, 1);

        //在最远房间相邻处生成一个boss房间

         foreach (var room in rooms)
         {
             SetupRoom(room, room.transform.position);
         }
         //生成门
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.anyKeyDown)
        //{
        //    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //}
    }

    public void ChangePointPos()
    {
        do
        {
            direction = (Direction)Random.Range(0, 4);

            switch(direction)
            {
                case Direction.up:
                    managerPoint.position += new Vector3(0, yOffset, 0);
                    break;
                case Direction.down:
                    managerPoint.position += new Vector3(0, -yOffset, 0);
                    break;
                case Direction.left:
                    managerPoint.position += new Vector3(-xOffset, 0, 0);
                    break;
                case Direction.right:
                    managerPoint.position += new Vector3(xOffset, 0, 0);
                    break;
            }
        } while (Physics2D.OverlapCircle(managerPoint.position, 0.2f, roomLayer));

    }


    public void SetupRoom(Room newRoom, Vector3 roomPosition)
    {
        newRoom.roomUp = Physics2D.OverlapCircle(roomPosition + new Vector3(0, yOffset, 0), 0.2f, roomLayer);
        newRoom.roomDown = Physics2D.OverlapCircle(roomPosition + new Vector3(0, -yOffset, 0), 0.2f, roomLayer);
        newRoom.roomLeft = Physics2D.OverlapCircle(roomPosition + new Vector3(-xOffset, 0, 0), 0.2f, roomLayer);
        newRoom.roomRight = Physics2D.OverlapCircle(roomPosition + new Vector3(xOffset, 0, 0), 0.2f, roomLayer);

        newRoom.CountRoom();

        switch(newRoom.doorNumber)
        {
            case 1:
                if(newRoom.roomUp)
                    Instantiate(walltype.singleUp, roomPosition, Quaternion.identity);
                if(newRoom.roomDown)
                    Instantiate(walltype.singleDown, roomPosition, Quaternion.identity);
                if(newRoom.roomLeft)
                    Instantiate(walltype.singleLeft, roomPosition, Quaternion.identity);
                if(newRoom.roomRight)
                    Instantiate(walltype.singleRight, roomPosition, Quaternion.identity);
                break;
            case 2:
                if(newRoom.roomLeft && newRoom.roomUp)
                    Instantiate(walltype.doubleUL, roomPosition, Quaternion.identity);
                if(newRoom.roomUp && newRoom.roomRight)
                    Instantiate(walltype.doubleUR, roomPosition, Quaternion.identity);
                if(newRoom.roomRight && newRoom.roomDown)
                    Instantiate(walltype.doubleRD, roomPosition, Quaternion.identity);
                if(newRoom.roomDown && newRoom.roomLeft)
                    Instantiate(walltype.doubleLD, roomPosition, Quaternion.identity);
                if(newRoom.roomLeft && newRoom.roomRight)
                    Instantiate(walltype.doubleLR, roomPosition, Quaternion.identity);
                if(newRoom.roomDown && newRoom.roomUp)
                    Instantiate(walltype.doubleUD, roomPosition, Quaternion.identity);
                break;
            case 3:
                if(newRoom.roomLeft && newRoom.roomUp && newRoom.roomRight)
                    Instantiate(walltype.tripleULR, roomPosition, Quaternion.identity);
                if(newRoom.roomLeft && newRoom.roomUp && newRoom.roomDown)
                    Instantiate(walltype.tripleULD, roomPosition, Quaternion.identity);
                if(newRoom.roomLeft && newRoom.roomDown && newRoom.roomRight)
                    Instantiate(walltype.tripleLRD, roomPosition, Quaternion.identity);
                if(newRoom.roomDown && newRoom.roomUp && newRoom.roomRight)
                    Instantiate(walltype.tripleURD, roomPosition, Quaternion.identity);
                break;
            case 4:
                if(newRoom.roomLeft && newRoom.roomUp && newRoom.roomRight && newRoom.roomDown)
                    Instantiate(walltype.fourDoors, roomPosition, Quaternion.identity);
                break;
        }
    }

    [System.Serializable]
    public class Walltype
    {
        public GameObject singleLeft, singleRight, singleUp, singleDown,
                          doubleUL, doubleLR, doubleLD, doubleUR, doubleUD, doubleRD,
                          tripleULR, tripleULD, tripleURD, tripleLRD,
                          fourDoors;
    }


    void InitialiseList()
    {
        gridPositions.Clear();
        for (int x = 1; x < 17; x++)
        {
            for(int y = 1; y < 9; y++)
            {
                gridPositions.Add(new Vector3(x, y, 0f));
            }
        }
    }


    Vector3 RandomPosition()
    {
        int RandomIndex = Random.Range (0, gridPositions.Count);
        Vector3 randomPosition = gridPositions [RandomIndex];
        gridPositions.RemoveAt (RandomIndex);
        return randomPosition;
    }


    void LayoutObjectAtRandom (GameObject[] tileArray, int minimum, int maximum)
    {
        int objectCount = Random.Range(minimum, maximum + 1);
        for (int i = 0; i < objectCount; i++)
        {
            Vector3 randomPosition = RandomPosition();
            GameObject tileChoice = tileArray [Random.Range(0, tileArray.Length)];
            Instantiate (tileChoice, randomPosition + managerPoint.position + new Vector3(-8.5f, -3.5f, 0f), Quaternion.identity);
        }
    }
}

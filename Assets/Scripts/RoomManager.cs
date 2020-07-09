using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomManager : MonoBehaviour
{
    public enum Direction { up, down, left, right };
    public Direction direction;

    [Header("房间信息")]
    public GameObject roomPrefab;
    public int roomNumber;
    public Color startColor, endColor;
    private GameObject endRoom;
    private GameObject bossRoom;

    [Header("位置控制")]
    public Transform managerPoint;
    public float xOffset;
    public float yOffset;
    public LayerMask roomLayer;
    public List<Room> rooms = new List <Room>();
    public Walltype walltype;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < roomNumber; i++)
        {
            rooms.Add(Instantiate(roomPrefab, managerPoint.position, Quaternion.identity).GetComponent<Room>());
            //在point上生成示例房间
            ChangePointPos();
            //改变point位置
        }

        rooms[0].GetComponent<SpriteRenderer>().color = startColor;

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
        bossRoom.GetComponent<SpriteRenderer>().color = endColor;
        SetupRoom(endRoom.GetComponent<Room>(), endRoom.GetComponent<Room>().transform.position);
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
}

using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public int width;
    public int height;
    public int x;
    public int y;

    private bool updatedDoors = false;

    // Stores the coordinates of this room
    public Room(int _x, int _y)
    {
        x = _x;
        y = _y;
    }

    public Door leftDoor;
    public Door rightDoor;
    public Door topDoor;
    public Door bottomDoor;

    public GameObject leftWall;
    public GameObject rightWall;
    public GameObject topWall;
    public GameObject bottomWall;

    public List<Door> doors = new List<Door>();

    // Start is called before the first frame update
    void Start()
    {
        gameObject.layer = LayerMask.NameToLayer("Default");
        // For whenever you forget where to start the scene from
        if (RoomController.instance == null)
        {
            Debug.Log("Pressed play in the wrong scene!");
            return;
        }

        Door[] ds = GetComponentsInChildren<Door>();
        foreach (Door d in ds)
        {
            doors.Add(d);
            switch (d.doorType)
            {
                case Door.DoorType.right:
                    rightDoor = d;
                    break;
                case Door.DoorType.left:
                    leftDoor = d;
                    break;
                case Door.DoorType.top:
                    topDoor = d;
                    break;
                case Door.DoorType.bottom:
                    bottomDoor = d;
                    break;
            }
        }

        RoomController.instance.RegisterRoom(this);
    }

    // Updates the room and removes doors which doesn't lead to anywhere
    // Spawns in walls to replace the doors
    public void RemoveUnconnectedDoors()
    {
        foreach (Door door in doors)
        {
            switch (door.doorType)
            {
                case Door.DoorType.right:
                    if (getRight() == null)
                    {
                        door.gameObject.SetActive(false);
                        rightWall.SetActive(true);
                    }
                    break;
                case Door.DoorType.left:
                    if (getLeft() == null)
                    {
                        door.gameObject.SetActive(false);
                        leftWall.SetActive(true);
                    }
                    break;
                case Door.DoorType.top:
                    if (getTop() == null)
                    {
                        door.gameObject.SetActive(false);
                        topWall.SetActive(true);
                    }
                    break;
                case Door.DoorType.bottom:
                    if (getBottom() == null)
                    {
                        door.gameObject.SetActive(false);
                        bottomWall.SetActive(true);
                    }
                    break;
            }
        }
    }

    // Checks if there is a room next to them
    #region AdjacentRoomChecker
    public Room getLeft()
    {
        if (RoomController.instance.DoesRoomExist(x - 1, y))
        {
            return RoomController.instance.FindRoom(x - 1, y);
        }
        return null;
    }
    public Room getRight()
    {
        if (RoomController.instance.DoesRoomExist(x + 1, y))
        {
            return RoomController.instance.FindRoom(x + 1, y);
        }
        return null;
    }
    public Room getTop()
    {
        if (RoomController.instance.DoesRoomExist(x, y + 1))
        {
            return RoomController.instance.FindRoom(x, y + 1);
        }
        return null;
    }
    public Room getBottom()
    {
        if (RoomController.instance.DoesRoomExist(x, y - 1))
        {
            return RoomController.instance.FindRoom(x, y - 1);
        }
        return null;
    }
    #endregion

    // Update is called once per frame
    void Update()
    {
        if (name.Contains("End") && !updatedDoors)
        {
            RemoveUnconnectedDoors();
            updatedDoors = true;
        }
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireCube(transform.position, new Vector3(width, height, 0));
    //}

    // Gets the centre of the room
    public Vector3 GetRoomCenter()
    {
        return new Vector3(x * width, y * height);
    }

    // Checks if the player has entered the room
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            RoomController.instance.OnPlayerEnterRoom(this);
            gameObject.layer = LayerMask.NameToLayer("Minimap");
            Debug.Log("PLAYER HAS ENTERED ROOM");
            foreach (Transform child in transform)
            {
                child.gameObject.layer = LayerMask.NameToLayer("Minimap");
            }
        }
    }
}

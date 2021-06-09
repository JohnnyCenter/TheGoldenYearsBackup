using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    public List<GameObject> doors;
    public List<GameObject> tpRooms;
    public List<Transform> tpPoints;
    public List<Material> wallpapers;
    
    /// <summary>
    /// DoorIndex 0 = Hall>Livingroom door located in Hallway,
    /// tpPointIndex 0 = Hall>Livingroom point located outside Bathroom,
    /// Therefore:
    /// <list type="bullet">
    /// <item>
    /// <description>0 = Hall>LivingRoom, </description>
    /// </item>
    /// <item>
    /// <description>1 = Hall>Kitchen,</description>
    /// </item>
    /// <item>
    /// <description>2 = Hall>Bathroom</description>
    /// </item>
    /// <item>
    /// <description>3 = LivingRoom>Hall,</description>
    /// </item>
    /// <item>
    /// <description>4 = Livingroom>Bedroom,</description>
    /// </item>
    /// <item>
    /// <description>5 = Livingroom>Kitchen</description>
    /// </item>
    /// <item>
    /// <description>6 = Kitchen>LivingRoom,</description>
    /// </item>
    /// <item>
    /// <description>7 = Kitchen>Hall</description>
    /// </item>
    /// <item>
    /// <description>8 = Bathroom>Hall</description>
    /// </item>
    /// <item>
    /// <description>9 = Bedroom>LivingRoom</description>
    /// </item>
    /// </list>
    /// </summary>
    /// <param name="doorIndex"></param>
    /// <param name="tpPointIndex"></param>
    public void SwapDoors(int doorIndex, int tpPointIndex)
    {
        doors[doorIndex].GetComponent<NOTLonely_Door.DoorScript>().teleportRoom = tpPoints[tpPointIndex].transform;
        if (doorIndex <= 2)
        {
            SetWallPaper1(tpPointIndex, doorIndex, 0);
        }
        if (doorIndex >= 3 && doorIndex <= 5)
        {
            SetWallPaper1(tpPointIndex, doorIndex, 1);
        }
        if (doorIndex >= 6 && doorIndex <= 7)
        {
            SetWallPaper1(tpPointIndex, doorIndex, 2);
        }
        if (doorIndex == 8)
        {
            SetWallPaper1(tpPointIndex, doorIndex, 3);
        }
        if (doorIndex == 9)
        {
            SetWallPaper1(tpPointIndex, doorIndex, 4);
        }
        
    }
    /// <summary>
    /// Resets doors.
    /// </summary>
    public void ResetDoors()
    {
        SwapDoors(0, 0);
        SwapDoors(1, 1);
        SwapDoors(2, 2);
        SwapDoors(3, 3);
        SwapDoors(4, 4);
        SwapDoors(5, 5);
        SwapDoors(6, 6);
        SwapDoors(7, 7);
        SwapDoors(8, 8);
        SwapDoors(9, 9);
    }
    void SetWallPaper1(int tpRoomIndex1, int tpRoomIndex2, int wallPaperIndex)
    {
        Material[] materials = tpRooms[tpRoomIndex1].GetComponent<MeshRenderer>().materials;
        materials[1] = wallpapers[wallPaperIndex];
        tpRooms[tpRoomIndex1].GetComponent<MeshRenderer>().materials = materials;
        if (tpRoomIndex1 <= 2)
        {
            SetWallPaper2(tpRoomIndex2, 0);
        }
        if (tpRoomIndex1 >= 3 && tpRoomIndex1 <= 5)
        {
            SetWallPaper2(tpRoomIndex2, 1);
        }
        if (tpRoomIndex1 >= 6 && tpRoomIndex1 <= 7)
        {
            SetWallPaper2(tpRoomIndex2, 2);
        }
        if (tpRoomIndex1 == 8)
        {
            SetWallPaper2(tpRoomIndex2, 3);
        }
        if (tpRoomIndex1 == 9)
        {
            SetWallPaper2(tpRoomIndex2, 4);
        }
    }
    void SetWallPaper2(int tpRoomIndex, int wallPaperIndex)
    {
        Material[] materials = tpRooms[tpRoomIndex].GetComponent<MeshRenderer>().materials;
        materials[1] = wallpapers[wallPaperIndex];
        tpRooms[tpRoomIndex].GetComponent<MeshRenderer>().materials = materials;
    }
}

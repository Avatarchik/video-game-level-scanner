using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Linq;
using System;

public class RoomPropertiesPanelScript : MonoBehaviour {

    public LevelBuilder Level;
    public Text RoomNrText;
    private const int noColors = 4;
    private int noRooms = 0;
    private int currentRoom;
    public int CurrentRoom { 
        get { return currentRoom; } 
        set 
        { 
            currentRoom = value; 
            RoomNrText.text = (currentRoom+1).ToString(); 
        }
    }
    private bool isInitialized = false;
    public Material[] Materials = new Material[noColors];
    public Button[] Buttons;
   
    public void InitializePanel(int N)
    {
        noRooms = N;
        CurrentRoom = 0;
        isInitialized = true;
    }

    public void SwitchProperty(int option)
    {
        if (isInitialized)
        {
            Level.Rooms[currentRoom].FloorMaterial.color = Materials[option % noColors].color;
        }
    }

    public void SwitchToNextRoom()
    {
        if (isInitialized)
        {
            CurrentRoom = (currentRoom + 1) % noRooms;
        }
    }

    public void SwitchToPrevRoom()
    {
        if (isInitialized)
        {
            if (currentRoom == 0)
                CurrentRoom = noRooms - 1;
            else
                CurrentRoom = currentRoom - 1;
        }
    }	
}

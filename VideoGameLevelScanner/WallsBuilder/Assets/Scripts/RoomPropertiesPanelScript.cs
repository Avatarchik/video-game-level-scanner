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
    public Toggle[] Toggles;
   
    public void InitializePanel(int N)
    {
        noRooms = N;
        CurrentRoom = 0;
        isInitialized = true;
        UpdatePanel();
    }
    public void UpdatePanel()
    {
        if (isInitialized)
        {
            var room = Level.Rooms[currentRoom];
            int toggleIndex = FindMaterialIndex(room.FloorMaterial);
            foreach (var toggle in Toggles.Where(toggle => toggle.isOn == true))
            {
                toggle.isOn = false;
            }
            if (toggleIndex > -1)
	        {
                Toggles[toggleIndex].isOn = true;
		    }
        }
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
            UpdatePanel();
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
            UpdatePanel();
        }
    }
    private int FindMaterialIndex(Material mat)
    {
        return Array.FindIndex(Materials, material => material.color == mat.color);
    }
	
}

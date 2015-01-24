using UnityEngine;
using System.Collections;

public class RespawnScript : MonoBehaviour {

    public LevelBuilder level;
    public GameObject player;
    public Camera playerCamera;
    public GameObject mainCamera;

    public void SpawnPlayer()
    {
        var spawnPoint = level.Rooms[0].floors[0].gameObject.transform;
        var shift = new Vector3(0.5f*level.unit,2.0f,-0.5f*level.unit);
        player.transform.position = spawnPoint.position + shift;

        player.SetActive(true);
        mainCamera.camera.enabled = false;
        playerCamera.enabled = true;
    }
}

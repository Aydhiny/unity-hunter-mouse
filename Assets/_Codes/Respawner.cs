using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawner : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    GameObject PlayerToSpawn;
    Vector3 respawnPosition;
    Transform HeartPanel;

    private void Start()
    {
        HeartPanel = GameObject.Find("HeartPanel").transform;
    }

    public void SetPosition(Vector3 newPost) 
    {
        respawnPosition = newPost;
    }

    void respawn() 
    {
        foreach(Transform child in HeartPanel) 
        {
            Destroy(child.gameObject);
        }
        Instantiate(PlayerToSpawn, respawnPosition, Quaternion.identity); ;
    }

    public void playerIsDead() 
    {
        Invoke("respawn", 1);
    }
}

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

    public AnimationHandler anim;

    public AudioClip deathSound; // Assign the death sound AudioClip in the Inspector

    public AudioSource deathAudioSource;


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
        // Instantiate(PlayerToSpawn, respawnPosition, Quaternion.identity);
        GameObject newPlayer = Instantiate(PlayerToSpawn, respawnPosition, Quaternion.identity);

        // Get the AnimationHandler component from the new player object
        AnimationHandler newPlayerAnim = newPlayer.GetComponent<AnimationHandler>();

        // Assign the src variable to the AnimationHandler component
        if (newPlayerAnim != null)
        {
            newPlayerAnim.src = anim.src;
        }
        if (deathAudioSource != null && deathSound != null)
        {
            deathAudioSource.clip = deathSound;
            deathAudioSource.Play();
        }

        // Restart sound
        anim.RestartSound();
    }

    public void playerIsDead() 
    {
        Invoke("respawn", 1);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class RangeAI : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private float MaxDis;
    private Transform playerTransform;
    private NavMeshAgent agent;
    Vector3 StartPosition;
    public GameObject bomb;
    public Transform cannonPoint;
    float nextBomb;
    public float fireRate;
    public float bombForce = 200;

    public int enemyHP;
    public GameObject DestroyFX;

    public AudioClip explosionSound; // Assign the explosion sound AudioClip in the Inspector
    public AudioSource explosionAudioSource;
    // Start is called before the first frame update
    void Start()
    {
        StartPosition = transform.position;
        agent = GetComponent<NavMeshAgent>();
        explosionAudioSource = GameObject.FindGameObjectWithTag("swordSrc").GetComponent<AudioSource>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(playerTransform != null) 
        {
            float currentDis = Vector3.Distance(transform.position, playerTransform.position);

            if (currentDis <= MaxDis)
            {
                Vector3 targetToLookAt = new Vector3(playerTransform.position.x, transform.position.y, playerTransform.position.z);
                transform.LookAt(targetToLookAt);
                if(Time.time >= nextBomb) 
                {
                    nextBomb = Time.time + 1f / fireRate;
                    Shoot();
                }
            }
            else if(currentDis > MaxDis && currentDis < MaxDis + 8) 
            {
                agent.SetDestination(playerTransform.position);
            }
            else 
            {
                agent.SetDestination(StartPosition);
            }        
        }
        else if (playerTransform == null)
        {
            playerTransform = GameObject.FindGameObjectWithTag("Player")?.transform;
            if (playerTransform == null)
            {
                // Player object is still null, so we cannot proceed further
                return;
            }
        }


        if (enemyHP <= 0)
        {
            Instantiate(DestroyFX, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    void Shoot() 
    {
        GameObject b = Instantiate(bomb, cannonPoint.position, transform.rotation);
        Vector3 dir = playerTransform.position - transform.position;
        dir = dir.normalized;
        b.GetComponent<Rigidbody>().AddForce(dir * bombForce);
        b.GetComponent<Rigidbody>().useGravity = true;
        Destroy(b, 10);
        if (explosionSound != null && explosionAudioSource != null)
        {
            explosionAudioSource.clip = explosionSound;
            explosionAudioSource.PlayOneShot(explosionSound);
        }
    }

    public void TakeDamage(int dmg)
    {
        enemyHP -= dmg;
    }
}

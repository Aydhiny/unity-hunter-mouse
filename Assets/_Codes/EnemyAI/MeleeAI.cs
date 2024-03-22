using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class MeleeAI : MonoBehaviour
{

    [SerializeField]
    private float MaxDis;
    private Transform playerTransform;
    private Vector3 StartPos;
    [SerializeField]
    private float meleeRange = 3;


    private NavMeshAgent agent;

    public int enemyHP;
    public GameObject DestroyFX;

    public Animator SpiderAnimator;
    private bool alive;

    // Start is called before the first frame update
    void Start()
    {
        StartPos = transform.position;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        alive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(playerTransform != null && alive) 
        {
            float currentDis = Vector3.Distance(transform.position, playerTransform.position);

            if (currentDis <= MaxDis && currentDis > meleeRange)
            {
                transform.LookAt(playerTransform.position);

                //go to player
                agent.SetDestination(playerTransform.position);
                SpiderAnimator.SetBool("canWalk", true);
                SpiderAnimator.SetBool("canAttack", false);
            } 

            else if(currentDis <= meleeRange) 
            {
                lookatPlayer();
                SpiderAnimator.SetBool("canWalk", false);
                SpiderAnimator.SetBool("canAttack", true);
                print("Attack!");
            }
            else 
            {
                SpiderAnimator.SetBool("canAttack", false);
                SpiderAnimator.SetBool("canWalk", false);
                agent.SetDestination(StartPos);
            }
        }
        else 
        {
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }

        if(enemyHP <= 0 && alive) 
        {
            alive = false;
            SpiderAnimator.SetTrigger("canDie");
            Instantiate(DestroyFX, transform.position, transform.rotation);
            Destroy(gameObject, 1.5f);
        }
    }
    void lookatPlayer() 
    {
        Vector3 targetToLookAt = new Vector3(playerTransform.position.x, transform.position.y, playerTransform.position.z);
        transform.LookAt(targetToLookAt);
    }

    public void TakeDamage(int dmg) 
    {
        enemyHP -= dmg;
    }
}

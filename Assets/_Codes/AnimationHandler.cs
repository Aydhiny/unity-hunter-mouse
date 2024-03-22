using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    Animator _animator;
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Idle() 
    {
        _animator.SetBool("do_idle", true);
    }
    public void Run() 
    {
        _animator.SetBool("do_idle", false);
    }
    public void Jump() 
    {
        _animator.SetTrigger("do_jump");
    }

    public void AirBorn() 
    {
        _animator.SetBool("do_falling", true);
    }
    public void Landed() 
    {
        _animator.SetBool("do_falling", false);
    }
    public void Attack() 
    {
        _animator.SetTrigger("do_attack");
    }
}

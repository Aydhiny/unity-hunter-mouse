using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    Animator _animator;
    public AudioSource src;
    public AudioClip sfxRun, sfxBlade, sfxJump;

    private bool isAttackSoundPlaying = false;
    private bool isJumpSoundPlaying = false;

    public GameObject jumpEffectPrefab;

    private Coroutine soundCoroutine;

    public PlayerHealthHandler playerHealthHandler;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void Idle()
    {
        _animator.SetBool("do_idle", true);
    }

    public void Run()
    {
        _animator.SetBool("do_idle", false);

        // Start playing the run sound
    }

    public void Jump()
    {
        _animator.SetTrigger("do_jump");
            StartCoroutine(PlayJumpSound());
            Instantiate(jumpEffectPrefab, transform.position, Quaternion.identity);
    }

    IEnumerator PlayJumpSound()
    {
        yield return new WaitForSeconds(0.01f);

        // Play the jump sound
        src.clip = sfxJump;
        src.Play();

        yield return new WaitForSeconds(src.clip.length);

        // Reset flag to indicate that the jump sound has finished playing
        isJumpSoundPlaying = false;
    }
    IEnumerator ResetAttackSoundFlag()
    {
        // Wait for the duration of the attack sound
        yield return new WaitForSeconds(src.clip.length);

        // Reset flag to indicate that the attack sound has finished playing
        isAttackSoundPlaying = false;
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
        if (!isAttackSoundPlaying)
        {
            isAttackSoundPlaying = true;

            src.loop = false;

            src.clip = sfxBlade;
            src.Play();

            StartCoroutine(ResetAttackSoundFlag());
        }
    }

    public void PlayerDied()
    {
       if (playerHealthHandler.PlayerCurrentHealth == 0)
            src.Stop();
    }
    public void RestartSound()
    {
        src.Play();
    }
}

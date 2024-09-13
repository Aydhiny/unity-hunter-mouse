using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    [SerializeField]
    public float playerSpeed = 2.0f;
    [SerializeField]
    public float jumpHeight = 1.0f;
    [SerializeField]
    private float gravityValue = -9.81f;
    AnimationHandler handler;
    public float GroundTimer;

    SkinnedMeshRenderer _skin;
    public Color flashColor;
    private Color normalColor;
    public float intensity;

    private AudioSource audioSource;
    public AudioClip flashSound;

    public float maxSlopeAngle = 120f;
    public float slideSpeed = 3.0f;

    // Start is called before the first frame update
    private void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
        handler = gameObject.GetComponent<AnimationHandler>();
        _skin = GetComponentInChildren<SkinnedMeshRenderer>();
        normalColor = _skin.material.GetVector("_EmissionColor");

        if (audioSource == null)
        {
            GameObject swordSrc = GameObject.FindGameObjectWithTag("swordSrc");
            if (swordSrc != null)
            {
                audioSource = swordSrc.GetComponent<AudioSource>();
            }
            else
            {
                Debug.LogError("AudioSource component with tag 'swordSrc' not found!");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer) 
        {
            GroundTimer = 0.2f;
        }

        if (GroundTimer > 0) 
        {
            GroundTimer -= Time.deltaTime;
            handler.Landed();
        }
        else 
        {
            handler.AirBorn();
        }

        if(groundedPlayer && playerVelocity.y < 0) 
        {
            playerVelocity.y = 0f;
        }
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        controller.Move(move * Time.deltaTime * playerSpeed);

        if(move != Vector3.zero) 
        {
            handler.Run();
            gameObject.transform.forward = move;
        } 
        else 
        {
            handler.Idle();
        }
        if(Input.GetButtonDown("Jump")) 
        {
            if(GroundTimer > 0) 
            {
                GroundTimer = 0;
                handler.Jump();
                playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
            }
        }

        if(Input.GetMouseButtonDown(0)) 
        {
            handler.Attack();
        }
        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }
    public void flashRed() 
    {
        _skin.material.SetVector("_EmissionColor", flashColor * intensity);
        Invoke("normal", 0.12f);

        if (audioSource != null && flashSound != null)
        {
            audioSource.PlayOneShot(flashSound);
        }
    }
    void normal() 
    {
        _skin.material.SetVector("_EmissionColor", normalColor);
    }

    private bool IsOnSteepSlope()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1.1f))
        {
            float slopeAngle = Vector3.Angle(hit.normal, Vector3.up);
            return slopeAngle > maxSlopeAngle;
        }
        return false;
    }
}

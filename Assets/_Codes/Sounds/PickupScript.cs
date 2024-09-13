using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets._Codes.Sounds
{
    public class PickupScript : MonoBehaviour
    {
        public AudioClip pickupSound; // Assign the pickup sound AudioClip in the Inspector
        public AudioSource pickupAudioSource; // Assign the AudioSource component in the Inspector

        private void Start()
        {
            pickupAudioSource = GameObject.FindGameObjectWithTag("pickupSrc").GetComponent<AudioSource>();
        }

        private void OnTriggerEnter(Collider other)
        {
            // Check if the collider is the player
            if (other.CompareTag("Player"))
            {
                // Check if the pickup sound and audio source are assigned
                if (pickupSound != null && pickupAudioSource != null)
                {
                    // Assign the pickup sound to the audio source and play it
                    pickupAudioSource.clip = pickupSound;
                    pickupAudioSource.Play();

                    // Disable the object after pickup (optional)
                    gameObject.SetActive(false);
                }
            }

        }
    }
}
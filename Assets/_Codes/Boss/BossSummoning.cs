using UnityEngine;
using System.Collections;

public class BossSummoning : MonoBehaviour
{
    public GameObject enemyPrefab; // Prefab of the enemy to summon
    public Transform summonPoint; // Point where enemies will be summoned
    public GameObject summonEffectPrefab; // Prefab of the summoning effect
    public float summonInterval = 5f; // Time interval between summons
    public int numberOfEnemies = 3; // Number of enemies to summon each time

    public AudioClip summonSound; // Sound to play when enemies are summoned
    public AudioSource audioSource; // Reference to the AudioSource component

    void Start()
    {
        audioSource = GetComponent<AudioSource>(); // Get the AudioSource component

        // Start summoning enemies periodically
        StartCoroutine(SummonEnemies());
    }

    IEnumerator SummonEnemies()
    {
        // Loop indefinitely
        while (true)
        {
            // Instantiate summoning effect
            Instantiate(summonEffectPrefab, summonPoint.position, Quaternion.identity);

            // Play summon sound
            if (audioSource != null && summonSound != null)
            {
                audioSource.PlayOneShot(summonSound);
            }

            // Wait for a short delay
            yield return new WaitForSeconds(0.5f);

            // Summon enemies
            for (int i = 0; i < numberOfEnemies; i++)
            {
                Instantiate(enemyPrefab, summonPoint.position, Quaternion.identity);
            }

            // Wait for the next summon interval
            yield return new WaitForSeconds(summonInterval);
        }
    }
}
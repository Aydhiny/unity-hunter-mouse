using UnityEngine;

public class LevelBossDestroyHandler : MonoBehaviour
{
    public string[] enemyTagsToDestroy;
    public AudioSource musicAudioSource; 

    void OnDestroy()
    {
        if (gameObject.CompareTag("LevelBoss"))
        {
            foreach (string tagToDestroy in enemyTagsToDestroy)
            {
                GameObject[] enemies = GameObject.FindGameObjectsWithTag(tagToDestroy);
                foreach (GameObject enemy in enemies)
                {
                    Destroy(enemy);
                }
            }

            if (musicAudioSource != null)
            {
                musicAudioSource.Stop();
            }
        }
    }
}
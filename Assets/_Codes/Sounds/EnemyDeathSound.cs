using UnityEngine;

public class EnemyDeathSound : MonoBehaviour
{
    private void OnDestroy()
    {
        AudioManager.instance.PlaySound("EnemyDeath");
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using TMPro;
using System.Collections;

public class CheckpointDisplay : MonoBehaviour
{
    public AudioClip checkpointSound;
    private AudioSource audioSource;
    public TextMeshProUGUI checkpointText;

    private void Start()
    {
        audioSource = GameObject.FindGameObjectWithTag("checkpointSrc").GetComponent<AudioSource>();
        checkpointText.alpha = 0f;
        checkpointText.gameObject.SetActive(false);
    }

    public void DisplayCheckpointText()
    {
            checkpointText.text = "New checkpoint reached!";
            checkpointText.gameObject.SetActive(true);
            StartCoroutine(FadeInCheckpointText());
            if (checkpointSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(checkpointSound);
            }
    }

    private IEnumerator FadeInCheckpointText()
    {
        float duration = 1f;
        float timer = 0f;

        while (timer < duration)
        {
            float alpha = Mathf.Lerp(0f, 1f, timer / duration);

            checkpointText.alpha = alpha;

            timer += Time.deltaTime;

            yield return null; 
        }

        checkpointText.alpha = 1f;

        StartCoroutine(HideCheckpointText());
    }

    private IEnumerator HideCheckpointText()
    {
        yield return new WaitForSeconds(4f); 

        StartCoroutine(FadeOutCheckpointText());
    }

    private IEnumerator FadeOutCheckpointText()
    {
        float duration = 1f; 
        float timer = 0f;

        while (timer < duration)
        {
            float alpha = Mathf.Lerp(1f, 0f, timer / duration);

            checkpointText.alpha = alpha;

            timer += Time.deltaTime;

            yield return null;
        }

        checkpointText.alpha = 0f;

        checkpointText.gameObject.SetActive(false);
    }
}
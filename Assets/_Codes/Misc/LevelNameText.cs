using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using UnityEngine;
using TMPro;
public class LevelNameText : MonoBehaviour
{
    private TextMeshProUGUI levelNameText;

    private void Start()
    {
        levelNameText = GetComponent<TextMeshProUGUI>();

        StartCoroutine(AnimateText());
    }

    private IEnumerator AnimateText()
    {
        float duration = 4f;
        float timer = 0f;

        while (timer < duration)
        {
            float alpha = Mathf.Lerp(1f, 0f, timer / duration);

            levelNameText.alpha = alpha;

            timer += Time.deltaTime;

            yield return null; 
        }

        levelNameText.alpha = 0f;

        gameObject.SetActive(false);
    }
}

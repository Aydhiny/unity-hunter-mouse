using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;
using UnityEngine;
public class loadingScript : MonoBehaviour
{
    public GameObject LoadScreen;

    private void Start()
    {
        LoadScreen.SetActive(false);
    }
    public void LoadScene(int sceneId)
    {
        StartCoroutine(LoadSceneAsync(sceneId));
    }

    IEnumerator LoadSceneAsync(int sceneId)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneId);
        LoadScreen.SetActive(true);
        while (operation.isDone) 
        {
            yield return null;
        }
    }
}

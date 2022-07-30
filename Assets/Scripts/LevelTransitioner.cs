using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTransitioner : MonoBehaviour
{
    public void LoadLevel(string levelName)
    {
        gameObject.GetComponent<Animator>().SetTrigger("FadeOut");
        StartCoroutine(waiter(levelName));  
    }

    IEnumerator waiter(string levelName)
    {
        yield return new WaitForSecondsRealtime(1);
        SceneManager.LoadScene(levelName);
    }
}

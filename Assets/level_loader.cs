using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class level_loader : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator transition;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadNextLevel(){
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void LoadaLevel(int num){
        StartCoroutine(LoadLevel(num));
    }

    IEnumerator LoadLevel(int levelIndex){
        transition.SetTrigger("start");
        yield return new WaitForSeconds(0.5f);
        transition.SetTrigger("spood");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(levelIndex);
    }

    public void quit(){
        Application.Quit();
    }
}

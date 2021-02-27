using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class pausemenu : MonoBehaviour
{
    // Start is called before the first frame update

    public static bool Paused = false;

    public GameObject pauseui;
    level_loader load;
    
    void Start()
    {
        pauseui.SetActive(false);
        GameObject levelLoad = GameObject.Find("level loader");
        load = levelLoad.GetComponent("level_loader") as level_loader;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(Paused){
                Resume();
            }
            else{
                Pause();
            }
        }
    }

    void Resume(){
        pauseui.SetActive(false);
        Time.timeScale = 1f;
        Paused = false;
    }

    void Pause(){
        pauseui.SetActive(true);
        Time.timeScale = 0f;
        Paused = true;
    }

    public void Menu(){
        print("menu");
        Time.timeScale = 1f;
        pauseui.SetActive(false);
        load.LoadaLevel(0); 
    }
    public void Restart(){
        Time.timeScale = 1f;
        pauseui.SetActive(false);
        int sceneID = SceneManager.GetActiveScene().buildIndex;
        load.LoadaLevel(sceneID); 
    }
}

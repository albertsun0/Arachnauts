using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class felixtalk : MonoBehaviour
{
    // Start is called before the first frame update

    int index = 0;

    string[] text = {"Hi my name is Felix. ", "I’m a spider over at the ISS. Me and my buds, Francis and baby Felicia, were brought here against our will!", "I’m gonna do whatever it takes to bring my family back to Earth!" };
    
    Text instruction;
level_loader load;
    void Start()
    {
        instruction = GetComponent<Text>();
         GameObject levelLoad = GameObject.Find("level loader");
        load = levelLoad.GetComponent("level_loader") as level_loader;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void nextline(){
        index++;
        if(index == 3){
            load.LoadNextLevel();
        }
        instruction.text = text[index];
    }
}

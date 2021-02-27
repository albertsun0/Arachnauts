using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spiderswap : MonoBehaviour
{

    public string[] arr;
    public string selectedSpider = "spider";
    // Start is called before the first frame update

    public int selected = 0;

    Vector3 lastPosition;
    void Start()
    {
        selectedSpider = arr[selected];
    }
    private int index;


    public GameObject spooder;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.LeftShift)){
            selected++;
            if(selected >= arr.Length){
                selected = 0;
            }
            selectedSpider =  arr[selected];
            print(selected);
            spooder = GameObject.Find(arr[selected]);
            StartCoroutine(LerpPosition(spooder.transform.position + new Vector3(0,0.6f,0), 0.2f));
            
        }

        spooder = GameObject.Find(arr[selected]);
        //if(lastPosition == spooder.transform.position){
            transform.position = spooder.transform.position + new Vector3(0,0.6f,0);
        //}
        /*else{
            transform.position = new Vector3(0,0,-100);
        }
        lastPosition = spooder.transform.position;*/
        
    }

     IEnumerator LerpPosition(Vector2 targetPosition, float duration)
    {
        float time = 0;
        Vector2 startPosition = transform.position;

        while (time < duration)
        {
            transform.position = Vector2.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPosition;
    }
}

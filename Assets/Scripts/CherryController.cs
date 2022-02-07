using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CherryController : MonoBehaviour
{
    [SerializeField] private GameObject cherry;
    
    void Start()
    {
        cherry.SetActive(false);
    }
    void Show()
    {
        cherry.SetActive(true);
        Invoke("Hide",20f);
    }
    void Hide()
    {
        cherry.SetActive(false); 
        //Invoke("Show",1f);

    }


    // Update is called once per frame
    void Update()
    {
        Debug.Log("Score: "+ScoreManager.instance.getScore());
         if (ScoreManager.instance.getScore()==10||ScoreManager.instance.getScore()==100||ScoreManager.instance.getScore()==300)
         {
             Invoke("Show",1f);
         }
         
    }
}

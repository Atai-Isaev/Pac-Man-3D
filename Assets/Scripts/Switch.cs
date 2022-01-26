using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Switch : MonoBehaviour
{
    public GameObject mainCam;
    public GameObject debugCam;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("1Key"))
        {
            mainCam.SetActive(true);
            debugCam.SetActive(false);
        }
        if (Input.GetButtonDown("2Key"))
        {
            mainCam.SetActive(false);
            debugCam.SetActive(true);
        }
      
        
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetManager : MonoBehaviour
{
    public GameObject[] ghosts;
    public static ResetManager instance;

    private void Awake()
    {
        instance = this;
    }

    public void Reset()
    {
        for(int i = 0; i < ghosts.Length; i++)
        {
            GhostController controller = ghosts[i].GetComponent<GhostController>();
            if (controller.IsActivated())
            {
                controller.Reset();
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}

using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class BoosterManager : MonoBehaviour
{
    public GameObject[] ghosts;
    public static BoosterManager instance;

    private void Awake()
    {
        instance = this;
    }

    public async Task StartBoosterModeAsync()
    {
        for(int i = 0; i < ghosts.Length; i++)
        {
            GhostController controller = ghosts[i].GetComponent<GhostController>();
            if (controller.IsActivated()) {
                controller.StartScaredMode();
            }
        }
        await Task.Delay(7 * 1000);
        EndBoosterMode();
    }

    public void EndBoosterMode()
    {
        for (int i = 0; i < ghosts.Length; i++)
        { 
            GhostController controller = ghosts[i].GetComponent<GhostController>();
            if (controller.IsScared())
            {
                controller.StartPatrolMode();
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

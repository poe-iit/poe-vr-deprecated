using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightToggle : MonoBehaviour
{
    //public GameObject[] Lights;
    public Light[] Lights;
    public float timeToWait = 5;
    public float timeUsed;
    // Start is called before the first frame update
    void Start()
    {
        timeUsed = timeToWait;
    }

    // Update is called once per frame
    void Update()
    {
        
        for (int i = 0; i < Lights.Length;)
        {
            timeUsed -= Time.deltaTime;
            Debug.Log(timeUsed);
            if (timeUsed>=timeToWait) {
                Lights[i].enabled = !Lights[i].enabled;
                i++;
                timeUsed=timeToWait;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class addLightToList : MonoBehaviour
{
    // Start is called before the first frame updat
    public Light ceilingLight;
    public GameObject server;
    void Start()
    {
        server = GameObject.FindGameObjectWithTag("server");
        server.gameObject.SendMessage("addAllLights", ceilingLight);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.tag);
        if (other.gameObject.tag == "nav_agent")
        {
            Debug.Log("Passing: "+ other.gameObject.tag);
            other.gameObject.SendMessage("passAddition", ceilingLight);

        }
    }
}

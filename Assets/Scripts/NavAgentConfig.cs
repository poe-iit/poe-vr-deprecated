using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavAgentConfig : MonoBehaviour
{
    // Start is called before the first frame update
    public NavMeshAgent agent;
    public GameObject server;
    
    void Start()
    {
        server = GameObject.FindGameObjectWithTag("server");
    }

    // Update is called once per frame
    void Update()
    {
        if (agent.remainingDistance == 0)
        {
            Debug.Log("AGENT STOPPED");
            server.SendMessage("doSomethingWithLights");
        }
        //Debug.Log("Remaining Distance: " + agent.remainingDistance);
    }

    public void setPath(NavMeshPath chosenPath)
    {
        agent.path=chosenPath;
    }

    

    public void passAddition( Light light)
    {
        server.SendMessage("addToLightPath", light);
    }
}

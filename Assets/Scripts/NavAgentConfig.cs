using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavAgentConfig : MonoBehaviour
{
    // Start is called before the first frame update
    public NavMeshAgent agent;
    public GameObject server;
    public int agentIndex;
    public bool resetPos = false;
    public bool pathFinding = false;

    void Start()
    {
        server = GameObject.FindGameObjectWithTag("server");
    }

    // Update is called once per frame
    void Update()
    {
        
        Debug.Log("reaminging distance "+ agent.remainingDistance);
        if (agent.remainingDistance == 0 && pathFinding)
        {
            Debug.Log("AGENT STOPPED");
            server.SendMessage("doSomethingWithLights");
        }
        //Debug.Log("Remaining Distance: " + agent.remainingDistance);
    }

    public void setPath(NavMeshPath chosenPath)
    {
        //removePath();
        //resetLocation();
        agent.SetPath(chosenPath);
        //Debug.Log("PATH IS SET ON AGENT = "+ (agent.path = chosenPath));
        //agent.isStopped = false;
    }
    public void removePath()
    {
        //agent.isStopped = true;
        agent.ResetPath();
       
    }


    public void passAddition(Light light)
    {
        Debug.Log("PASSING LIGHT AGAIN");
        server.SendMessage("addToLightPath", light);
    }

    public void setAgentIndex(int index)
    {
        agentIndex = index;
    }

    public void resetLocation()
    {
        Vector3 parentLocation = this.transform.parent.position;
        //this.transform.position = parentLocation;
        agent.Warp(parentLocation);
        //this.transform.position = new Vector3(0, 1, 0);
    }

    public void setPathFinding(bool value)
    {
        pathFinding = value;
        if (pathFinding==false)
        {
            agent.ResetPath();
            resetLocation();
        }
        //Debug.Log("PAHTH FINDING VARIABLE SET");
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.Extras;

public class FingerPointer : MonoBehaviour
{
    public SteamVR_LaserPointer laserPointer;
    public GameObject fire;
    public bool firegun = true;
    public GameObject[] fires;
    public int numFires = 1;
    public GameObject server;

    // Start is called before the first frame update
    void Start()
    {
        laserPointer.PointerIn += PointerInside;
        laserPointer.PointerOut += PointerOutside;
        laserPointer.PointerClick += PointerClick;
        server = GameObject.FindGameObjectWithTag("server");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PointerInside(object sender, PointerEventArgs e)
    {
        //Debug.Log("Item tag " + e.target.tag);
        if (e.target.name == "Cube")
        {
            Debug.Log("Cube was entered");
        }
        else if (e.target.name == "Button")
        {
            Debug.Log("Button was entered");
        }
    }
   
    public void PointerOutside(object sender, PointerEventArgs e)
    {
        if (e.target.name == "Cube")
        {
            Debug.Log("Cube was exited");
        }
        else if (e.target.name == "Button")
        {
            Debug.Log("Button was exited");
        }
    }
    public void PointerClick(object sender, PointerEventArgs e)
    {
        Debug.Log("Item tag " + e.target.tag);
        fires = GameObject.FindGameObjectsWithTag("fire");
        if (firegun) {
            if (e.target.tag == "floor" && fires.Length<numFires)
            {
                Debug.Log("Got Floor, Placing fire");
                Vector3 objectPos = e.target.transform.position;
                Debug.Log(objectPos);
                objectPos.y += 1.5f;
                objectPos.z += 1.5f;
                objectPos.x += 1.5f;
                Instantiate(fire, objectPos, Quaternion.identity);
                //server.SendMessage("setPathFinding", false);
                //server.SendMessage("setPathDeciding", false);
                //server.SendMessage("startFindingPath");
            }
        }
        else
        {
            if (e.target.tag == "fire")
            {
                Destroy(e.target.gameObject);
            }
        }
        if (e.target.name == "Cube")
        {
            Debug.Log("Cube was clicked");
        }
        else if (e.target.name == "Button")
        {
            Debug.Log("Button was clicked");
        }
    }
}

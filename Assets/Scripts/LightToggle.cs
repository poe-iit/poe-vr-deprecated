using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LightToggle : MonoBehaviour
{
    public bool pathFinding = false;
    public bool pathDecided = false;
    public Light[] Lights;

    //public GameObject StartZone;
    public List<GameObject> SafeZones = new List<GameObject>();
    public List<GameObject> StartZones = new List<GameObject>();
    private List<NavMeshPath> SafePath = new List<NavMeshPath>();
    public List<float> PathLength = new List<float>();
    public int shortestPathIndex = 0;
    public int shortestPathIndex2 = 0;
    public float shortestPathCost = 0;
    //public Light[] pathOfLights = new Light[64];

    public List<Light> pathOfLights = new List<Light>();

    public List<GameObject> navAgent = new List<GameObject>();

    public float timeUsed;
    public bool toggleLights = false;
    public int i = 0;
    public float targetTime = 1.0f;
    public Color color0 = Color.red;
    //public Color color1 = Color.blue;
    public Color initialColor = Color.white;
    public Color colorPath = Color.blue;
    bool colorChanged = false;
    public bool enableButtons = false;
    public bool allowedToRun = false;
    public Light[] buttonLights;
    public List<Light> allCeilingLights;
    public static int numberLights = 0;
    public AudioSource alarmAudio;
    public bool audioAllowed = true;
    public bool lightsRemoved = false;
    public GameObject[] fire;
    //public int lastIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        GetSafeZones();
        GetStartZones();

        foreach (GameObject fooObj in GameObject.FindGameObjectsWithTag("nav_agent"))
        {

            //peopleList.Add(fooObj);
            navAgent.Add(fooObj);
        }

        for (int i=0;i<navAgent.Count;i++)
        {
            navAgent[i].SendMessage("setAgentIndex", i);
        }
        //navAgent = GameObject.FindGameObjectsWithTag("nav_agent");

        /*for (int i=0;i<SafePath.Count; i++) {
            for (int j = 0; j < SafePath[i].Count; j++) {
                SafePath[i][j] = new NavMeshPath();

            }
        }*/

        /*for (int i=0; i<SafeZones.Count; i++)
        {
            PathLength.Add(new float());
            
        }*/
        timeUsed = targetTime;
        /*for (int count = 0; count < Lights.Length; count++)
        {
            Lights[count].color = initialColor;
        }*/
        //allCeilingLightsGameobject = GameObject.FindGameObjectsWithTag("ceiling_light");
        setLightColor(color0);


        //numberLights = allCeilingLightsGameobject.Length;
        //allCeilingLights[0] = allCeilingLightsGameobject[0].GetComponent<Light>();
        /*public Light[] allCeilingLights = new Light[numberLights];
        for (int i=0; i<allCeilingLightsGameobject.Length; i++) {
            allCeilingLights[i] = allCeilingLightsGameobject[i].GetComponent<Light>();
            allCeilingLights[i].color = initialColor;
        }*/

        

    }

    // Update is called once per frame
    void Update()
    {
        fire = GameObject.FindGameObjectsWithTag("fire");

        
        if (fire.Length > 0)
        {
            pathFinding = true;
        }
        else
        {
            pathFinding = false;
            pathDecided = false;
        }
        

        for (int ni=0; ni<navAgent.Count; ni++) {
            navAgent[ni].SendMessage("setPathFinding", pathFinding);
        }
        if (pathFinding && pathDecided==false) {
            CompareSafeZones();
            FindShortestPath();
            lightsRemoved = false;
        }
        else if((pathFinding==false && pathDecided==false) || (pathFinding == false && pathDecided == true))
        {

            setLightColor(initialColor);
            colorChanged = false;
            if (lightsRemoved==false || pathOfLights.Count>0) {
                for (int count = 0; count < pathOfLights.Count; count++)
                {
                    pathOfLights.RemoveAt(count);
                }
                for (int count = 0; count<SafePath.Count; count++)
                {
                    SafePath.RemoveAt(count);
                }
                for (int count = 0; count < PathLength.Count; count++)
                {
                    PathLength.RemoveAt(count);
                }
                i = 0;
                lightsRemoved = true;
            }
        }
        drawPaths();
        
    }
    public void toggleTheLights ()
    {
        //toggleLights = !toggleLights;
        //enableButtons = false;
        buttonLights[0].enabled = false;
        buttonLights[1].enabled = false;
        if (enableButtons) {
            allowedToRun = !allowedToRun;
            //buttonLights[0].enabled = false;
            //buttonLights[1].enabled = false;
            enableButtons = false;
            colorChanged = false;
            if (audioAllowed==true) {
                alarmAudio.enabled = !alarmAudio.enabled;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.gameObject.tag);
        if (other.gameObject.tag=="id_card")
        {
            //Debug.Log("Got ID card");
            enableButtons = true;
            buttonLights[0].enabled = true;
            buttonLights[1].enabled = true;
        }
    }

    public void GetSafeZones()
    {
        SafeZones.AddRange(GameObject.FindGameObjectsWithTag("safe_zone"));
    }

    public void GetStartZones()
    {
        StartZones.AddRange(GameObject.FindGameObjectsWithTag("start_zone"));

    }

    public void CompareSafeZones()
    {
        //Debug.Log(DangerZone.transform.position);
        //Debug.Log("Safe zone 1: " + SafeZones[0].transform.position);
        //Debug.Log("Safe zone 2: " + SafeZones[1].transform.position);
        //Debug.Log("Comparing path");
        for (int i = 0; i < SafeZones.Count; i++)
        {
            PathLength.Add(new float());
            PathLength[i] = 0;

        }
        /*for (int i = 0; i < StartZones.Count; i++)
        {
           
                PathLength[i]=0;
            
        }*/
        for (int i = 0; i<(StartZones.Count); i++) {
            //SafePath.Add(new NavMeshPath());
            //PathLength.Add(new float());
            //SafePath[i] = new NavMeshPath();
            for (int i2 = 0; i2 < (SafeZones.Count); i2++)
            {
                //PathLength[i][i2] = 0;
                NavMeshPath tempSafePath = new NavMeshPath();
                NavMesh.CalculatePath(StartZones[i].transform.position, SafeZones[i2].transform.position, NavMesh.AllAreas, tempSafePath);
                //Debug.Log("TEMP PATH LENGTH " + Vector3.Distance(SafeZones[i].transform.position, tempSafePath.corners[tempSafePath.corners.Length-1]));
                
                Debug.Log("TEMP PATH STATUS " + tempSafePath.status);
                if (tempSafePath.status== NavMeshPathStatus.PathComplete) {
                    
                    Debug.Log("ADDING PATH FOR SAFE ZONE " + SafeZones[i2]);
                    SafePath.Add(tempSafePath);
                }
                else
                {
                    
                }
                
                //SafePath[i][i2]
                //DRAW PATH CODE

                if (SafePath[i2].corners.Length > 0)
                {
                    
                    for (int i3 = 0; i3 < SafePath[i2].corners.Length-1; i3++)
                        Debug.DrawLine(SafePath[i2].corners[i3], SafePath[i2].corners[i3 + 1], Color.red);
                }
                else
                {
                    //Debug.Log("No number");
                }
                //PathLength[i].Add(new float());
                /*for (int i5= 0; i5<PathLength[i].Count; i5++){
                    PathLength[i][i5] = 0;
                }*/
                for (int i3 = 0; i3 < (SafePath[i2].corners.Length-1); i3++)
                {
                   
                        PathLength[i2]+=Vector3.Distance(SafePath[i2].corners[i3], SafePath[i2].corners[i3 + 1]);
                        //Debug.Log(PathLength[0][i3]);
                    
                }

            }
            
            


        }
        
    }

    public void FindShortestPath()
    {
        Debug.Log("First shortest path cost:"+PathLength[0]);
        shortestPathCost = 999999999999;
        shortestPathIndex = 0;
        //shortestPathIndex2 = 0;
        for (int i = 0; i<(SafeZones.Count); i++)
        {
            
                Debug.Log("Is: " + PathLength[i]+ " The shortest path cost?");
                Debug.Log(PathLength[i]);
                if (PathLength[i] < shortestPathCost && PathLength[i] > 0)
                {
                    Debug.Log("YES");
                    Debug.Log(PathLength[i]);
                    shortestPathCost = PathLength[i];
                    shortestPathIndex = i;
                   
                }
                else
                {
                    Debug.Log("NO");
                }
            
        }
        for (int i3 = 0; i3 < SafePath[shortestPathIndex].corners.Length - 1; i3++)
            Debug.DrawLine(SafePath[shortestPathIndex].corners[i3], SafePath[shortestPathIndex].corners[i3 + 1], Color.blue);

        for (int ni=0; ni<navAgent.Count; ni++) {
           // Debug.Log("ni index "+ni);
            navAgent[ni].SendMessage("setPath", SafePath[shortestPathIndex]);
            pathDecided = true;
        }
    }

    public void addToLightPath(Light lightToAdd)
    {
        //Debug.Log("assigning path");
        /*for (int i = 0; i<pathOfLights.Length; i++) {
            Debug.Log(pathOfLights[i]);
            if (pathOfLights[i] == null)
            {
                pathOfLights[i] = lightToAdd;
                i = pathOfLights.Length;
            }
        }*/
        pathOfLights.Add(lightToAdd);

        Debug.Log("FINALLY ADDING LIGHT " + lightToAdd);
        Debug.Log("Length "+ pathOfLights.Count);
    }

    public void doSomethingWithLights()
    {
        /*for (int i = 0; i < pathOfLights.Length; i++)
        {
            if (pathOfLights[i]!=null)
            {
                pathOfLights[i].color = Color.green;
            }
        }*/

        if (colorChanged == false)
        {
            setLightColor(color0);
            setLightColorPath(colorPath);
            /*for (int count = 0; count < pathOfLights.Count; count++)
            {
                //Lights[count].color = color0;
                pathOfLights[count].color = color0;

            }*/
            colorChanged = true;
        }
        
        
        timeUsed -= Time.deltaTime;

        if (timeUsed <= 0.0f)
        {
            if (i != 0)
            {
                Debug.Log("I value: " + i);
                pathOfLights[i - 1].enabled = !pathOfLights[i - 1].enabled;
                //Lights[i-1].color = color0;
            }
            if (i >= pathOfLights.Count)
            {

                i = 0;
                pathOfLights[i].enabled = !pathOfLights[i].enabled;
                //Lights[i].color = color0;
                timeUsed = targetTime;
                i++;
                //Debug.Log("reached end of array");

            }
            else
            {
                pathOfLights[i].enabled = !pathOfLights[i].enabled;
                //Lights[i].color = colorPath;
                //lastIndex = i;
                i++;
                timeUsed = targetTime;
            }

        }

    }
    
    
    public void setPathFinding(bool value)
    {
        pathFinding = value;
        
    }

    public void setLightColorPath(Color lightColor)
    {
        for (int count = 0; count < pathOfLights.Count; count++)
        {
            //Lights[count].color = color0;
            pathOfLights[count].color = lightColor;
            pathOfLights[count].enabled = true;
            //pathOfLights.RemoveAt(count);
        }

    }
    public void setLightColor(Color lightColor)
    {
        for (int i = 0; i < allCeilingLights.Count; i++)
        {
            allCeilingLights[i].GetComponent<Light>().color = lightColor;
        }
    }

    public void addAllLights(Light light)
    {
        allCeilingLights.Add(light);
    }

    public void drawPaths()
    {
        for (int i=0; i<SafePath.Count; i++) {
            for (int i3 = 0; i3 < SafePath[i].corners.Length - 1; i3++)
            {

                Debug.DrawLine(SafePath[i].corners[i3], SafePath[i].corners[i3 + 1], Color.blue);
            }
        }
        }

}
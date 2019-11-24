using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightToggle : MonoBehaviour
{
    public Light[] Lights;

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
    public GameObject[] allCeilingLightsGameobject;
    public static int numberLights = 0;
    
    //public int lastIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        timeUsed = targetTime;
        /*for (int count = 0; count < Lights.Length; count++)
        {
            Lights[count].color = initialColor;
        }*/
        allCeilingLightsGameobject = GameObject.FindGameObjectsWithTag("ceiling_light");
        for (int i = 0; i<allCeilingLightsGameobject.Length; i++)
        {
            allCeilingLightsGameobject[i].GetComponent<Light>().color=initialColor;
        }
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
       
        if (toggleLights == false && colorChanged==false)
        {
            for (int count = 0; count < allCeilingLightsGameobject.Length; count++)
            {
                //Lights[count].color = initialColor;
                allCeilingLightsGameobject[count].GetComponent<Light>().color = initialColor;
                //allCeilingLights.GetComponent<light>().color= initialColor;
                //allCeilingLights[0].GetComponent<lights>()
                
            }
            colorChanged = true;
        }
        
            if (allowedToRun)
            {
            Debug.Log("allowed to run");
            if (colorChanged == false) { }
                for (int count = 0; count < allCeilingLightsGameobject.Length; count++)
                {
                    //Lights[count].color = color0;
                    allCeilingLightsGameobject[count].GetComponent<Light>().color = color0;

                }
                colorChanged = true;

                timeUsed -= Time.deltaTime;

                if (timeUsed <= 0.0f)
                {
                    if (i != 0)
                    {
                        Lights[i - 1].enabled = !Lights[i - 1].enabled;
                        //Lights[i-1].color = color0;
                    }
                    if (i >= Lights.Length)
                    {

                        i = 0;
                        Lights[i].enabled = !Lights[i].enabled;
                        //Lights[i].color = color0;
                        timeUsed = targetTime;
                        i++;
                        Debug.Log("reached end of array");

                    }
                    else
                    {
                        Lights[i].enabled = !Lights[i].enabled;
                        //Lights[i].color = colorPath;
                        //lastIndex = i;
                        i++;
                        timeUsed = targetTime;
                    }

                }




            }
        
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
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.gameObject.tag);
        if (other.gameObject.tag=="id_card")
        {
            Debug.Log("Got ID card");
            enableButtons = true;
            buttonLights[0].enabled = true;
            buttonLights[1].enabled = true;
        }
    }

}
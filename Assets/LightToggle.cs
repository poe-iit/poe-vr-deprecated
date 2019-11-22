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
    bool colorChanged = false;
    //public int lastIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        timeUsed = targetTime;
        for (int count = 0; count < Lights.Length; count++)
        {
            Lights[count].color = initialColor;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (toggleLights == false)
        {
            for (int count = 0; count < Lights.Length; count++)
            {
                Lights[count].color = initialColor;
            }
            colorChanged = false;
        }
        if (toggleLights)
        {
            if (colorChanged == false) { }
            for (int count = 0; count < Lights.Length; count++)
            {
                Lights[count].color = color0;

            }
            colorChanged = true;

            timeUsed -= Time.deltaTime;

            if (timeUsed <= 0.0f)
            {
                if (i != 0)
                {
                    Lights[i - 1].enabled = !Lights[i - 1].enabled;
                }
                if (i >= Lights.Length)
                {

                    i = 0;
                    Lights[i].enabled = !Lights[i].enabled;
                    timeUsed = targetTime;
                    i++;
                    Debug.Log("reached end of array");

                }
                else
                {
                    Lights[i].enabled = !Lights[i].enabled;
                    //lastIndex = i;
                    i++;
                    timeUsed = targetTime;
                }

            }




        }
    }

}
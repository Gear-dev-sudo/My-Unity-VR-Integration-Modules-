using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RandColor : MonoBehaviour
{
   Color color;
    void Start()
    {


        Light []lights = gameObject.GetComponentsInChildren<Light>();
        Color randCol=new Color(Random.Range(0, 255), Random.Range(0, 255), Random.Range(0, 255));
        foreach (Light light in lights)
         {
            light.color = randCol;
          }
       


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

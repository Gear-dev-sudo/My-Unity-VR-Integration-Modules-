using UnityEngine;


/*
 * usage:
 * public class MyAudioController : GenericSingletonClass<MyAudioController>
 * {
 * 
 * }
 */



    public class MySecurityCamContoller : my_unity_integration.GenericSingletonClass<MySecurityCamContoller>
{
        
        public int camCount;

        void Start()
        {
            camCount = 0;
        }
        public void addCam()
        {
            camCount++;
        }


    }


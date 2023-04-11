using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Addcam : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Assign the MySecurityCamContoller object")]
    private MySecurityCamContoller securityCamController;

    [SerializeField]
    [Tooltip("Assign the two Text(TMP) objects")]
    private TMPro.TextMeshProUGUI text1;

    [SerializeField]
    [Tooltip("Assign the two Text(TMP) objects")]
    private TMPro.TextMeshProUGUI text2;

    private void Start()
    {
        // Find the MySecurityCamController object in the scene
        securityCamController = GameObject.FindObjectOfType<MySecurityCamContoller>();

        // If the MySecurityCamController object is not found, log a warning
        if (securityCamController == null)
        {
            Debug.LogWarning("MySecurityCamController object not found in the scene!");
        }
        else
        {
            // Add a camera to the MySecurityCamController object
            securityCamController.addCam();
        }  // Update the text objects with the camera count
        text1.SetText(" # Cam: " + securityCamController.camCount);
        text2.SetText(" # Security Cam: " + securityCamController.camCount);
    }

   
}

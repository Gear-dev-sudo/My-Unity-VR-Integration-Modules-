using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Addcam : MonoBehaviour
{


    [SerializeField]
    [Tooltip("Assign the two Text(TMP) objects")]
    TMPro.TextMeshProUGUI text1;

    [SerializeField]
    [Tooltip("Assign the two Text(TMP) objects")]
    TMPro.TextMeshProUGUI text2;
    // Start is called before the first frame update
    void Awake()
    {
        MySecurityCamContoller.Instance.addCam();
        text1.SetText(" # Cam: " + MySecurityCamContoller.Instance.camCount);
        text2.SetText(" # Security Cam: " + MySecurityCamContoller.Instance.camCount);


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

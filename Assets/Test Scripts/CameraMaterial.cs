using Unity.VisualScripting;
using UnityEngine;

public class CameraMaterial : MonoBehaviour
{
    [Tooltip("The gameobject to apply the material to.")]
    public GameObject targetObject;
    // Material material;
    RenderTexture renderTexture;
    
    private Camera cameraComponent;
    Material material;
    private void Start()
    {
        // Get the camera component attached to this gameobject
        cameraComponent = GetComponent<Camera>();
        renderTexture = new RenderTexture(256,256,16);
        renderTexture.Create();
        material = targetObject.GetComponent<MeshRenderer>().material;
        
    }
    MeshRenderer mR;
    void Update()
    {
        cameraComponent.forceIntoRenderTexture = true;
        cameraComponent.targetTexture = renderTexture;
        material.mainTexture = renderTexture;
    }
    void OnDestroy()
    {
        renderTexture.Release();
    }
}

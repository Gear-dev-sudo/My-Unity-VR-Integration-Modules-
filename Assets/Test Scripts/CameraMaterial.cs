using UnityEngine;

public class CameraMaterial : MonoBehaviour
{
    [Tooltip("The gameobject to apply the material to.")]
    public GameObject targetObject;
    
    [Tooltip("The material to apply to the target object.")]
    private Material outputMaterial = new Material(Shader.Find("Standard"));
    
    private Camera cameraComponent;
    
    private void Start()
    {
        // Get the camera component attached to this gameobject
        cameraComponent = GetComponent<Camera>();
        
        // Set the camera's target texture to a new RenderTexture
        RenderTexture renderTexture = new RenderTexture(Screen.width, Screen.height, 24);
        cameraComponent.targetTexture = renderTexture;
        
        // Set the output material's main texture to the camera's target texture
        outputMaterial.mainTexture = renderTexture;
        
        // Apply the output material to the target object
        targetObject.GetComponent<Renderer>().material = outputMaterial;
    }
}

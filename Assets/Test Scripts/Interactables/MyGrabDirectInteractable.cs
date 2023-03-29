using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.XR.Interaction.Toolkit;

public class MyGrabDirectInteractable : XRBaseInteractable
{
    //Attach TransForms
    public Transform attachTransform;
    public Transform h_leftAttachTransform;
    public Transform h_rightAttachTransform;

    //Attach Offset
    public Vector3 attachPositionOffset;
    public Vector3 attachRotationOffset;

    private IXRInteractor _interactor;
    private Quaternion _attachInitialRotation;
    
    Transform attachPoint;
    public bool dualAttachCheckbox=false;
    public bool dynamicAttachCheckbox=false;

    protected override void Awake()
    {

        base.Awake();
        if (this.attachTransform == null&&!dualAttachCheckbox)
        {
            GameObject newGO = new GameObject("Attach Transform of " + name);
            Transform newTransform = newGO.transform;
            newTransform.parent = gameObject.transform;

            this.attachTransform = newTransform;
            attachPoint = newTransform;
        }  
        
        else if(this.attachTransform!=null&&!dualAttachCheckbox)
        {
            attachPoint = this.attachTransform;
        }

     
        if(this.attachTransform != null)
        _attachInitialRotation = attachTransform.localRotation;
        


    }

    protected override void OnEnable()
    {
        base.OnEnable();
        this.selectEntered.AddListener(XRSelectEnter);
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        this.selectEntered.RemoveListener(XRSelectEnter);
    }
    public void XRSelectEnter(SelectEnterEventArgs selectEnterEventArgs)
    {
        Debug.LogWarning("XR SEL ENTER");
        if (!dualAttachCheckbox&&dynamicAttachCheckbox)
        { attachPoint.position = selectEnterEventArgs.interactorObject.transform.position;

            attachPoint.rotation = selectEnterEventArgs.interactorObject.transform.rotation;
        }
        else if(dualAttachCheckbox&&!dynamicAttachCheckbox)
        {
            if (selectEnterEventArgs.interactorObject.transform.CompareTag("Left Hand"))
            {
                Debug.LogWarning("Left Hand");
              attachPoint = h_leftAttachTransform;
            }
            else if (selectEnterEventArgs.interactorObject.transform.CompareTag("Right Hand"))
            {
                attachPoint = h_rightAttachTransform;
            }
            
        }


        }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        _interactor = args.interactorObject;
     if (!dynamicAttachCheckbox)
        {
            //Debug.LogWarning("" + ((XRDirectInteractor)(args.interactorObject)).attachTransform.position + attachTransform.position);

            //((XRDirectInteractor)(args.interactorObject)).attachTransform = attachTransform;
            // Debug.LogWarning(""+ ((XRDirectInteractor)(args.interactorObject)).attachTransform.position+ attachTransform.position);
            //UpdateInteractorLocalPose(args.interactorObject);
            args.interactableObject.transform.position = (args.interactorObject).transform.position;
            args.interactableObject.transform.Translate(args.interactableObject.transform.position - attachPoint.position);
            


        }
       
        Attach();
    }

    Vector3 InteractorLocalPosition;
    Quaternion InteractorLocalRotation;
    void UpdateInteractorLocalPose(IXRInteractor interactor)
    {
        // In order to move the Interactable to the Interactor we need to
        // calculate the Interactable attach point in the coordinate system of the
        // Interactor's attach point.
        var thisAttachTransform = GetAttachTransform(interactor);
        var attachOffset = transform.position - thisAttachTransform.position;
        var localAttachOffset = thisAttachTransform.InverseTransformDirection(attachOffset);

        InteractorLocalPosition = localAttachOffset;
        InteractorLocalRotation = Quaternion.Inverse(Quaternion.Inverse(transform.rotation) * thisAttachTransform.rotation);
        //interactor.transform.Translate(attachOffset);
        //Debug.LogWarning("MOVING"+attachOffset);
        
    }

 
    protected override void OnSelectExiting(SelectExitEventArgs args)
    {
        base.OnSelectExiting(args);
        Detach();
        _interactor = null;
    }

    private void Attach()
    {
        if (_interactor is XRDirectInteractor)
        {
            // 将游戏对象附加到交互器上
            transform.SetParent(_interactor.transform);
            
            attachTransform.localPosition += attachPositionOffset;
            attachTransform.localRotation = Quaternion.Euler(attachRotationOffset) * _attachInitialRotation;
        
        }
    }

    private void Detach()
    {
        if (_interactor is XRDirectInteractor)
        {
            // 将游戏对象从交互器上分离
            transform.SetParent(null);
            attachTransform.localRotation = _attachInitialRotation;
        }
    }



    Vector3 GetWorldAttachPosition(IXRInteractor interactor)
    {
        var interactorAttachTransform = interactor.GetAttachTransform(this);
        return interactorAttachTransform.position + interactorAttachTransform.rotation * InteractorLocalPosition;
    }

    /// <summary>
    /// Calculates the world rotation to place this object at when selected.
    /// </summary>
    /// <param name="interactor">Interactor that is initiating the selection.</param>
    /// <returns>Returns the attach rotation in world space.</returns>
    Quaternion GetWorldAttachRotation(IXRInteractor interactor)
    {
       
        var interactorAttachTransform = interactor.GetAttachTransform(this);
        return interactorAttachTransform.rotation* InteractorLocalRotation;
    }
}






#if UNITY_EDITOR
[CustomEditor(typeof(MyGrabDirectInteractable))]
public class MyGrabInteractableEditor : Editor
{
    protected SerializedProperty m_InteractionLayers;
    public static readonly GUIContent interactionLayerSetting = EditorGUIUtility.TrTextContent("Interaction Layer Mask", "Allows interaction with Interactors whose Interaction Layer Mask overlaps with any Layer in this Interaction Layer Mask.");

    public override void OnInspectorGUI()
    {
        EditorGUILayout.LabelField("Interaction Layer", EditorStyles.boldLabel);
        m_InteractionLayers = serializedObject.FindProperty("m_InteractionLayers");
        serializedObject.Update();

        serializedObject.ApplyModifiedProperties();

        MyGrabDirectInteractable interactable = (MyGrabDirectInteractable)target;
        EditorGUILayout.PropertyField(m_InteractionLayers, interactionLayerSetting);
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Attach", EditorStyles.boldLabel);
        interactable.dualAttachCheckbox = EditorGUILayout.Toggle("Dual Attach",interactable.dualAttachCheckbox);
        interactable.dynamicAttachCheckbox = EditorGUILayout.Toggle("Dynamic Attach",interactable.dynamicAttachCheckbox);
         if (interactable.dualAttachCheckbox)
        {
            EditorGUILayout.LabelField("Left/Right Hand Attach Points", EditorStyles.boldLabel);
            interactable.h_leftAttachTransform= (Transform)EditorGUILayout.ObjectField("Left Hand Attach Transform", interactable.h_leftAttachTransform, typeof(Transform), true);
            interactable.h_rightAttachTransform= (Transform)EditorGUILayout.ObjectField("Right Hand Attach Transform", interactable.h_rightAttachTransform, typeof(Transform), true);
            // Add other menu items here
        }
        if (!interactable.dualAttachCheckbox)
        {
            if (!interactable.dynamicAttachCheckbox)
            {
                interactable.attachTransform = (Transform)EditorGUILayout.ObjectField("Attach Transform", interactable.attachTransform, typeof(Transform), true);
                
                interactable.attachRotationOffset = EditorGUILayout.Vector3Field("Rotation Offset", interactable.attachRotationOffset);
            }interactable.attachPositionOffset = EditorGUILayout.Vector3Field("Position Offset", interactable.attachPositionOffset);
            }
        }
}
#endif

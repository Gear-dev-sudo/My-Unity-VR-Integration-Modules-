using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.XR.Interaction.Toolkit;

public class MyGrabDirectInteractable : XRBaseInteractable
{
    public Transform attachTransform;
    public Vector3 attachPositionOffset;
    public Vector3 attachRotationOffset;

    private IXRInteractor _interactor;
    private Quaternion _attachInitialRotation;
    private Rigidbody _Rigidbody;
    Transform attachPoint;


    protected override void Awake()
    {

        base.Awake();
        if (this.attachTransform == null)
        {
            GameObject newGO = new GameObject("Attach Transform of " + name);
            Transform newTransform = newGO.transform;
            newTransform.parent = gameObject.transform;

            this.attachTransform = newTransform;
            attachPoint = newTransform;
        }

        else
        {
            attachPoint = this.attachTransform;
        }




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
        attachPoint.position = selectEnterEventArgs.interactorObject.transform.position;

        attachPoint.rotation = selectEnterEventArgs.interactorObject.transform.rotation;
    }

    protected override void OnSelectEntering(SelectEnterEventArgs args)
    {
        base.OnSelectEntering(args);
        _interactor = args.interactorObject;

        //test
        Debug.Log("_interactor is XRDirectInteractor"+(_interactor is XRDirectInteractor));
        Debug.Log("_interactor is XRRayInteractor"+(_interactor is XRRayInteractor));
        //test

        Attach();
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
            // ����Ϸ���󸽼ӵ���������
            transform.SetParent(_interactor.transform);
            attachTransform.localPosition = attachPositionOffset;
            attachTransform.localRotation = Quaternion.Euler(attachRotationOffset) * _attachInitialRotation;
        }
    }

    private void Detach()
    {
        if (_interactor is XRDirectInteractor)
        {
            // ����Ϸ����ӽ������Ϸ���
            transform.SetParent(null);
            attachTransform.localRotation = _attachInitialRotation;
        }
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
        interactable.attachTransform = (Transform)EditorGUILayout.ObjectField("Attach Transform", interactable.attachTransform, typeof(Transform), true);
        interactable.attachPositionOffset = EditorGUILayout.Vector3Field("Position Offset", interactable.attachPositionOffset);
        interactable.attachRotationOffset = EditorGUILayout.Vector3Field("Rotation Offset", interactable.attachRotationOffset);
    }
}
#endif

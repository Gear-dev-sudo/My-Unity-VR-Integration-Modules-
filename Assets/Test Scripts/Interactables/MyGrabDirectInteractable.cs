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

    private XRBaseInteractor _interactor;
    private Quaternion _attachInitialRotation;

    protected override void Awake()
    {
        base.Awake();
        if (attachTransform == null)
        {
            attachTransform = transform;
        }
        _attachInitialRotation = attachTransform.localRotation;
    }

   

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        _interactor = (XRBaseInteractor)args.interactorObject;
        Attach();
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);
        Detach();
        _interactor = null;
    }

    private void Attach()
    {
        if (_interactor is XRDirectInteractor)
        {
            // 将游戏对象附加到交互器上
            transform.SetParent(_interactor.transform);
            attachTransform.localPosition = attachPositionOffset;
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
}
#if UNITY_EDITOR

[CustomEditor(typeof(MyGrabDirectInteractable))]
public class MyGrabInteractableEditor : Editor
{
    public override void OnInspectorGUI()
    {
        

        MyGrabDirectInteractable interactable = (MyGrabDirectInteractable)target;

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Attach", EditorStyles.boldLabel);
        interactable.attachTransform = (Transform)EditorGUILayout.ObjectField("Attach Transform", interactable.attachTransform, typeof(Transform), true);
        interactable.attachPositionOffset = EditorGUILayout.Vector3Field("Position Offset", interactable.attachPositionOffset);
        interactable.attachRotationOffset = EditorGUILayout.Vector3Field("Rotation Offset", interactable.attachRotationOffset);
    }
}
#endif
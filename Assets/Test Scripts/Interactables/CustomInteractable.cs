using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CustomInteractable : XRBaseInteractable
{
    public Transform attachTransform;

    protected override void OnSelectEntered(XRBaseInteractor interactor)
    {
        if (attachTransform != null)
        {
            interactor.attachTransform.position = attachTransform.position;
            interactor.attachTransform.rotation = attachTransform.rotation;
        }

        base.OnSelectEntered(interactor);
    }
}
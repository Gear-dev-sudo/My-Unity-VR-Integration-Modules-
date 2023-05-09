using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using Microsoft.MixedReality.Toolkit.Experimental.UI;

public class ShowKeyboard : MonoBehaviour
{
    private TMP_InputField inputField;

    public float distance = 0.5f;
    public float verticalOffset = -0.5f;

    public Transform positionSource;

    // Start is called before the first frame update
    void Start()
    {
        inputField = GetComponent<TMP_InputField>();
        inputField.onSelect.AddListener(XRDirectClimbInteractor => OpenKeyBoard());
        
    }

    private void OpenKeyBoard()
    {
        NonNativeKeyboard.Instance.InputField = inputField;
        NonNativeKeyboard.Instance.PresentKeyboard(inputField.text);

        Vector3 direction = positionSource.forward;
        direction.y = 0;
        direction.Normalize();

        Vector3 targetPos = positionSource.position + direction * distance + Vector3.up * verticalOffset;
        NonNativeKeyboard.Instance.RepositionKeyboard(targetPos);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

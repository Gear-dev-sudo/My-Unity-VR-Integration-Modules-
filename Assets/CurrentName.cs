using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentName : MonoBehaviour
{
    TMPro.TMP_Text text;
    MyScoreSingleton scoreSingleton;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TMPro.TMP_Text>();
        scoreSingleton = FindObjectOfType<MyScoreSingleton>();
    }

    // Update is called once per frame
    void Update()
    {
        scoreSingleton.currentName = this.text.text;
    }
}

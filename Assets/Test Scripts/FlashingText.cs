using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FlashingText : MonoBehaviour
{
    [Tooltip("The text component to be flashed.")]
    public Text textComponent;
    [Tooltip("The time interval between flashes.")]
    public float flashInterval = 0.5f;

    private bool isFlashing = false;

    private void Start()
    {
        if (textComponent == null)
        {
            textComponent = GetComponent<Text>();
        }
        StartCoroutine(FlashText());
    }

    private IEnumerator FlashText()
    {
        while (true)
        {
            if (isFlashing)
            {
                textComponent.color = Color.black;
                isFlashing = false;
            }
            else
            {
                textComponent.color = Color.green;
                isFlashing = true;
            }
            yield return new WaitForSeconds(flashInterval);
        }
    }
}

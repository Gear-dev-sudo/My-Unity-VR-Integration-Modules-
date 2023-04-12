using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BudgetController : MonoBehaviour
{
    [Tooltip("Total Budget")]
    public int Budget;

    [Tooltip("Text to display when budget is exceeded")]
    public TMPro.TMP_Text budgetExceededText;

    public TMPro.TMP_Text budgetIndicator;

    [Tooltip("Duration of the flashing effect")]
    public float flashDuration = 0.5f;

    [Tooltip("Interval between flashes")]
    public float flashInterval = 0.1f;

    public bool isFlashing = false;

    // Start is called before the first frame update
    void Start()
    {
        
        // Deactivate the budgetExceededText at the start
        budgetExceededText.gameObject.SetActive(false);
        budgetIndicator.text = (" Your Budget $:"+Budget);
    }

    public void Buy(int cost)
    {
        Debug.LogWarning("Cost " + cost);
       
        if (Budget - cost >= 0)
        {
            Budget -= cost;  
            Debug.LogWarning("Budget Left " + Budget);
            budgetIndicator.color = Color.white;
            budgetIndicator.text = (" Your Budget $:" + Budget);
        } 
      
        else
        {
            Budget -= cost;
            budgetIndicator.color = Color.red;
            budgetIndicator.text = (" Your Budget $:" + Budget);
            // If budget is exceeded, start flashing the budgetExceededText
            if (!isFlashing)
            {
                StartCoroutine(FlashText());
            }
        }
    }

    IEnumerator FlashText()
    {
     

        while (true)
        {
            // Activate the budgetExceededText
            budgetExceededText.gameObject.SetActive(true);

            // Wait for the flashInterval
            yield return new WaitForSeconds(flashInterval);

            // Deactivate the budgetExceededText
            budgetExceededText.gameObject.SetActive(false);

            // Wait for the flashInterval
            yield return new WaitForSeconds(flashInterval);

            // Check if the flashDuration has elapsed
            if (flashDuration > 0)
            {
                flashDuration -= flashInterval;
            }
            else
            {
                // If flashDuration has elapsed, stop flashing and deactivate the budgetExceededText
                isFlashing = false;
                budgetExceededText.gameObject.SetActive(false);
                yield break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}

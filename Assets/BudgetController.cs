using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BudgetController : MonoBehaviour
{
    [Tooltip("Total Budget")]
    public int Budget=200;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void Buy(int cost)
    {
        Budget -= Budget;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

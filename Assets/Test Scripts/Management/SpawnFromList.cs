using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Spawn from a list of objects using an index
/// </summary>
public class SpawnFromList : MonoBehaviour
{
    [Tooltip("List of objects that are spawned")]
    public List<GameObject> originalObjects = null;

    [Tooltip("Corresponding Cost")]
    public List<int> originalObjectsCost = null;
    
    [Tooltip("Corresponding Score")]
    public List<int> originalObjectsScore = null;

    [Tooltip("Transform for how the object will be spawned")]
    public Transform spawnPoint = null;

    [Tooltip("Will the spawned object be childed to the point?")]
    public bool attachToSpawnPoint = false;

    private GameObject currentObject = null;
    private int index = 0;
    public BudgetController budgetController;
    public MyScoreSingleton myScoreSingleton;

    public void SpawnAtDropdownIndex(Dropdown dropdown)
    {
        index = Mathf.Clamp(dropdown.value, 0, originalObjects.Count);
        Spawn();
    }
    
 

    public void SpawnAndReplaceAtDropdownIndex(int value)
    {
        index = Mathf.Clamp(value, 0, originalObjects.Count);
        SpawnAndReplace();
    }

    public void Spawn()
    {
        CreateObject();
    }

    public void SpawnAndReplace()
    {
        GameObject newObject = CreateObject();
        ReplaceObject(newObject);
    }

    private GameObject CreateObject()
    {
        GameObject prefabToSpawn = originalObjects[index];
        GameObject newObject = Instantiate(prefabToSpawn, spawnPoint.position, spawnPoint.rotation);

        if (attachToSpawnPoint)
            newObject.transform.SetParent(spawnPoint);

        return newObject;
    }

    private void ReplaceObject(GameObject newObject)
    {
        if (currentObject)
            Destroy(currentObject);

        currentObject = newObject;
    }

    public void SpawnRandom()
    {
        index = Random.Range(0, originalObjects.Count);
        Spawn();
    }

    public void SpawnAtIndex(int value)
    {
        index = value;
        Spawn();

    }

    public TMPro.TMP_Dropdown tMP_Drop;
    public void SpawnAtIndexWithCost()
    {
        Debug.LogWarning("Spawn pre index+inde"+ tMP_Drop.value);
        index = tMP_Drop.value-1;
        if (index >= 0)
        {
            budgetController.Buy(originalObjectsCost[index]);
            myScoreSingleton.ComputeScore(originalObjectsScore[index]);
            Spawn();
        }
    }

    private void OnValidate()
    {
        if (!spawnPoint)
            spawnPoint = transform;
    }
}
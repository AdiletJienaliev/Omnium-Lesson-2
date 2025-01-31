using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class ICreatorUnit : MonoBehaviour
{
    [Header("Settings")]
    public Vector2 sizeMin;
    public Vector2 sizeMax;
    public float secondPerCreate;
    
    [Header("Links")]
    [Space]
    public Transform player;
    public GameObject unit;
    
    public static ICreatorUnit instance;
    
    private List<IUnit> units = new List<IUnit>();

    private void Awake()
    {
        if(instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        
        StartCoroutine(GeneratorCounter());
    }

    private IEnumerator GeneratorCounter()
    {
        while (true)
        {
            CreateUnit();
            yield return new WaitForSeconds(secondPerCreate);
        }
    }
    private void CreateUnit()
    {
        Debug.Log("Creating unit");
        Vector3 position = new Vector3(UnityEngine.Random.Range(sizeMin.x, sizeMax.x), 1, UnityEngine.Random.Range(sizeMin.y, sizeMax.y));
        GameObject newUnit = Instantiate(unit, position, Quaternion.identity);
        IUnit unitScript = newUnit.GetComponent<IUnit>();
        units.Add(unitScript);
        unitScript.listIndex = units.Count - 1;
        
        unitScript.Initialize(player);
    }

    public void RemoveUnit(int listIndex)
    {
        
        Debug.Log("Removing unit " + listIndex);
        units.RemoveAt(listIndex);
        UpdateListIndexes();
    }

    private void UpdateListIndexes()
    {
        for (int i = 0; i < units.Count; i++)
        {
            units[i].listIndex = i;
        }
    }

    public IUnit GetUnit()
    {
        float maxDistance = float.MaxValue;
        int minIndex = units.Count - 1;
        
        for (int i = 0; i < units.Count; i++)
        {
            float distanceToUnit = Vector3.Distance(units[i].GetPosition(), player.position);
            if (distanceToUnit < maxDistance)
            {
                maxDistance = distanceToUnit;
                minIndex = i;
            }
        }

        return units[minIndex];
    }
}

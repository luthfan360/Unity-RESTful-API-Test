using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    GameObject gridPrefab;
    GameObject newGridElement;
    Transform Grid;
    
    // Start is called before the first frame update
    void Start()
    {
        addElements();
        Grid = GameObject.Find("Grid").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void addElements()
    {
        for (int i = 0; i < 6; i++)
        {
            Vector2 spawnposition = new Vector2(0, 0);
            newGridElement = Instantiate(gridPrefab, spawnposition, Quaternion.identity);
            newGridElement.transform.SetParent(Grid, false);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Security.Claims;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public int colomLenght, rowLength;
    public float x_Space,y_space, z_Space;
    public GameObject prefab;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < colomLenght * rowLength; i++)
        {
            Instantiate(prefab, new Vector3(x_Space * (i % colomLenght),y_space, z_Space * (i / colomLenght)), Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

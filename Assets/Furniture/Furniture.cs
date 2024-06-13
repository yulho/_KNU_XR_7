using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.UI;
public class Furniture : MonoBehaviour
{
    public Vector3 spawnPosition;
    public GameObject fur;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void OnButtonClick()
    {
        Instantiate(fur, spawnPosition, Quaternion.identity);
    }
}

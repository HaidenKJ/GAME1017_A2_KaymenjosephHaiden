using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoadScript : MonoBehaviour
{
    void Start()
    {
        DontDestroyOnLoad(this.gameObject); // Prevent this object from being destroyed on scene load
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
}

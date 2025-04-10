using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController : MonoBehaviour
{
    public SoundManager SoundManagerR; // My monkey brain cannot remember the diffrence in capitilzation, so I just added a R to make it easier to auto correct into 
    public UIManager UIManagerR;
    public static MainController Instance { get; private set; }
    void Awake()
    {
    if (Instance == null)  // If no instance exists
    {
        Instance = this;  // Set this object as the instance
        DontDestroyOnLoad(gameObject);  // Keep it across scenes
    }
    else
    {
        Destroy(gameObject);  // If another exists, destroy this one
    }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

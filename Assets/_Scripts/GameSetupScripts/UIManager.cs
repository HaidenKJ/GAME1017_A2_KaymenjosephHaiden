using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    bool isPaused = false;
    Button Resume;
    Button Reset;
    [SerializeField] GameObject MenuPanel;
    // Start is called before the first frame update
    void Awake()
    {
        MenuPanel.SetActive(false);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    if (Input.GetKeyDown(KeyCode.Escape) && !isPaused)
    {
        Time.timeScale = 0;  // Pause the game
        isPaused = true;      // Mark as paused
        Debug.Log("Game is paused");
        MenuPanel.SetActive(true);
    }
    else if (Input.GetKeyDown(KeyCode.Escape) && isPaused)
    {
        Time.timeScale = 1;  // Resume the game
        isPaused = false;    // Mark as unpaused
        Debug.Log("Game resumed");
        MenuPanel.SetActive(false);
    }
    }

    public void Menu()
    {
        if (isPaused)
        {

        }
    }
}

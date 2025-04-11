using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class BestTimeManager : MonoBehaviour
{
    public static BestTimeManager Instance;

    public TMP_Text bestTimeText;  // Reference to the UI Text for displaying best time.
    public Timer timer;            // Reference to the Timer script.

    private float bestTime;

    void Awake()
    {
        // Singleton setup: Ensure only one instance of this manager persists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keep across scenes
        }
        else
        {
            Destroy(gameObject);  // Destroy this instance if one already exists
            return;
        }
    }

    void Start()
    {
        // Load the best time from PlayerPrefs when the scene starts
        bestTime = PlayerPrefs.GetFloat("BestTime", 0f);
        UpdateBestTimeUI();  // Update the UI with the best time
    }

    void Update()
    {
        // Continuously check if the current time surpasses the best time
        if (timer != null && timer.isRunning)
        {
            float currentTime = timer.GetTime();

            if (currentTime > bestTime)
            {
                bestTime = currentTime;
                PlayerPrefs.SetFloat("BestTime", bestTime);  // Save the new best time
                PlayerPrefs.Save(); // Ensure it is written immediately to disk
                UpdateBestTimeUI(); // Update UI to reflect new best time
                Debug.Log($"[BestTimeManager] New best time: {bestTime}");
            }
        }
    }

    // Update the UI text with the current best time
    void UpdateBestTimeUI()
    {
        if (bestTimeText == null)
        {
            Debug.LogWarning("[BestTimeManager] BestTimeText is not assigned.");
            return;
        }

        int minutes = Mathf.FloorToInt(bestTime / 60);
        int seconds = Mathf.FloorToInt(bestTime % 60);
        bestTimeText.text = $"Best Time: {minutes:00}:{seconds:00}";
    }

    // Call this method at the end of the game or when transitioning to the Game Over scene
    public void DisplayBestTimeOnEndScreen()
    {
        bestTime = PlayerPrefs.GetFloat("BestTime", 0f);  // Load best time again when showing end screen
        UpdateBestTimeUI();
    }
}

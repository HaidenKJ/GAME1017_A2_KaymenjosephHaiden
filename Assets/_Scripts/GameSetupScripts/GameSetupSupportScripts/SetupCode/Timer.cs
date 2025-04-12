using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text timerText;
    public float elapsedTime;
    public bool isRunning = true;

    private bool hasSurvivedOneMinute = false; // Flag to track if the achievement is unlocked

    void Update()
    {
        if (isRunning)
        {
            elapsedTime += Time.deltaTime;
            UpdateTimerUI();

            // Check for Survive One Minute achievement
            if (!hasSurvivedOneMinute && elapsedTime >= 60f)
            {
                hasSurvivedOneMinute = true; // Achievement triggered
                NotifyAchievementUnlocked(); // Call the method to notify achievement
            }
        }
    }

    void UpdateTimerUI()
    {
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void StopTimer()
    {
        isRunning = false;
    }

    public void StartTimer()
    {
        isRunning = true;
    }

    public float GetTime()
    {
        return elapsedTime;
    }

    // Notify the achievement observer when unlocked
    private void NotifyAchievementUnlocked()
    {
        // Here you would notify the AchievementObserver or any observer system
        Debug.Log("Survived One Minute Achievement Unlocked!");
    }
}

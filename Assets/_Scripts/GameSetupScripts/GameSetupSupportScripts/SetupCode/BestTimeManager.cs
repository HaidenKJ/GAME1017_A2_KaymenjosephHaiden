using UnityEngine;
using UnityEngine.UI;

public class BestTimeManager : MonoBehaviour
{
    public Text bestTimeText;
    public Timer timer; // reference to your Timer script

    private float bestTime;

    void Start()
    {
        bestTime = PlayerPrefs.GetFloat("BestTime", 0f);
        UpdateBestTimeUI();
    }

    public void TrySetBestTime()
    {
        float currentTime = timer.GetTime();

        if (currentTime > bestTime)
        {
            bestTime = currentTime;
            PlayerPrefs.SetFloat("BestTime", bestTime);
            PlayerPrefs.Save();
            UpdateBestTimeUI();
        }
    }

    void UpdateBestTimeUI()
    {
        int minutes = Mathf.FloorToInt(bestTime / 60);
        int seconds = Mathf.FloorToInt(bestTime % 60);
        bestTimeText.text = $"Best: {minutes:00}:{seconds:00}";
    }
}

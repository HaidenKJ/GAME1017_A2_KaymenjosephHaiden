using System.Collections.Generic;
using UnityEngine;

public class AchievementObserver : MonoBehaviour, IObserver
{
    private Dictionary<Event, Achievement> achievements;

    void Start()
    {
        achievements = new Dictionary<Event, Achievement>();

        // Updated achievements
        achievements[Event.SurvivedOneMinute] = new Achievement("Survive 1 Minute");
        achievements[Event.TenJumpsOverObstacles] = new Achievement("Jumped Over 10 Obstacles", 10);
        achievements[Event.TenRollsUnderObstacles] = new Achievement("Rolled Under 10 Obstacles", 10);
        achievements[Event.LostLife] = new Achievement("Lost a Life");
    }

    public void OnNotify(Event gameEvent)
    {
        // Log when event is triggered
        Debug.Log("Received event: " + gameEvent);

        if (achievements.ContainsKey(gameEvent) && !achievements[gameEvent].IsUnlocked)
        {
            achievements[gameEvent].Progress();
        }
    }
}

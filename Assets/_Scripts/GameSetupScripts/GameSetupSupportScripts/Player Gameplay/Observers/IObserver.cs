// TODO: Add to Week 12 lab.
public enum Event
{
    PlayerJumped,
    FiveRolls,
    SurvivedOneMinute,  
    TenJumpsOverObstacles, 
    TenRollsUnderObstacles,
    LostLife
}


public interface IObserver
{ 
    void OnNotify(Event gameEvent);
}


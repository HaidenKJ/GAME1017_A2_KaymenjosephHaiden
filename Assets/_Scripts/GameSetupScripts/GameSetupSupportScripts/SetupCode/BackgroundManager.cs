using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    [SerializeField] private GameObject[] backgrounds; // 3 background GameObjects
    [SerializeField] private float minSwapTime = 7f;
    [SerializeField] private float maxSwapTime = 15f;

    private float timer;
    private int currentFrontIndex = 0;

    void Start()
    {
        SetNewSwapTime();
        SetFrontBackground(currentFrontIndex);
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            currentFrontIndex = (currentFrontIndex + 1) % backgrounds.Length;
            SetFrontBackground(currentFrontIndex);
            SetNewSwapTime();
        }
    }

    void SetNewSwapTime()
    {
        timer = Random.Range(minSwapTime, maxSwapTime);
    }

    void SetFrontBackground(int frontIndex)
    {
        for (int i = 0; i < backgrounds.Length; i++)
        {
            var sr = backgrounds[i].GetComponent<SpriteRenderer>();
            sr.sortingOrder = (i == frontIndex) ? 1 : 0;
        }
    }
}

using UnityEngine;

public class PlayerBounds : MonoBehaviour
{
    private Camera mainCam;
    private Vector2 screenBounds;
    public float objectWidth;
    public float objectHeight;

    void Start()
    {
        mainCam = Camera.main;

        // Get half size of the object using the sprite renderer bounds
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            objectWidth = sr.bounds.extents.x;
            objectHeight = sr.bounds.extents.y;
        }
    }

    void LateUpdate()
    {
        Vector3 viewPos = transform.position;
        Vector3 lowerLeft = mainCam.ViewportToWorldPoint(new Vector3(0, 0, transform.position.z - mainCam.transform.position.z));
        Vector3 upperRight = mainCam.ViewportToWorldPoint(new Vector3(1, 1, transform.position.z - mainCam.transform.position.z));

        viewPos.x = Mathf.Clamp(viewPos.x, lowerLeft.x + objectWidth, upperRight.x - objectWidth);
        viewPos.y = Mathf.Clamp(viewPos.y, lowerLeft.y + objectHeight, upperRight.y - objectHeight);

        transform.position = viewPos;
    }
}

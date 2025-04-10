using UnityEngine;
using UnityEngine.SceneManagement; 
using TMPro;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject CouscousPrefab;
    private bool hasCouscousCharge = false; 
    [SerializeField] public float PlayerMoveSpeed = 5f;
    [SerializeField] private int maxLives = 3; // Maximum lives
    private int currentLives;
    [SerializeField] private TMP_Text livesText; // UI Text to display lives
    [SerializeField] private AudioClip damageSFX;
     private AudioSource audioSource; 

    void Start()
    {
        currentLives = maxLives;
        UpdateLivesUI();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // Handle shooting
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject bulletInst = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            Destroy(bulletInst, 3f);
        }
        // Handle shooting Couscous Missile (new addition)
        if (Input.GetKeyDown(KeyCode.C)) 
        {
            ShootCouscousMissile();
            hasCouscousCharge = false; // Consume charge after shooting
        }

        // Handle movement using Unity's Input system
        float moveHorizontal = Input.GetAxis("Hori");
        float moveVertical = Input.GetAxis("Verti");

        // Create a movement vector and move the player
        Vector3 movement = new Vector3(moveHorizontal, moveVertical, 0);
        transform.position += movement * PlayerMoveSpeed * Time.deltaTime;
    }

    private void UpdateLivesUI()
    {
        if (livesText != null)
        {
            livesText.text = $"Lives Left: {currentLives}";
        }
    }

    public void TakeDamage()
    {
        currentLives--;
        UpdateLivesUI();

        if (damageSFX != null && audioSource != null)
        {
            audioSource.PlayOneShot(damageSFX);
        }

        if (currentLives <= 0)
        {
            GameOver();
        }
    }
    public void GainLife()
    {
        if (currentLives < maxLives) // Prevent exceeding max lives
        {
            currentLives++;
            UpdateLivesUI();
        }
    }

    private void ShootCouscousMissile()
    {
        // Instantiate the Couscous Missile at player's position (you can adjust the offset if needed)
        GameObject couscousMissile = Instantiate(CouscousPrefab, transform.position, Quaternion.identity);      
    }
    public void CollectCouscousItem()
    {
        hasCouscousCharge = true; // Replenish charge when the item is collected
    }
    private void GameOver()
    {
        SceneManager.LoadScene(2); // Load Scene 2 when the game is over
    }

}

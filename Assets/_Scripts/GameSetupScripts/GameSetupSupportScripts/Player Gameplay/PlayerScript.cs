using UnityEngine;
using UnityEngine.SceneManagement; 
using TMPro;

public class PlayerScript : MonoBehaviour
{
    // [SerializeField] private GameObject bulletPrefab;
    // [SerializeField] private GameObject CouscousPrefab;
    // private bool hasCouscousCharge = false; 
    [SerializeField] public float PlayerMoveSpeed = 5f;
    [SerializeField] private int maxLives = 3; // Maximum lives
    private int currentLives;
    [SerializeField] private TMP_Text livesText; // UI Text to display lives
    [SerializeField] private AudioClip damageSFX;
    private AudioSource audioSource;

    // Add jump and crouch settings
    [SerializeField] private float jumpForce = 5f; // Jump force
    [SerializeField] private float crouchSpeed = 2f; // Speed while crouching
    [SerializeField] private float normalSpeed = 5f; // Normal speed

    private bool isGrounded;
    private bool isCrouching;

    private Rigidbody2D rb;
    private Collider2D playerCollider;
    private LayerMask groundLayer; // Ground detection

    void Start()
    {
        currentLives = maxLives;
        UpdateLivesUI();
        audioSource = GetComponent<AudioSource>();
        
        rb = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<Collider2D>();
        groundLayer = LayerMask.GetMask("Ground"); // Ensure "Ground" layer is set in your Unity project
    }

    void Update()
    {
        // // Handle shooting
        // if (Input.GetKeyDown(KeyCode.Space))
        // {
        //     GameObject bulletInst = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        //     Destroy(bulletInst, 3f);
        // }
        // // Handle shooting Couscous Missile (new addition)
        // if (Input.GetKeyDown(KeyCode.C)) 
        // {
        //     ShootCouscousMissile();
        //     hasCouscousCharge = false; // Consume charge after shooting
        // }

        // Handle movement using Unity's Input system
        float moveHorizontal = Input.GetAxis("Hori");
        Vector3 movement = new Vector3(moveHorizontal, 0, 0);
        transform.position += movement * PlayerMoveSpeed * Time.deltaTime;

        // Handle Jumping
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce); // Apply jump force
        }

        // Handle Crouching
        if (Input.GetKey(KeyCode.S)) // Crouch when pressing "S" key
        {
            Crouch(true);
        }
        else
        {
            Crouch(false);
        }
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.IsTouchingLayers(playerCollider, groundLayer);
        Debug.Log("Is Grounded: " + isGrounded); // Check if player is grounded
    }

    private void Crouch(bool crouch)
    {
        if (crouch && !isCrouching)
        {
            isCrouching = true;
            playerCollider.transform.localScale = new Vector3(1, 0.5f, 1); // Make the player smaller for crouch
            PlayerMoveSpeed = crouchSpeed; // Reduce speed while crouching
        }
        else if (!crouch && isCrouching)
        {
            isCrouching = false;
            playerCollider.transform.localScale = new Vector3(1, 1f, 1); // Reset scale back to normal
            PlayerMoveSpeed = normalSpeed; // Reset to normal speed
        }
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

    // private void ShootCouscousMissile()
    // {
    //     // Instantiate the Couscous Missile at player's position (you can adjust the offset if needed)
    //     GameObject couscousMissile = Instantiate(CouscousPrefab, transform.position, Quaternion.identity);      
    // }
    // public void CollectCouscousItem()
    // {
    //     hasCouscousCharge = true; // Replenish charge when the item is collected
    // }
    
    private void GameOver()
    {
        SceneManager.LoadScene(2); // Load Scene 2 when the game is over
    }
}

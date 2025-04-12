using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] public float PlayerMoveSpeed = 5f;
    [SerializeField] private int maxLives = 3;
    private int currentLives;
    [SerializeField] private TMP_Text livesText;
    [SerializeField] private AudioClip damageSFX;
    [SerializeField] private AudioClip JumpSFX;
    [SerializeField] private AudioClip KnifeflightSFX;

    private AudioSource audioSource;

    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float crouchSpeed = 2f;
    [SerializeField] private float normalSpeed = 5f;

    private bool isGrounded;
    private bool isCrouching;
    private bool isInvulnerable = false; // Track invulnerability state
    [SerializeField] private float invulnerabilityDuration = 5f; // Invulnerability duration
    private float invulnerabilityTimer;

    private Rigidbody2D rb;
    private Collider2D playerCollider;
    private LayerMask groundLayer;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        currentLives = maxLives;
        UpdateLivesUI();
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<Collider2D>();
        groundLayer = LayerMask.GetMask("Ground");

        // Get the SpriteRenderer from the child GameObject
        spriteRenderer = GetComponentInChildren<SpriteRenderer>(); // Find SpriteRenderer on child object
    }

    void Update()
    {
        if (isInvulnerable)
        {
            invulnerabilityTimer -= Time.deltaTime;
            if (invulnerabilityTimer <= 0)
            {
                EndInvulnerability();
            }
        }

        // Handle movement
        float moveHorizontal = Input.GetAxis("Hori");
        Vector3 movement = new Vector3(moveHorizontal, 0, 0);
        transform.position += movement * PlayerMoveSpeed * Time.deltaTime;

        // Handle jumping
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            audioSource.PlayOneShot(JumpSFX);
        }

        // Handle crouching
        if (Input.GetKey(KeyCode.S))
        {
            Crouch(true);
            audioSource.PlayOneShot(KnifeflightSFX);
        }
        else
        {
            Crouch(false);
        }
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.IsTouchingLayers(playerCollider, groundLayer);
    }

    private void Crouch(bool crouch)
    {
        if (crouch && !isCrouching)
        {
            isCrouching = true;
            playerCollider.transform.localScale = new Vector3(1, 0.5f, 1);
            PlayerMoveSpeed = crouchSpeed;
        }
        else if (!crouch && isCrouching)
        {
            isCrouching = false;
            playerCollider.transform.localScale = new Vector3(1, 1f, 1);
            PlayerMoveSpeed = normalSpeed;
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
        if (isInvulnerable) return; // Ignore damage if invulnerable

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
        else
        {
            StartInvulnerability(); // Start invulnerability after taking damage
        }
    }

    public void GainLife()
    {
        if (currentLives < maxLives)
        {
            currentLives++;
            UpdateLivesUI();
        }
    }

    private void GameOver()
    {
        SceneManager.LoadScene(2); // Game Over scene
    }

    private void StartInvulnerability()
    {
        isInvulnerable = true;
        invulnerabilityTimer = invulnerabilityDuration;

        // Check if the SpriteRenderer is found on the child GameObject
        if (spriteRenderer != null)
        {
            spriteRenderer.color = new Color(1, 1, 1, 0.5f); // Set player to semi-transparent
        }
        else
        {
            Debug.LogWarning("SpriteRenderer not found on any child of the PlayerShip Parent. Make sure the component is attached to a child object.");
        }

        playerCollider.enabled = false; // Disable collisions with obstacles (but allow ground)
    }

    private void EndInvulnerability()
    {
        isInvulnerable = false;

        // Reset transparency if the SpriteRenderer is found
        if (spriteRenderer != null)
        {
            spriteRenderer.color = new Color(1, 1, 1, 1); // Reset transparency
        }

        playerCollider.enabled = true; // Enable collisions again
    }
}

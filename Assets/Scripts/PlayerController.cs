using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D body;
    private bool IsGrounded;
    public Transform GroundChecker;
    public float CheckRaduis = 0.01f;
    public LayerMask GroundLayer;
    public float jumpHeight = 5;
    public float jumpFall = 4;
    public float fallMultiplier = 1.5f;

    public static bool isGameOver = false;
    public TextMeshProUGUI gameOverText;
    public Button gameOverButton;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isGameOver = false;
        body = GetComponent<Rigidbody2D>();
        gameOverText.gameObject.SetActive(false);
        gameOverButton.gameObject.SetActive(false);

        gameOverButton.onClick.AddListener(RestartGame);
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameOver) { return; }

        Collider2D collider2d = Physics2D.OverlapCircle(GroundChecker.position, CheckRaduis, GroundLayer);
        IsGrounded = collider2d != null && collider2d.CompareTag("Ground");
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded)
        {
            body.linearVelocity = new Vector2(transform.position.x, jumpHeight);
            // body.AddForceY(400);
        }

        if (body.linearVelocity.y > 0 && !Input.GetKey(KeyCode.Space))
        {
            // Jump button released early: fall faster
            body.linearVelocity += Vector2.up * Physics2D.gravity.y * (jumpFall - 1) * Time.deltaTime;
        }
        else if (body.linearVelocity.y < 0)
        {
            // Falling: increase gravity
            body.linearVelocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }

        if (transform.position.y < -4.5)
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        isGameOver = true;

        body.linearVelocity = Vector2.zero;
        body.bodyType = RigidbodyType2D.Kinematic;
        GetComponent<BoxCollider2D>().enabled = false;

        // Stop ground/platform movement — you'll need to broadcast this
        GroundSpawner[] spawners = Object.FindObjectsByType<GroundSpawner>(FindObjectsSortMode.None);
        foreach (var spawner in spawners)
        {
            Rigidbody2D spawn = GetComponent<Rigidbody2D>();
            spawner.enabled = false; // or call spawner.StopSpawning();


        }

        gameOverText.gameObject.SetActive(true);
        gameOverButton.gameObject.SetActive(true);
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

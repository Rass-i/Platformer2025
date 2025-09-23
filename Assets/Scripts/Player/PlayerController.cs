using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpSpeed = 7f;

    public bool playerIsGrounded;
    public Transform groundCheck;
    public LayerMask whatIsGround;
    public Vector2 groundBoxSize = new Vector2(0.8f, 0.2f);

    public bool playerIsOnWall;
    public Transform wallCheck;
    public LayerMask whatIsWall;
    public Vector2 wallBoxSize = new Vector2(0.1f, 0.8f);
    

    private InputManager _input;
    private Rigidbody2D _rigidbody2D;

    private void Start()
    {
        _input = GetComponent<InputManager>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        playerIsGrounded = Physics2D.OverlapBox(groundCheck.position, groundBoxSize, 0f, whatIsGround);
        if (_input.Jump && playerIsGrounded)
        {
            _rigidbody2D.linearVelocityY = jumpSpeed;
        }

        if (_input.Jumped && _rigidbody2D.linearVelocityY > 0f)
        {
            _rigidbody2D.linearVelocityY = _rigidbody2D.linearVelocity.y * 0.5f;
        }

        playerIsOnWall = Physics2D.OverlapBox(wallCheck.position, wallBoxSize, 0f, whatIsWall);
        if (_input.Jump && playerIsOnWall)
        {
            _rigidbody2D.linearVelocityY = jumpSpeed;
        }
        
        Attack();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(groundCheck.position, groundBoxSize);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, 0.2f);
        Gizmos.color = Color.aquamarine;
        Gizmos.DrawWireCube(wallCheck.position, wallBoxSize);
        
    }

    private void FixedUpdate()
    {
        _rigidbody2D.linearVelocityX = _input.Horizontal * moveSpeed;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.CompareTag("Death"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void Attack()
    {
        if (!Physics2D.OverlapCircle(groundCheck.position, 0.2f, LayerMask.GetMask("Enemy")))return;

        var enemyColliders = Physics2D.OverlapCircleAll(groundCheck.position, 0.2f, LayerMask.GetMask("Enemy"));

        foreach (var enemy in enemyColliders)
        {
            Destroy(enemy.gameObject);
        }

        _rigidbody2D.linearVelocityY = jumpSpeed / 1.3f;
    }
}

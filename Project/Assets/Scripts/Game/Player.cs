using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    public static Player Instance;

    public float jumpForce = 3f;
    public float speed;
    public Vector2 previousVelocity;

    // Componentes
    private Rigidbody2D rb;

    //Swipe Mode
    private Vector3 startPos, swipeDir;

    // Distancia que le impulsa el powerup de impulso
    private int impulseDistance = 300;

    private void Awake()
    {
        Instance = this;
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.onGameEnd += PausePlayer;
        GameManager.instance.onGamePaused += PausePlayer;
        GameManager.instance.onGameUnpaused += UnpausePlayer;

        PausePlayer();
    }

    // Update is called once per frame
    void Update()
    {
        //Jump Test
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump(5);
        }

    }
    private void FixedUpdate()
    {
        if (GameManager.instance.currentGameState != GameState.inGame)
        {
            return;
        }

        if (PlayerPrefs.GetInt("GameInput") == 2)
        {
            if (Input.touchCount > 0)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    startPos = new Vector3(Input.GetTouch(0).position.x, 0, 0);
                }
                else if (Input.GetTouch(0).phase == TouchPhase.Moved)
                {
                    swipeDir = new Vector3((Input.GetTouch(0).position.x - startPos.x), 0, 0);
                    Move();
                }
            }
        }
        else
        {
            Move();
        }
    }

    public void Jump(float forceMultiplier)
    {
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(Vector3.up * jumpForce * forceMultiplier, ForceMode2D.Impulse);
    }

    public void Move()
    {
        Vector3 dir = Vector3.zero;
        switch (PlayerPrefs.GetInt("GameInput"))
        {
            case 3:
                //Get the Horizontal axis and add a force
                dir = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
                if ((rb.velocity.x < 0 && dir.x >= 0) || (rb.velocity.x > 0 && dir.x <= 0))
                {
                    rb.velocity = new Vector2(0, rb.velocity.y);
                }
                rb.AddForce(dir * speed, ForceMode2D.Force);
                break;
            case 1:
                dir = new Vector3(CrossPlatformInputManager.GetAxis("Horizontal"), 0, 0);
                rb.AddForce(dir * speed, ForceMode2D.Force);
                break;
            case 0:
                transform.Translate(Input.acceleration.x, 0, 0);
                break;
            case 2:
                //Moves the player across the X axis
                transform.position += swipeDir;
                //+-4.5f = max dist player can move from lef to right
                if (transform.position.x > 4.5f)
                {
                    transform.position = new Vector3(4.5f, transform.position.y, 0);
                }
                else if (transform.position.x < -4.5f)
                {
                    transform.position = new Vector3(-4.5f, transform.position.y, 0);
                }
                break;
            default:
                Debug.LogWarning("Default exit on Player.Move method");
                break;
        }
    }

    public void PausePlayer()
    {
        rb.gravityScale = 0;
        previousVelocity = rb.velocity;
        rb.velocity = Vector2.zero;
    }
    private void UnpausePlayer()
    {
        rb.gravityScale = 1;
        rb.velocity = previousVelocity;
    }

    public void StartImpulse()
    {
        StartCoroutine(Impulse());
    }

    /// <summary>
    /// Impulse the player
    /// </summary>
    private IEnumerator Impulse()
    {
        Vector3 targetPos = transform.position + Vector3.up * impulseDistance;
        while (transform.position.y < targetPos.y)
        {
            rb.velocity = Vector2.up * 50f;
            yield return 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "LoseTrigger")
        {
            if (PlayerPrefs.GetInt("Shield") > 0)
            {
                Jump(30f);
                PlayerPrefs.SetInt("Shield", PlayerPrefs.GetInt("Shield") - 1);
            }
            else
            {
                //The actual Game end 
                GameManager.instance.GameEnd();
            }
        }
        else if (collision.gameObject.name.Contains("PlatformTrigger"))
        {
            PlatformSpawner.Instance.RemovePlatform();
        }
    }
}

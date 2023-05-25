using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public LayerMask interactableLayer;
    public LayerMask grassLayer;

    private bool isMoving;
    private Vector2 input;
    private Animator animator;

    GameController game_controller;
    Heart heart;

    public new Rigidbody2D rigibody { get; private set; }
    private Vector2 direction = Vector2.down;
    public float speed = 5f;

    public KeyCode inputUp = KeyCode.W;
    public KeyCode inputDown = KeyCode.S;
    public KeyCode inputLeft = KeyCode.A;
    public KeyCode inputRight = KeyCode.D;

    public AnimatedSpriteRenderer spriteRendererUp;
    public AnimatedSpriteRenderer spriteRendererDown;
    public AnimatedSpriteRenderer spriteRendererLeft;
    public AnimatedSpriteRenderer spriteRendererRight;
    private AnimatedSpriteRenderer activeSpriteRenderer;
    public AnimatedSpriteRenderer spriteRendererDeath;

    private UIManager uiManager;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rigibody = GetComponent<Rigidbody2D>();
        activeSpriteRenderer = spriteRendererDeath;
    }

    void Start()
    {
        game_controller = FindObjectOfType<GameController>();
        uiManager = FindObjectOfType<UIManager>();
        heart = FindObjectOfType<Heart>();
    }

    // Update is called once per frame
    void Update()
    {
        /*        if (!isMoving)
                {
                    input.x = Input.GetAxisRaw("Horizontal");
                    input.y = Input.GetAxisRaw("Vertical");

                    // remove diagonal movement
                    if (input.x != 0) input.y = 0;

                    if (input != Vector2.zero)
                    {
                        animator.SetFloat("moveX", input.x);
                        animator.SetFloat("moveY", input.y);

                        var targetPos = transform.position;
                        targetPos.x += input.x;
                        targetPos.y += input.y;

                        //StartCoroutine(Move(targetPos));

                        if (IsWalkable(targetPos))
                        {
                            StartCoroutine(Move(targetPos));
                        }
                    }
                }

                animator.SetBool("isMoving", isMoving);*/

        Debug.Log("update");
        if (Input.GetKey(inputUp))
        {
            SetDirection(Vector2.up, spriteRendererUp);
        }
        else if (Input.GetKey(inputDown))
        {
            SetDirection(Vector2.down, spriteRendererDown);
        }
        else if (Input.GetKey(inputLeft))
        {
            SetDirection(Vector2.left, spriteRendererLeft);
        }
        else if (Input.GetKey(inputRight))
        {
            SetDirection(Vector2.right, spriteRendererRight);
        }
        else
        {
            SetDirection(Vector2.zero, spriteRendererDown);
        }
    }

    private void FixedUpdate()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");

        // remove diagonal movement
        if (input.x != 0) input.y = 0;

        if (input != Vector2.zero)
        {
            var targetPos = transform.position;
            targetPos.x += input.x;
            targetPos.y += input.y;

            if (IsWalkable(targetPos))
            {
                Vector2 position = transform.position;
                Vector2 translation = direction * speed * Time.fixedDeltaTime;
                rigibody.MovePosition(position + translation);
            }
        }
    }

    private void SetDirection(Vector2 newDirection, AnimatedSpriteRenderer spriteRenderer)
    {
        direction = newDirection;

        spriteRendererUp.enabled = spriteRenderer == spriteRendererUp;
        spriteRendererDown.enabled = spriteRenderer == spriteRendererDown;
        spriteRendererLeft.enabled = spriteRenderer == spriteRendererLeft;
        spriteRendererRight.enabled = spriteRenderer == spriteRendererRight;
        spriteRendererDeath.enabled = spriteRenderer == spriteRendererDeath;

        activeSpriteRenderer = spriteRenderer;
        activeSpriteRenderer.idle = direction == Vector2.zero;
    }

    IEnumerator Move(Vector3 targetPost)
    {
        isMoving = true;
        while ((targetPost - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPost, moveSpeed * Time.deltaTime);
            yield return null;
        }

        transform.position = targetPost;

        isMoving = false;
    }

    private bool IsWalkable(Vector3 targetPos)
    {
        if (Physics2D.OverlapCircle(targetPos, 0 , interactableLayer) != null)
        {
            return false;
        }

        if (Physics2D.OverlapCircle(targetPos, 0, grassLayer) != null)
        {
            return false;
        }

        return true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Gold"))
        {
            game_controller.IncrementGold();
            Destroy(collision.gameObject);
        }

        /*if (collision.gameObject.layer == LayerMask.NameToLayer("Explosion"))
        {
            Debug.Log("dap bom");
            //gameObject.SetActive(false);
            //DeathSequence();
        }*/

        if (collision.CompareTag("Bomb"))
        {
            heart.Decrease();
            if (heart.GetMaxHealth() <= 0)
            {
                DeathSequence();
                uiManager.ShowPanelGameOver(true);
            }
            Destroy(collision.gameObject);
        }
    }

    public void DeathSequence()
    {
        enabled = false;
       // GetComponent<BombController>().enabled = false;
        spriteRendererUp.enabled = false;
        spriteRendererDown.enabled = false;
        spriteRendererLeft.enabled = false;
        spriteRendererRight.enabled = false;
        spriteRendererDeath.enabled = true;

        //Invoke(nameof(OnDeathSequenceEnded), 1.25f);
    }

    private void OnDeathSequenceEnded()
    {
        gameObject.SetActive(false);
    }
}

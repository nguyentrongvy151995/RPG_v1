using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;

    private bool isMoving;
    private Vector2 input;
    private Animator animator;

    GameController game_controller;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        game_controller = FindObjectOfType<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isMoving)
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

                StartCoroutine(Move(targetPos));

                /*if (IsWalkable(targetPos))
                {
                    StartCoroutine(Move(targetPos));
                }*/
            }
        }

        animator.SetBool("isMoving", isMoving);
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Gold"))
        {
            game_controller.IncrementGold();
            Destroy(collision.gameObject);
        }
    }
}

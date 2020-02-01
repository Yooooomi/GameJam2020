using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private enum Direction
    {
        STILL,
        LEFT,
        RIGHT,
    };

    public bool moveX;
    public bool moveY;

    private Stats stats;
    private Rigidbody2D rb;
    private Vector2 desired;
    public Inputs input;

    public Animator animator;

    private Direction currentDirection;

    private void Start()
    {

        stats = GetComponent<Stats>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void SetInput(Inputs input)
    {
        this.input = input;
    }

    private void SetOrientation(Direction dir)
    {
        if (currentDirection == dir)
        {
            return;
        }
        if (animator != null)
        {
            animator.SetBool("left", dir == Direction.LEFT);
            animator.SetBool("right", dir == Direction.RIGHT);
        }
        currentDirection = dir;
    }

    private void Update()
    {
        if (!input) return;
        float hor = input.GetAxis("Horizontal");
        float ver = input.GetAxis("Vertical");

        desired = new Vector2(hor, ver).normalized;

        if (!moveX)
        {
            desired.x = 0;
        }
        if (!moveY)
        {
            desired.y = 0;
        }

        if (desired.x == 0)
        {
            SetOrientation(Direction.STILL);
        }
        else if (desired.x > 0)
        {
            SetOrientation(Direction.RIGHT);
        }
        else
        {
            SetOrientation(Direction.LEFT);
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + desired * stats.moveSpeed * Time.fixedDeltaTime);
    }
}

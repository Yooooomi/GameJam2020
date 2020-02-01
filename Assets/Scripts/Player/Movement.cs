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

    public Sprite left;
    public Sprite middle;
    public Sprite right;

    private Dictionary<Direction, Sprite> dToSprite;

    public SpriteRenderer playerSprite;

    private Stats stats;
    private Rigidbody2D rb;
    private Vector2 desired;
    private Inputs input;

    private Direction currentDirection;

    private void Start()
    {
        dToSprite = new Dictionary<Direction, Sprite>
        {
            {Direction.LEFT, left },
            {Direction.STILL, middle },
            {Direction.RIGHT, right },
        };

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
        //playerSprite.sprite = dToSprite[dir];
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
        else if (desired.y > 0)
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

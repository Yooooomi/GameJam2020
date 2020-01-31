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
    private Vector3 desired;

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

    private void SetOrientation(Direction dir)
    {
        if (currentDirection == dir)
        {
            return;
        }
        playerSprite.sprite = dToSprite[dir];
        currentDirection = dir;
    }

    private void Update()
    {
        float horizontal = moveX ? Input.GetAxisRaw("Horizontal") : 0;
        float vertical = moveY ? Input.GetAxisRaw("Vertical") : 0;

        if (horizontal == 0)
        {
            SetOrientation(Direction.STILL);
        }
        else if (horizontal > 0)
        {
            SetOrientation(Direction.RIGHT);
        }
        else
        {
            SetOrientation(Direction.LEFT);
        }

        desired = new Vector3(horizontal, vertical, 0).normalized;
    }

    private void FixedUpdate()
    {
        rb.MovePosition(transform.position + desired * stats.moveSpeed * Time.fixedDeltaTime);
    }
}

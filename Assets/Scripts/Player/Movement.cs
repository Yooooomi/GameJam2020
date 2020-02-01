﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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
    private Inputs input;

    private Direction currentDirection;

    private void Start()
    {
        input = GetComponent<Inputs>();
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
        //playerSprite.sprite = dToSprite[dir];
        currentDirection = dir;
    }

    private void Update()
    {
        float hor = input.GetAxis("Horizontal");
        float ver = input.GetAxis("Vertical");

        desired = new Vector3(hor, 0, ver);

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
        rb.MovePosition(transform.position + desired * stats.moveSpeed * Time.fixedDeltaTime);
    }
}

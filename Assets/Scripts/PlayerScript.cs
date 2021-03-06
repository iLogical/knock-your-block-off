﻿using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class PlayerScript : MonoBehaviour, IKillable
{
    public float Speed = 5.0f;
    public float JumpHeight = 2.5f;
    public int Player;
    public Text ScoreText;
    public float ScoreTime = 5.0f;

    PlayerScript Opponent;
    float timeLeft;
    Rigidbody2D rigidbody2d;
    Transform groundCheck;
    bool grounded;
    Vector3 startPosition;
    string jumpInputBind;
    string moveInputBind;
    int score;
    SpriteRenderer spriteRenderer;

    public void AddScore(int points = 1)
    {
        score += points;
        ScoreText.text = score.ToString(); ;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            timeLeft = ScoreTime;
            Opponent = collision.gameObject.GetComponent<PlayerScript>();
        }
    }

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        groundCheck = transform.Find("groundCheck").gameObject.transform;
        startPosition = transform.position;
        jumpInputBind = string.Format("P{0}_Jump", Player);
        moveInputBind = string.Format("P{0}_Horizontal", Player);

        ScoreText.text = score.ToString();
    }

    void Update()
    {
        if (!Opponent)
            return;

        timeLeft -= Time.deltaTime;
        if (timeLeft < 0)
        {
            Opponent = null;
        }
    }

    void FixedUpdate()
    {
        GroundCheck();

        if (Input.GetButtonDown(jumpInputBind) && grounded)
            Jump();

        var velocity = InputToVelocity();
        Move(velocity);
    }

    Vector2 InputToVelocity()
    {
        var velocity = Vector2.zero;
        velocity.x += Input.GetAxis(moveInputBind);
        return velocity;
    }

    void GroundCheck()
    {
        grounded = Physics2D.Linecast(transform.position, -Vector3.up + groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
    }

    void Move(Vector2 force)
    {
        rigidbody2d.AddForce(force * Speed, ForceMode2D.Force);
    }

    void Jump()
    {
        rigidbody2d.AddForce(new Vector2(0, JumpHeight), ForceMode2D.Impulse);
    }

    public void Kill()
    {
        if (Opponent)
            Opponent.AddScore();
        Opponent = null;
        transform.position = startPosition;
        transform.rotation = Quaternion.identity;
        spriteRenderer.enabled = true;
    }
}
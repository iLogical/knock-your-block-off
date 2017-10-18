﻿using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class PlayerScript : MonoBehaviour
{
	public float Speed = 5.0f;
	public float JumpHeight = 2.5f;
	public int Player;
	public Text ScoreText;
	public PlayerScript Opponent;

	Rigidbody2D rigidbody2d;
	Transform groundCheck;
	bool grounded;
	Vector3 startPosition;
	string jumpInputBind;
	string moveInputBind;
	int score;

	public void AddScore()
	{
		score++;
		ScoreText.text = score.ToString(); ;
	}

	void Start()
	{
		rigidbody2d = GetComponent<Rigidbody2D>();
		groundCheck = transform.Find("groundCheck").gameObject.transform;
		startPosition = transform.position;
		jumpInputBind = string.Format("P{0}_Jump", Player);
		moveInputBind = string.Format("P{0}_Horizontal", Player);

		ScoreText.text = score.ToString(); ;
	}

	void Update()
	{
		GroundCheck();

		if ((Input.GetButtonDown(jumpInputBind)) && grounded)
			Jump();
	}

	void FixedUpdate()
	{
		var velocity = InputToVelocity();
		Move(velocity);
	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "Killbox")
		{
			Kill();
		}
	}

	void Kill()
	{
		Opponent.AddScore();

		transform.position = startPosition;
		transform.rotation = Quaternion.identity;
		rigidbody2d.velocity = Vector3.zero;
		rigidbody2d.angularVelocity = 0.0f;
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
}
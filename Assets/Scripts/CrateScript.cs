using System.Collections;
using UnityEngine;

public class CrateScript : MonoBehaviour
{
	ParticleSystem particles;
	SpriteRenderer spriteRenderer;

	void Start()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
		particles = GetComponent<ParticleSystem>();
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
		particles.Play();
		spriteRenderer.enabled = false;
		StartCoroutine(ExecuteAfterTime(particles.main.duration + 1.0f));
	}

	IEnumerator ExecuteAfterTime(float time)
	{
		yield return new WaitForSeconds(time);
		gameObject.SetActive(false);
	}
}

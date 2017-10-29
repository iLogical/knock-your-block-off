using System.Collections;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
[RequireComponent(typeof(SpriteRenderer))]
public class KillableScript : MonoBehaviour
{
	IKillable killable;
	ParticleSystem particles;
	SpriteRenderer spriteRenderer;

	void Start()
	{
		killable = GetComponent<IKillable>();
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
		if (killable != null)
			killable.Kill();
	}
}

using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class CrateScript : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "Killbox")
		{
			Kill();
		}
	}

	void Kill()
	{
        GetComponent<GameObject>().SetActive(false);
	}
}

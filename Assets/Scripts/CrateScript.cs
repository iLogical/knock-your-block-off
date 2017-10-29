using UnityEngine;

public class CrateScript : MonoBehaviour, IKillable
{
	public void Kill()
	{
		gameObject.SetActive(false);
	}
}

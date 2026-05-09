using System.Collections;
using UnityEngine;

public class TimedDestroy : MonoBehaviour
{
	[Tooltip("How long in seconds until the object is destroyed")]
	[SerializeField] private float lifetime = 5f;

	private IEnumerator Start()
	{
		yield return new WaitForSeconds(lifetime);
		Destroy(gameObject);
	}
}

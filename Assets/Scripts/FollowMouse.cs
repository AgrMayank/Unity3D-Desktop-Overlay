using UnityEngine;

public class FollowMouse : MonoBehaviour
{
	private new Camera camera;

	private void Awake()
	{
		camera = FindAnyObjectByType<Camera>();
	}

	private void Update()
	{
		Vector3 mousePos = camera.ScreenToWorldPoint(Input.mousePosition);
		transform.position = mousePos;
	}
}

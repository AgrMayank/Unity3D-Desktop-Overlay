using UnityEngine;

public class DragWindow : MonoBehaviour
{
	/// <summary>
	/// Simple click-and-drag, but moves the actual window (if not fullscreen)
	/// </summary>

	private new Collider2D collider = null;
	private bool draggingWindow = false;

	private void Awake()
	{
		collider = GetComponent<Collider2D>();
	}

	private void Update()
	{
		if (!Input.GetMouseButton(0))
		{
			draggingWindow = false;
			return;
		}

		if (Input.GetMouseButtonDown(0))
		{
			Vector2 pos = TransparentWindow.Camera.ScreenToWorldPoint(Input.mousePosition);

			Collider2D overlapCollider = Physics2D.OverlapPoint(pos);
			if (!overlapCollider)
			{
				draggingWindow = false;
				return;
			}

			if (overlapCollider == collider)
			{
				draggingWindow = true;
			}
		}

		if (draggingWindow)
		{
			TransparentWindow.DragWindow();
		}
	}
}

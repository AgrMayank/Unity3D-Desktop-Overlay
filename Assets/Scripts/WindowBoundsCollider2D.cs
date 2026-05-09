using UnityEngine;

[RequireComponent(typeof(Camera))]
public class WindowBoundsCollider2D : MonoBehaviour
{
	private new Camera camera;
	private EdgeCollider2D borderCollider;

	[Tooltip("Camera-relative size of the bounds (1 = full window, 0.5 = half). Useful for safe-areas")]
	[SerializeField]
	private float scale = 1f;

	[Tooltip("A larger radius helps prevent fast-moving objects from clipping through")]
	[SerializeField]
	private float edgeRadius = 10f;

	private void Start()
	{
		CreateCollider();
	}

	private void CreateCollider()
	{
		camera = GetComponent<Camera>();
		borderCollider = gameObject.AddComponent<EdgeCollider2D>();

		float cameraPlane = camera.orthographic ? 0 : -camera.transform.position.z;
		borderCollider.edgeRadius = edgeRadius;

		float maxScale = scale;
		float minScale = 1f - scale;
		borderCollider.points = new[]
		{
			(Vector2) camera.ViewportToWorldPoint(new Vector3(minScale, minScale, cameraPlane)) + new Vector2(-edgeRadius, -edgeRadius),
			(Vector2) camera.ViewportToWorldPoint(new Vector3(minScale, maxScale, cameraPlane)) + new Vector2(-edgeRadius, edgeRadius),
			(Vector2) camera.ViewportToWorldPoint(new Vector3(maxScale, maxScale, cameraPlane)) + new Vector2(edgeRadius, edgeRadius),
			(Vector2) camera.ViewportToWorldPoint(new Vector3(maxScale, minScale, cameraPlane)) + new Vector2(edgeRadius, -edgeRadius),
			(Vector2) camera.ViewportToWorldPoint(new Vector3(minScale, minScale, cameraPlane)) + new Vector2(-edgeRadius, -edgeRadius),
		};
	}

	private void OnDrawGizmosSelected()
	{
		float maxScale = scale;
		float minScale = 1f - scale;

		if (!camera)
		{
			camera = GetComponent<Camera>();
		}

		float cameraPlane = camera.orthographic ? 0 : -camera.transform.position.z;
		Vector3 pointA = camera.ViewportToWorldPoint(new Vector3(minScale, minScale, cameraPlane));
		Vector3 pointB = camera.ViewportToWorldPoint(new Vector3(minScale, maxScale, cameraPlane));
		Vector3 pointC = camera.ViewportToWorldPoint(new Vector3(maxScale, maxScale, cameraPlane));
		Vector3 pointD = camera.ViewportToWorldPoint(new Vector3(maxScale, minScale, cameraPlane));
		Gizmos.DrawLine(pointA, pointB);
		Gizmos.DrawLine(pointB, pointC);
		Gizmos.DrawLine(pointC, pointD);
		Gizmos.DrawLine(pointD, pointA);
	}
}

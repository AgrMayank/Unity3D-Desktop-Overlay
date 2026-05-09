using UnityEngine;

public class DragObject : MonoBehaviour
{
	[Tooltip("What GameObject layers should the click query against?")]
	[SerializeField]
	private LayerMask clickLayerMask = ~0;

	private TargetJoint2D joint;

	private void Update()
	{
		if (!Input.GetMouseButton(0))
		{
			if (joint)
			{
				Destroy(joint);
				joint = null;
			}

			return;
		}

		Vector2 pos = TransparentWindow.Camera.ScreenToWorldPoint(Input.mousePosition);

		if (joint)
		{
			joint.target = pos;
			return;
		}

		if (!Input.GetMouseButtonDown(0))
		{
			return;
		}

		Collider2D overlapCollider = Physics2D.OverlapPoint(pos, clickLayerMask);
		if (!overlapCollider)
		{
			return;
		}

		Rigidbody2D attachedRigidbody = overlapCollider.attachedRigidbody;
		if (!attachedRigidbody)
		{
			return;
		}

		joint = attachedRigidbody.gameObject.AddComponent<TargetJoint2D>();
		joint.autoConfigureTarget = false;
		joint.anchor = attachedRigidbody.transform.InverseTransformPoint(pos);
	}
}

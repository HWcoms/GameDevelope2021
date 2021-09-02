using UnityEngine;

/// <summary>
/// Rotates a this transform to align it towards the target transform's position.
/// </summary>
public class Gravity:MonoBehaviour
{
	#pragma warning disable 0649
	[SerializeField] private Transform planet;

	private void Update()
	{
		Vector3 dir = (transform.position - planet.position).normalized;

		GetComponent<PlayerMachine>().RotateGravity(dir);

		transform.rotation = Quaternion.FromToRotation(transform.up, dir) * transform.rotation;
	}
}
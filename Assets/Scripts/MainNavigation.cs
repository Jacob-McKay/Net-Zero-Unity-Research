using UnityEngine;
using System.Collections;

public class MainNavigation : MonoBehaviour
{  
		public Transform target;
		public Vector3 target2;
		public float distance = 200.0f;
		public float distanceMin = 5f;
		public float distanceMax = 500f;
		public float xSpeed = 10.0f;
		public float ySpeed = 50.0f;
		public float yMinLimit = 0f;
		public float yMaxLimit = 80f;
		public float scrollSpeed = 50f;
		public float smoothTime = 10f;
		public float changeTargetSmoothTime = 5f;
		float rotationYAxis = 0.0f;
		float rotationXAxis = 0.0f;
		float velocityX = 0.0f;
		float velocityY = 0.0f;

		void Start ()
		{
				Init ();
		}

		// Initialization
		void Init ()
		{
				//If there is no target, create a temporary target at 'distance' from the cameras current viewpoint
				if (!target) {
						GameObject go = new GameObject ("Cam Target");
						go.transform.position = transform.position + (transform.forward * distance);
						target = go.transform;
				}

				Vector3 angles = transform.eulerAngles;
				rotationYAxis = angles.y;
				rotationXAxis = angles.x;

				// Make the rigid body not change rotation
				if (rigidbody) {
						rigidbody.freezeRotation = true;
				}
		}

		void Update ()
		{
				//change target based on what object is selected with "MSOE" tag via left click
				if (Input.GetMouseButtonDown (0)) {
						Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
						RaycastHit hit;
						if (Physics.Raycast (ray, out hit) && hit.collider.tag == "MSOE") {
								//target = hit.collider.transform.position;
								Vector3 newTarget = hit.collider.transform.position;
								//Transform oldTarget = target;
								StartCoroutine (lerpCameraTarget (target, newTarget, 2f));
						}
				}

				//rotation from left click button
				if (Input.GetMouseButton (2)) {
						velocityX += Input.GetAxis ("Mouse X") * xSpeed * distance * 0.02f;
						velocityY += Input.GetAxis ("Mouse Y") * ySpeed * 0.02f;
				}
            
				rotationYAxis += velocityX;
				rotationXAxis -= velocityY;
            
				rotationXAxis = ClampAngle (rotationXAxis, yMinLimit, yMaxLimit);
            
				Quaternion fromRotation = Quaternion.Euler (transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0);
				Quaternion toRotation = Quaternion.Euler (rotationXAxis, rotationYAxis, 0);
				Quaternion rotation = toRotation;
				// affect the desired Zoom distance if we roll the scrollwheel
				distance = Mathf.Clamp (distance - Input.GetAxis ("Mouse ScrollWheel") * scrollSpeed, distanceMin, distanceMax);

				Vector3 negDistance = new Vector3 (0.0f, 0.0f, -distance);
				Vector3 position = rotation * negDistance + target.position;
            
				transform.rotation = rotation;
				transform.position = position;
            
				velocityX = Mathf.Lerp (velocityX, 0, Time.deltaTime * smoothTime);
				velocityY = Mathf.Lerp (velocityY, 0, Time.deltaTime * smoothTime);
		}
    
		private static float ClampAngle (float angle, float min, float max)
		{
				if (angle < -360F)
						angle += 360F;
				if (angle > 360F)
						angle -= 360F;
				return Mathf.Clamp (angle, min, max);
		}

		public void setCameraTargetFromSidebar (Transform target3, Vector3 newTarget2)
		{
				Debug.Log ("before coroutine");
				Debug.Log (target3);
				Debug.Log (newTarget2);
				StartCoroutine (lerpCameraTarget (target3, newTarget2, 2f));
				Debug.Log ("after coroutine");
		}
		//coroutine to lerp between old target and new target
		IEnumerator lerpCameraTarget (Transform target, Vector3 newTarget, float overTime)
		{
				float startTime = Time.time;
				while (Time.time < startTime + overTime) {
						target.position = Vector3.Lerp (target.position, newTarget, Time.deltaTime * changeTargetSmoothTime);
						yield return null;
				}
				target.position = newTarget;
		}
}

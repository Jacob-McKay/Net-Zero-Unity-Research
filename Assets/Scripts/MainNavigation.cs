using UnityEngine;
using System.Collections;

public class MainNavigation : MonoBehaviour
{  
		public Transform target;
		public float distance = 200.0f;
		public float distanceMin = 5f;
		public float distanceMax = 500f;
		public float xSpeed = 10.0f;
		public float ySpeed = 50.0f;
		public float yMinLimit = 0f;
		public float yMaxLimit = 80f;
		public float scrollSpeed = 50f;
		public float smoothTime = 10f;
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
				Ray ray = camera.ScreenPointToRay (new Vector3 (200, 200, 0));
				Debug.DrawRay (ray.origin, ray.direction * 10, Color.yellow, 10.0f);
		}
	
		void LateUpdate ()
		{
				//change target based on what object is selected with "MSOE" tag via left click
				if (Input.GetMouseButtonDown (0)) {
						Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
						RaycastHit hit2;
						if (Physics.Raycast (ray, out hit2) && hit2.collider.tag == "MSOE") {
								GameObject[] msoebuildings;
								msoebuildings = GameObject.FindGameObjectsWithTag ("MSOE");
								foreach (GameObject go in msoebuildings) {
										if (go == hit2.collider.gameObject) {
												target = go.transform;
										}
								}
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
}

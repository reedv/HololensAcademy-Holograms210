using UnityEngine;

namespace Academy.HoloToolkit.Unity
{
    /// <summary>
    /// GazeManager determines the location of the user's gaze, hit position and normals.
    /// </summary>
    public class GazeManager : Singleton<GazeManager>
    {
        [Tooltip("Maximum gaze distance for calculating a hit.")]
        public float MaxGazeDistance = 5.0f;

        [Tooltip("Select the layers raycast should target.")]
        public LayerMask RaycastLayerMask = Physics.DefaultRaycastLayers;

        /// <summary>
        /// Physics.Raycast result is true if it hits a Hologram.
        /// </summary>
        public bool Hit { get; private set; }

        /// <summary>
        /// HitInfo property gives access
        /// to RaycastHit public members.
        /// </summary>
        public RaycastHit HitInfo { get; private set; }

        /// <summary>
        /// Position of the user's gaze.
        /// </summary>
        public Vector3 Position { get; private set; }

        /// <summary>
        /// RaycastHit Normal direction.
        /// </summary>
        public Vector3 Normal { get; private set; }

        private GazeStabilizer gazeStabilizer;
        private Vector3 gazeOrigin;
        private Vector3 gazeDirection;

        void Awake()
        {
            /* TODO: DEVELOPER CODING EXERCISE 3.a */

            // 3.a: GetComponent GazeStabilizer and assign it to gazeStabilizer.
            // Based on how GetComponent is used here, I think it uses an implicit 'this' (ie. this gameobject)
            gazeStabilizer = GetComponent<GazeStabilizer>();
        }

        private void Update()
        {
            // 2.a: Assign Camera's main transform _position_ to gazeOrigin.
            gazeOrigin = Camera.main.transform.position;

            // 2.a: Assign Camera's main transform _forward_ to gazeDirection.
            gazeDirection = Camera.main.transform.forward;

            // 3.a: Using gazeStabilizer, call function UpdateHeadStability.
            // Pass in gazeOrigin and Camera's main transform rotation.
            // Note: 'main' refers to the first enabled camera tagged "MainCamera" (Read Only).
            gazeStabilizer.UpdateHeadStability(position: gazeOrigin,
                                               rotation: Camera.main.transform.rotation);

            // 3.a: Using gazeStabilizer, get the StableHeadPosition and
            // assign it to gazeOrigin.
            gazeOrigin = gazeStabilizer.StableHeadPosition;

            UpdateRaycast();
        }

        /// <summary>
        /// Calculates the Raycast hit position and normal.
        /// </summary>
        private void UpdateRaycast()
        {
            /* TODO: DEVELOPER CODING EXERCISE 2.a */

            // 2.a: Create a variable hitInfo of type RaycastHit.
            RaycastHit hitInfo;

            // 2.a: Perform a Unity Physics Raycast.
            // Collect return value in public property Hit.
            // Pass in origin as gazeOrigin and direction as gazeDirection.
            // Collect the information in hitInfo.
            // Pass in MaxGazeDistance and RaycastLayerMask.
            Hit = Physics.Raycast(gazeOrigin,
                                  gazeDirection,
                                  out hitInfo,
                                  MaxGazeDistance,
                                  layerMask: RaycastLayerMask);


            // 2.a: Assign hitInfo variable to the HitInfo public property 
            // so other classes can access it.
            HitInfo = hitInfo;

            if (Hit)
            {
                // If raycast hit a hologram...

                // 2.a: Assign property Position to be the hitInfo point (the point of the hit hologram).
                Position = hitInfo.point;
                // 2.a: Assign property Normal to be the hitInfo normal (the normal line to surface of the hit hologram).
                Normal = hitInfo.normal;
            }
            else
            {
                // If raycast did not hit a hologram...
                // Save defaults ...

                // 2.a: Assign Position to be gazeOrigin plus MaxGazeDistance times gazeDirection.
                // Projects the gaze Position to be to be as far out as poss. (or until hit Raycast layerMask)
                // from the origin/camera/user-POV
                Position = gazeOrigin + (gazeDirection * MaxGazeDistance);
                // 2.a: Assign Normal to be the user's gazeDirection.
                // This lies an obj. (like a cursor) to lie flat/top-down in the user's gaze
                Normal = gazeDirection;
            }
        }
    }
}
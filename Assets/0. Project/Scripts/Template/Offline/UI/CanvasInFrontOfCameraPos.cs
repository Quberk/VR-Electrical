using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VRMultiplayer.Template.Offline.UI{
    public class CanvasInFrontOfCameraPos : MonoBehaviour
    {
        [Header("General")]
        [SerializeField] private float yOffsets;
        [SerializeField] private float distanceToCamera;

        [Header("Menggunakan Delay")]
        [SerializeField] private bool useDelay;
        [SerializeField] private float delay;
        private Vector3 velocity = Vector3.zero;
        
        private GameObject mainCamera;
        private Vector3 targetPosition;

        void Start(){
            mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        }

        void Update(){
            Vector3 cameraForward = new Vector3(mainCamera.transform.forward.x, 
                                                0f + yOffsets, 
                                                mainCamera.transform.forward.z).normalized;

            targetPosition = mainCamera.transform.position + cameraForward * distanceToCamera;

            if (useDelay)
                transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, delay);

            else
                transform.position = targetPosition; 
        }

        public void ResetCanvasPosition(){
            transform.position = targetPosition;
        }
    }

}
 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VRMultiplayer.Template.Offline.Default{
    public class FollowObject : MonoBehaviour
    {
        [SerializeField] private GameObject targetObject;
        [SerializeField] private Vector3 offset;

        [SerializeField] private bool lookingAtTarget;
        [SerializeField] private GameObject targetToLookAt;

        // Update is called once per frame
        void Update()
        {
            transform.position = targetObject.transform.position + offset;

            if (lookingAtTarget)
                transform.LookAt(targetToLookAt.transform);
        }
    }
}


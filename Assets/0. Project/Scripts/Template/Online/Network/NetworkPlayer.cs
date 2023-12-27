using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.XR;

namespace VRMultiplayer.Template.Online.Network{

    public class NetworkPlayer : MonoBehaviour
    {
        [SerializeField] private Transform headTransform;
        [SerializeField] private Transform leftHandTransform;
        [SerializeField] private Transform rightHandTransform;

        [SerializeField] private GameObject headGfx;
        [SerializeField] private GameObject leftHandGfx;
        [SerializeField] private GameObject rightHandGfx;

        private PhotonView photonView;

        void Start(){
            photonView = GetComponent<PhotonView>();
        }

        void Update(){
            
            if (photonView.IsMine){

                //Menonaktifkan Tampilan diri sendiri
                headGfx.gameObject.SetActive(false);
                leftHandGfx.gameObject.SetActive(false);
                rightHandGfx.gameObject.SetActive(false);

                MapPosition(headTransform, XRNode.Head);
                MapPosition(leftHandTransform, XRNode.LeftHand);
                MapPosition(rightHandTransform, XRNode.RightHand);
            }


        }

        void MapPosition(Transform target, XRNode node){
            
            InputDevices.GetDeviceAtXRNode(node).TryGetFeatureValue(CommonUsages.devicePosition, out Vector3 position);
            InputDevices.GetDeviceAtXRNode(node).TryGetFeatureValue(CommonUsages.deviceRotation, out Quaternion rotation);

            target.position = position;
            target.rotation = rotation;

        }
    }
}


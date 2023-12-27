using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
namespace VRMultiplayer.Template.Offline.EquippingItem{

    public class GadgetEquipmentController : MonoBehaviour
    {
        private GameObject mainCamera;

        private XRGrabInteractable xRGrabInteractable;
        
        [SerializeField] private bool interactableEquipment;
        [SerializeField] private string parentObjectTag;
        private Vector3 initialLocalTransformPosition;
        private Quaternion initialLocalTransformRotation;

        [Header("Parent Object mengikuti Rotasi Camera")]
        [SerializeField] private bool followCameraRotation;
        [SerializeField] private float yRotationOffset; 

        private GameObject parentObject;

        void Start()
        {
            mainCamera = GameObject.FindGameObjectWithTag("MainCamera");

            if (followCameraRotation)
                parentObject = GameObject.FindGameObjectWithTag(parentObjectTag);
            else
                parentObject = gameObject.transform.parent.transform.gameObject;

            //Jika Objek adalah Equipment yang dapat diinteraksikan
            if (interactableEquipment){
                xRGrabInteractable = GetComponent<XRGrabInteractable>();
                xRGrabInteractable.onSelectEntered.AddListener(OnSelectGrabEntered);
                xRGrabInteractable.onSelectExited.AddListener(OnSelectGrabExited);

                initialLocalTransformPosition = gameObject.transform.localPosition;
                initialLocalTransformRotation = gameObject.transform.localRotation;
            }
        }

        void Update(){
            //Jika kita ingin mengatur rotasinya untuk mengikuti Rotasi Kamera
            if (followCameraRotation)
                parentObject.transform.rotation = Quaternion.Euler(new Vector3(gameObject.transform.rotation.eulerAngles.x, 
                                                                            mainCamera.transform.localRotation.eulerAngles.y + yRotationOffset, 
                                                                            gameObject.transform.rotation.eulerAngles.z));
        }

        //=================================METHOD UNTUK MENGATUR KETIKA OBJEK DIPEGANG=================================================
        void OnSelectGrabEntered(XRBaseInteractor interactor){
            gameObject.transform.SetParent(null);
        }

        void OnSelectGrabExited(XRBaseInteractor interactor){
            gameObject.transform.SetParent(parentObject.transform);
            gameObject.transform.localPosition = initialLocalTransformPosition;
            gameObject.transform.localRotation = initialLocalTransformRotation;
        }

    }

    public enum GadgetStatus{
        Unequipped,
        Equipped,
        Used
    }
}


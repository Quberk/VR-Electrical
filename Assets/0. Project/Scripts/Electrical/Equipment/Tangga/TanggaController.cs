using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace VRMultiplayer.Electrical.Equipment.Tangga{
    public class TanggaController : MonoBehaviour
    {
        [SerializeField] private XRGrabInteractable xRGrabInteractable;
        [SerializeField] private GameObject tanggaColliders;
        private bool beingGrabbed = false;

        // Start is called before the first frame update
        void Start()
        {
            xRGrabInteractable.onSelectEntered.AddListener(OnSelectGrabEntered);
            xRGrabInteractable.onSelectExited.AddListener(OnSelectGrabExited);
        }



        // Update is called once per frame
        void Update()
        {
            //if (!beingGrabbed) //Jika sudah dilepas dari Grab maka otomatis berusaha untuk berdiri tegap
            //    StandUp();
        }

        void StandUp()
        {
            Quaternion startRotation = transform.rotation;
            Quaternion endRotation = Quaternion.Euler(0, startRotation.y, 0);      
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, Time.deltaTime*2.0F);
        }

        //=================================METHOD UNTUK MENGATUR KETIKA OBJEK DIPEGANG=================================================
        void OnSelectGrabEntered(XRBaseInteractor interactor){
            tanggaColliders.SetActive(false);
            beingGrabbed = true;
        }

        void OnSelectGrabExited(XRBaseInteractor interactor){
            tanggaColliders.SetActive(true);
            beingGrabbed = false;
        }

    }

}

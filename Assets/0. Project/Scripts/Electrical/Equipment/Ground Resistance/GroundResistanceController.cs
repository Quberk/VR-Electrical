using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRMultiplayer.Template.Offline.EquippingItem;
using UnityEngine.XR.Interaction.Toolkit;

namespace VRMultiplayer.Electrical.Equipment.GroundResistance{
    public class GroundResistanceController : MonoBehaviour
    {
        [SerializeField] private GadgetEquipmentController gadgetEquipmentController;
        [SerializeField] private XRGrabInteractable xRGrabInteractable;
        [SerializeField] private string[] groundName;
        private Collider[] groundCollider;
        private Transform groundLocation;

        [Header("Saat Mendeteksi Ground dia Terinstall untuk beberapa saat")]
        [SerializeField] private float installedTime;
        private float installedlCounter = 0;
        private bool startInstalled = false;


        // Start is called before the first frame update
        void Start()
        {
            groundCollider = new Collider[groundName.Length];

            for (int i = 0; i < groundCollider.Length; i++){
                groundCollider[i] = GameObject.Find(groundName[i]).GetComponent<Collider>();
            }
        }

        // Update is called once per frame
        void Update()
        {
            InstalledCountDown();
        }

        void InstalledCountDown(){

            if (!startInstalled)
                return;
            
            installedlCounter += Time.deltaTime;


            if (installedlCounter >= installedTime){
                installedlCounter = 0;
                startInstalled = false;
                gadgetEquipmentController.enabled = true;
                xRGrabInteractable.enabled = true;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            for (int i = 0; i < groundCollider.Length; i++){
                if (other == groundCollider[i]){
                    startInstalled = true;
                    gadgetEquipmentController.enabled = false;
                    xRGrabInteractable.enabled = false;
                    groundLocation = other.GetComponent<Transform>();
                    transform.position = groundLocation.position;
                    transform.rotation = Quaternion.Euler(0f, -180f, 0f);
                }
            } 
        }

        void OnTriggerExit(Collider other)
        {
            for (int i = 0; i < groundCollider.Length; i++){
                if (other == groundCollider[i]){
                    
                }
            } 
        }
    }
}


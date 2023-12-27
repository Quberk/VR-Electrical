using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRMultiplayer.Template.Offline.ObjectInteractions;

namespace VRMultiplayer.Electrical.Simulasi{
    public class CableInstallation : MonoBehaviour
    {
        [SerializeField] private ObjectInteractionManager objectInteractionManager;
        [SerializeField] private Kabel[] kabel;

        void Start(){
            for (int i = 0; i < kabel.Length; i++){
                kabel[i].Deactive();
            }
        }

        // Update is called once per frame
        void Update()
        {
            for (int i = 0; i < kabel.Length; i++){
                
                //Jika Sudah terpasang Kabelnya maka hancurkan semua Kabel
                if (objectInteractionManager.GetCollided() && (objectInteractionManager.GetStep() == kabel[i].stepToActive)){
                    kabel[i].Deactive();
                    Destroy(kabel[i].startKabel);
                    Destroy(kabel[i].endKabel);
                    Destroy(kabel[i].wire);
                }

                //Jika sedang aktif Stepnya
                else if(objectInteractionManager.GetStep() == kabel[i].stepToActive)
                    if (kabel[i].startKabel != null)
                        kabel[i].Active();
            }
        }
    }

    [System.Serializable]
    public class Kabel{
        public GameObject startKabel;
        public GameObject endKabel;
        public GameObject wire;
        public int stepToActive;

        public void Active(){
            startKabel.SetActive(true);
            endKabel.SetActive(true);
            wire.SetActive(true);
        }

        public void Deactive(){
            startKabel.SetActive(false);
            endKabel.SetActive(false);
            wire.SetActive(false);
        }
    }
}


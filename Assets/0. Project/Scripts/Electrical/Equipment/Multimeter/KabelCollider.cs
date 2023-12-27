using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine;

namespace VRMultiplayer.Electrical.Equipment.Multimeter{
    public class KabelCollider : MonoBehaviour
    {
        [SerializeField] private KabelController kabelController;
        private string[] namaSumbuPositif; //Nama-nama Gameobject yang sebagai Sumbu Positif
        private string[] namaSumbuNegatif; //Nama-nama Gameobject yang sebagai Sumbu Negatif

        private GameObject[] sumbuPositif;
        private GameObject[] sumbuNegatif;

        private Sumbu sumbuYangTersambung = Sumbu.None;

        void Update(){
        }

        void FindSumbuPositifGameobjects(){
            sumbuPositif = new GameObject[namaSumbuPositif.Length];

            for (int i = 0; i < namaSumbuPositif.Length; i++){
                sumbuPositif[i] = GameObject.Find(namaSumbuPositif[i]).gameObject;
                
            }
        }

        void FindSumbuNegatifGameobjects(){
            sumbuNegatif = new GameObject[namaSumbuNegatif.Length];

            for (int i = 0; i < namaSumbuNegatif.Length; i++){
                sumbuNegatif[i] = GameObject.Find(namaSumbuNegatif[i]).gameObject;
            }
        }

        //============================================SETTER METHODS=======================================================================
        public void SetNamaSumbuPositif(string[] namaSumbuPositif){
            this.namaSumbuPositif = new string[namaSumbuPositif.Length];
            this.namaSumbuPositif = namaSumbuPositif;
            FindSumbuPositifGameobjects();
        }
        public void SetNamaSumbuNegatif(string[] namaSumbuNegatif){
            this.namaSumbuNegatif = new string[namaSumbuNegatif.Length];
            this.namaSumbuNegatif = namaSumbuNegatif;
            FindSumbuNegatifGameobjects();
        }

        //==============================================GETTER METHODS===================================================================
        public Sumbu GetSumbuYangTersambung(){
            return sumbuYangTersambung;
        }

        //===============================================COLLIDER ENTER========================================================================
        private void OnTriggerEnter(Collider other)
        {
            //Mengecek apakah yang tersambung adalah sumbu Positif
            for (int i = 0; i < sumbuPositif.Length; i++){
                if (other == sumbuPositif[i].GetComponent<Collider>()){
                    sumbuYangTersambung = Sumbu.SumbuPositif;
                    kabelController.SetSumbuYangTersambung(sumbuYangTersambung);
                    return;
                }
            }
            
            //Mengeck apakah yang tersambung adalah sumbu Negatif
            for (int i = 0; i < sumbuNegatif.Length; i++){
                if (other == sumbuNegatif[i].GetComponent<Collider>()){
                    sumbuYangTersambung = Sumbu.SumbuNegatif;
                    kabelController.SetSumbuYangTersambung(sumbuYangTersambung);
                    return;
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            //Mengecek apakah yang tersambung adalah sumbu Positif
            for (int i = 0; i < sumbuPositif.Length; i++){
                if (other == sumbuPositif[i].GetComponent<Collider>()){
                    sumbuYangTersambung = Sumbu.None;
                    kabelController.SetSumbuYangTersambung(sumbuYangTersambung);
                    return;
                }
            }
            
            //Mengeck apakah yang tersambung adalah sumbu Negatif
            for (int i = 0; i < sumbuNegatif.Length; i++){
                if (other == sumbuNegatif[i].GetComponent<Collider>()){
                    sumbuYangTersambung = Sumbu.None;
                    kabelController.SetSumbuYangTersambung(sumbuYangTersambung);
                    return;
                }
            }
        }
    }

    public enum Sumbu{
        SumbuPositif,
        SumbuNegatif,
        None

    }


}

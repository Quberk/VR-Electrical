using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Microsoft.MixedReality.Toolkit.Experimental.UI;

namespace VRMultiplayer.Template.Offline.UI.Keyboard{
    public class InputFieldKeyboard : MonoBehaviour
    {
        private TMP_InputField inputField;

        [Header("Keyboard Position")]
        [SerializeField] private bool cameraAsPositionSource = false;
        [SerializeField] private float distance = -0.1f;
        [SerializeField] private float verticalOffset = -0.2f;
        [SerializeField] private Transform positionSource;
        
        // Start is called before the first frame update
        void Start()
        {
            inputField = GetComponent<TMP_InputField>();
            inputField.onSelect.AddListener(x => OpenKeyboard());
            //Jika Camera sbg Position Source maka gunakan Main Camera sbg Position Source jika tidak maka gunakan InputField
            /*if (!cameraAsPositionSource)
                positionSource = gameObject.transform;
            
            else
                positionSource = GameObject.FindGameObjectWithTag("MainCamera").transform;*/
        }


        public void OpenKeyboard(){

            NonNativeKeyboard.Instance.InputField = inputField;
            NonNativeKeyboard.Instance.PresentKeyboard(inputField.text);

            Vector3 direction = positionSource.forward;
            direction.y = 0;
            direction.Normalize();

            Vector3 targetPosition = positionSource.position + direction * distance + Vector3.up * verticalOffset;

            NonNativeKeyboard.Instance.RepositionKeyboard(targetPosition, cameraAsPositionSource);

            SetCaretColorAlpha(1);

            NonNativeKeyboard.Instance.OnClosed += Instance_Closed;

        }

        private void Instance_Closed(object sender, System.EventArgs e){
            SetCaretColorAlpha(0);
            NonNativeKeyboard.Instance.OnClosed -= Instance_Closed;
        }

        public void SetCaretColorAlpha(float value){
            inputField.customCaretColor = true;
            Color caretColor = inputField.caretColor;
            caretColor.a = value;
            inputField.caretColor = caretColor;
        }
    } 

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace LocalizationFro
{
    public class TMP_Text_Setter_Array : MonoBehaviour
    {
        [SerializeField] string[] indexArray;

        private TextMeshProUGUI text;

        [SerializeField] private int startNumber = 0;

        private void Awake()
        {
            text = GetComponent<TextMeshProUGUI>();
        }

        private void OnEnable()
        {
            if (XMLManager.Instance != null)
            {
                XMLManager.Instance.OnLanguageChagned += SetText;
            }
        }

        private void OnDisable()
        {
            if (XMLManager.Instance != null)
            {
                XMLManager.Instance.OnLanguageChagned -= SetText;
            }
        }

        private void Start()
        {
            SetText();
        }

        private void SetText()
        {
            if (indexArray.Length == 0)
            {
                Debug.Log("Array를 확인해주세요");
                return;
            }

            text.text = XMLManager.Instance.GetText(indexArray[startNumber]);
        }


        private void SetText(int languageType)
        {
            //Debug.Log("언어 변경!");

            if (startNumber >= 0 && startNumber < indexArray.Length)
            {
                text.text = XMLManager.Instance.GetText(indexArray[startNumber]);
            }
            else
            {
                Debug.Log("Start Number : " + startNumber);
                Debug.Log("Start Number를 확인해 주세요!");
            }
        }


        public void NextText()
        {
            if (indexArray.Length == 0)
            {
                Debug.Log("Array를 확인해주세요");
                return;
            }

            //Debug.Log("다음 Text");
            startNumber++;
            if (startNumber >= indexArray.Length)
            {
                startNumber = startNumber = 0;
                SetText();
            }
            else
            {
                SetText();
            }

        }

        public void PreviousText()
        {
            if(indexArray.Length == 0)
            {
                Debug.Log("Array를 확인해주세요");
                return;
            }

            //Debug.Log("이전 Text");
            startNumber--;
            if (startNumber < 0)
            {
                startNumber = startNumber = indexArray.Length - 1;
                SetText();
            }
            else
            {
                SetText();
            }
        }
    }
}
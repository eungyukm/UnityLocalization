using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

namespace LocalizationFro
{
    public class TMP_Text_Setter : MonoBehaviour
    {
        [SerializeField] private string index;

        private TextMeshProUGUI text;

        private void Awake()
        {
            text = GetComponent<TextMeshProUGUI>();
        }

        private void OnEnable()
        {
            if(XMLManager.Instance != null)
            {
                XMLManager.Instance.OnLanguageChagned += SetText;
            }
        }

        private void OnDisable()
        {
            if(XMLManager.Instance != null)
            {
                XMLManager.Instance.OnLanguageChagned -= SetText;
            }
        }

        private void SetText(int languageType)
        {
            //Debug.Log("언어 변경!");
            text.text = XMLManager.Instance.GetText(index);
        }

        private void Start()
        {
            text.text = XMLManager.Instance.GetText(index);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LocalizationFro
{
    public class Text_Setter : MonoBehaviour
    {
        [SerializeField] string index;

        private Text text;

        private void Awake()
        {
            text = GetComponent<Text>();
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

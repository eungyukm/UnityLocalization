using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class TMP_Text_Setter : MonoBehaviour
{
    [SerializeField] string index;
    [SerializeField] XMLManager xmlManager;

    private TextMeshProUGUI text;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        xmlManager.OnLanguageChagned += SetText;
    }

    private void OnDisable()
    {
        xmlManager.OnLanguageChagned -= SetText;
    }

    private void SetText(int languageType)
    {
        Debug.Log("언어 변경!");
        text.text = xmlManager.GetText(index);
    }

    private void Start()
    {
        text.text = xmlManager.GetText(index);
    }
}

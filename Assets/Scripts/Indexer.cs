using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Indexer : MonoBehaviour
{
    [SerializeField] string index;
    [SerializeField] Manager_XML xmlManager;

    private TextMeshProUGUI text;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        text.text = xmlManager.GetText(index);
    }
}

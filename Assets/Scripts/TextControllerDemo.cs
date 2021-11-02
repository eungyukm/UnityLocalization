using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LocalizationFro;

public class TextControllerDemo : MonoBehaviour
{
    [SerializeField] private TMP_Text_Setter_Array tmp_Setter_Array1;
    [SerializeField] private TMP_Text_Setter_Array tmp_Setter_Array2;

    [SerializeField] private Text_Setter_Array text_Setter1;
    [SerializeField] private Text_Setter_Array text_Setter2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.N))
        {
            tmp_Setter_Array1.NextText();
            tmp_Setter_Array2.NextText();

            text_Setter1.NextText();
            text_Setter2.NextText();
        }

        if(Input.GetKeyDown(KeyCode.P))
        {
            tmp_Setter_Array1.PreviousText();
            tmp_Setter_Array2.PreviousText();

            text_Setter1.PreviousText();
            text_Setter2.PreviousText();
        }
    }
}

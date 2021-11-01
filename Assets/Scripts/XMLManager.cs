using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

namespace LocalizationFro
{
    public class XMLManager : MonoBehaviour
    {
        private const string xmlPath = "XML/Localization";
        private const string fileName = "Localization";
        private const string rootNodeName = "Localization";
        private const string childNodeName = "Text";

        //private const string 
        private Dictionary<string, XML_Text> textDic = new Dictionary<string, XML_Text>();

        public eLanguage language = eLanguage.KR; // -> 이부분 매니저단으로 옮겨야함

        [SerializeField] private TMP_Dropdown dropdown;

        public UnityAction<int> OnLanguageChagned;

        private static XMLManager instance;

        public static XMLManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<XMLManager>();
                }
                return instance;
            }
        }

        private void Awake()
        {
            LoadXmlFile(fileName);
        }

        private void Start()
        {
            dropdown.onValueChanged.AddListener(delegate {
                ChangeLangue(dropdown);
            });
        }

        /// <summary>
        /// 한글 테스트
        /// </summary>
        /// <param name="dropdown"></param>
        private void ChangeLangue(TMP_Dropdown dropdown)
        {
            //Debug.Log("dropdown.value : " + dropdown.value);
            switch (dropdown.value)
            {
                case (int)eLanguage.KR:
                    //Debug.Log("한글");
                    language = eLanguage.KR;
                    OnLanguageChagned?.Invoke((int)eLanguage.KR);
                    break;

                case (int)eLanguage.EN:
                    //Debug.Log("영어");
                    language = eLanguage.EN;
                    OnLanguageChagned?.Invoke((int)eLanguage.EN);
                    break;

                case (int)eLanguage.JP:
                    //Debug.Log("일어");
                    language = eLanguage.JP;
                    OnLanguageChagned?.Invoke((int)eLanguage.JP);
                    break;

                case (int)eLanguage.CH:
                    //Debug.Log("중국어");
                    language = eLanguage.CH;
                    OnLanguageChagned?.Invoke((int)eLanguage.CH);
                    break;
            }
        }


        /// <summary>
        /// Xml File 로드
        /// </summary>
        private void LoadXmlFile(string fileName)
        {
            TextAsset xmlTextAsset = Resources.Load<TextAsset>("XML/" + fileName) as TextAsset;
            if (xmlTextAsset == null)
            {
                Debug.Log("xml is null");
            }
            else
            {
                //Debug.Log(xmlTest.ToString());
                XmlNodeList xmlNodeList = GetXmlNodeList(xmlTextAsset.text, rootNodeName, childNodeName);

                if (xmlNodeList == null)
                {
                    Debug.Log("xml Node List is null");
                }
                else
                {
                    //Debug.Log("xml Node List is not null");
                }
                //Debug.Log("Node Count : " + xmlNodeList.Count);

                textDic = LoadXMLText(xmlTextAsset.text);

                //Debug.Log("Dic Count : " + textDic.Count);

                //Debug.Log("00000 : " + GetText("00000"));
            }
        }

        XmlNodeList GetXmlNodeList(string xml, string xmlFileName, string rootName)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xml);
            XmlNodeList xmlNodeList = xmlDoc.SelectNodes($"{xmlFileName}/{rootName}");
            return xmlNodeList;
        }

        private Dictionary<string, XML_Text> LoadXMLText(string xmlFile)
        {
            XmlNodeList nodeList = GetXmlNodeList(xmlFile, rootNodeName, childNodeName);
            Dictionary<string, XML_Text> textDic = new Dictionary<string, XML_Text>();
            string Idx = string.Empty;
            foreach (XmlNode node in nodeList)
            {
                XML_Text languageInfo = new XML_Text();

                #region Base
                if (node.SelectSingleNode("Idx") != null) Idx = node.SelectSingleNode("Idx").InnerText;
                #endregion

                #region Language
                if (node.SelectSingleNode("KR") != null) languageInfo.KR = node.SelectSingleNode("KR").InnerText;
                if (node.SelectSingleNode("EN") != null) languageInfo.EN = node.SelectSingleNode("EN").InnerText;
                if (node.SelectSingleNode("JP") != null) languageInfo.JP = node.SelectSingleNode("JP").InnerText;
                if (node.SelectSingleNode("CH") != null) languageInfo.CH = node.SelectSingleNode("CH").InnerText;
                #endregion

                //Debug.Log("idex Count : " + Idx);
                textDic.Add(Idx, languageInfo);
            }

            return textDic;
        }

        /// <summary>
        /// 언어에 따른 텍스트 뱉어줌
        /// </summary>
        /// <param name="Idx"></param>
        /// <returns></returns>
        public string GetText(string Idx)
        {
            if (textDic.ContainsKey(Idx))
            {
                XML_Text xmlText = textDic[Idx];
                string text = string.Empty;
                switch (language)
                {
                    case eLanguage.KR: text = xmlText.KR; break;
                    case eLanguage.EN: text = xmlText.EN; break;
                    case eLanguage.JP: text = xmlText.JP; break;
                    case eLanguage.CH: text = xmlText.CH; break;
                }
                if (string.IsNullOrEmpty(text)) //인덱스는 있지만 해당 언어의 텍스트가 없으면 인덱스번호 반환
                {
                    return Idx;
                }
                else //인덱스도 있고 해당 언어의 텍스트가 있으면 텍스트 반환
                {
                    return text;
                }
            }
            else //인덱스가 없으면 인덱스번호 반환
            {
                return Idx;
            }
        }
    }


    [Serializable]
    /// 기본텍스트
    struct XML_Text
    {
        public string Idx;
        public string KR;
        public string EN;
        public string JP;
        public string CH;
    }

    public enum eLanguage
    {
        KR,
        EN,
        JP,
        CH,
    }
}
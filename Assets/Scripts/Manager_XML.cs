using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class Manager_XML : MonoBehaviour
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

    
    [SerializeField] private TextMeshProUGUI hello;

    private void Awake()
    {
        LoadXmlFile(fileName);
    }

    private void OnEnable()
    {
        OnLanguageChagned += ChangeText;
    }

    private void Start()
    {
        dropdown.onValueChanged.AddListener(delegate {
            ChangeLangue(dropdown);
        });
    }

    private void ChangeText(int idx)
    {
        string text = GetText("00000");
        Debug.Log("Search Text : " + text);
        hello.text = text;
    }

    /// <summary>
    /// 한글 테스트
    /// </summary>
    /// <param name="dropdown"></param>
    private void ChangeLangue(TMP_Dropdown dropdown)
    {
        Debug.Log("dropdown.value : " + dropdown.value);
        switch(dropdown.value)
        {
            case (int)eLanguage.KR:
                Debug.Log("한글");
                language = eLanguage.KR;
                OnLanguageChagned?.Invoke((int)eLanguage.KR);
                break;

            case (int)eLanguage.EN:
                Debug.Log("영어");
                language = eLanguage.EN;
                OnLanguageChagned?.Invoke((int)eLanguage.EN);
                break;

            case (int)eLanguage.JP:
                Debug.Log("일어");
                language = eLanguage.JP;
                OnLanguageChagned?.Invoke((int)eLanguage.JP);
                break;

            case (int)eLanguage.CH:
                Debug.Log("중국어");
                language = eLanguage.CH;
                OnLanguageChagned?.Invoke((int)eLanguage.CH);
                break;
        }
    }


    /// <summary>
    /// 모든 Xml데이터 로드
    /// </summary>
    private void LoadXmlFile(string fileName)
    {
        TextAsset xmlTextAsset = Resources.Load<TextAsset>("XML/" + fileName) as TextAsset;
        if(xmlTextAsset == null)
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
                Debug.Log("xml Node List is not null");
            }
            Debug.Log("Node Count : " + xmlNodeList.Count);

            textDic = LoadXMLText(xmlTextAsset.text);

            Debug.Log("Dic Count : " + textDic.Count);

            Debug.Log("00000 : " + GetText("00000"));
        }
    }

    XmlNodeList GetXmlNodeList(string xml, string xmlFileName, string rootName)
    {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(xml);
        XmlNodeList xmlNodeList = xmlDoc.SelectNodes($"{xmlFileName}/{rootName}");
        return xmlNodeList;
    }


    /// <summary>
    /// 시퀀스 뽑아내기
    /// </summary>
    /// <param name="xmlFile"></param>
    /// <returns></returns>
    /*
    List<SequenceInfo> LoadSequenceXml(string str)
    {
        TextAsset textAsset = (TextAsset)Resources.Load(Cons.Path_Xml + str + "Info"); //밖으로 빼줌
        //Debug.Log(textAsset);
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(textAsset.text);

        XmlNodeList nodes = xmlDoc.SelectNodes(str + "Info/" + str);
        List<SequenceInfo> sequenceInfoList = new List<SequenceInfo>();
        foreach (XmlNode node in nodes)
        {
            SequenceInfo sequenceInfo = new SequenceInfo();
            #region Base
            if (node.SelectSingleNode("Idx") != null) sequenceInfo.Idx = int.Parse(node.SelectSingleNode("Idx").InnerText);
            if (node.SelectSingleNode("Type") != null) sequenceInfo.Type = Util.String2Enum<eType>(node.SelectSingleNode("Type").InnerText);
            #endregion

            #region UI
            if (node.SelectSingleNode("UI_Text") != null) sequenceInfo.UI_Text = node.SelectSingleNode("UI_Text").InnerText;
            if (node.SelectSingleNode("AnnounceRes") != null) sequenceInfo.AnnounceRes = node.SelectSingleNode("AnnounceRes").InnerText;
            if (node.SelectSingleNode("DescriptionAnnounce") != null) sequenceInfo.DescriptionAnnounce = node.SelectSingleNode("DescriptionAnnounce").InnerText;
            if (node.SelectSingleNode("UI_Tip") != null) sequenceInfo.UI_Tip = node.SelectSingleNode("UI_Tip").InnerText;
            if (node.SelectSingleNode("UI_Image") != null) sequenceInfo.UI_Image = node.SelectSingleNode("UI_Image").InnerText;
            if (node.SelectSingleNode("UI_Type") != null) sequenceInfo.UI_Type = Util.String2Enum<eUIType>(node.SelectSingleNode("UI_Type").InnerText);
            if (node.SelectSingleNode("UI_AutoClose") != null) sequenceInfo.UI_AutoClose = bool.Parse(node.SelectSingleNode("UI_AutoClose").InnerText);
            if (node.SelectSingleNode("UI_AutoSequence") != null) sequenceInfo.UI_AutoSequence = bool.Parse(node.SelectSingleNode("UI_AutoSequence").InnerText);
            if (node.SelectSingleNode("UI_Action") != null) sequenceInfo.UI_Action = node.SelectSingleNode("UI_Action").InnerText;//행동할 액션
            if (node.SelectSingleNode("UI_Animation") != null) sequenceInfo.UI_Animation = node.SelectSingleNode("UI_Animation").InnerText;//UI애니메이션 hsjoo 추가
            if (node.SelectSingleNode("UI_MirrorView") != null) sequenceInfo.UI_MirrorView = node.SelectSingleNode("UI_MirrorView").InnerText;//보여질 미러뷰
            if (node.SelectSingleNode("UI_DescriptionImage") != null) sequenceInfo.UI_DescriptionImage = node.SelectSingleNode("UI_DescriptionImage").InnerText;
            #endregion

            #region Move
            if (node.SelectSingleNode("Move_Position") != null)
            {
                sequenceInfo.Move_Position = node.SelectSingleNode("Move_Position").InnerText;
            }
            if (node.SelectSingleNode("Move_PositionHMI") != null)
            {
                sequenceInfo.Move_PositionHMI = node.SelectSingleNode("Move_PositionHMI").InnerText;
            }
            if (node.SelectSingleNode("Move_StartUISound") != null) sequenceInfo.Move_StartUISound = node.SelectSingleNode("Move_StartUISound").InnerText;
            if (node.SelectSingleNode("Move_ArriveSound") != null) sequenceInfo.Move_ArriveSound = node.SelectSingleNode("Move_ArriveSound").InnerText;
            #endregion

            #region View
            if (node.SelectSingleNode("View_Time") != null) sequenceInfo.View_Time = float.Parse(node.SelectSingleNode("View_Time").InnerText);
            if (node.SelectSingleNode("View_Target") != null) sequenceInfo.View_Target = node.SelectSingleNode("View_Target").InnerText;
            if (node.SelectSingleNode("MirrorViewName") != null) sequenceInfo.MirrorViewName = node.SelectSingleNode("MirrorViewName").InnerText;
            if (node.SelectSingleNode("AlarmLight") != null) sequenceInfo.LightType = Util.String2Enum<eLightType>(node.SelectSingleNode("AlarmLight").InnerText);
            if (node.SelectSingleNode("View_Type") != null) sequenceInfo.View_Type = Util.String2Enum<eViewType>(node.SelectSingleNode("View_Type").InnerText);
            if (node.SelectSingleNode("View_Pivot") != null) sequenceInfo.View_Pivot = node.SelectSingleNode("View_Pivot").InnerText;
            #endregion

            #region Button
            if (node.SelectSingleNode("Button_Name") != null) sequenceInfo.Button_Name = node.SelectSingleNode("Button_Name").InnerText;
            #endregion

            #region Interact
            if (node.SelectSingleNode("Interact_Target") != null) sequenceInfo.Interact_Target = node.SelectSingleNode("Interact_Target").InnerText;
            if (node.SelectSingleNode("Anim_Param") != null) sequenceInfo.Anim_Param = node.SelectSingleNode("Anim_Param").InnerText;
            #endregion

            #region Grabbable
            if (node.SelectSingleNode("Grabbable") != null) sequenceInfo.Grabbable = node.SelectSingleNode("Grabbable").InnerText;
            #endregion

            #region Sound
            if (node.SelectSingleNode("SoundA") != null) sequenceInfo.SoundA = node.SelectSingleNode("SoundA").InnerText;
            #endregion

            sequenceInfoList.Add(sequenceInfo);
        }
        return sequenceInfoList;
    }
     */


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

            Debug.Log("ide : " + Idx);
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

/// <summary>
/// 합치부 정보
/// </summary>
/*
[Serializable]
public class SequenceInfo : BaseInfo
{
    #region Base
    public int Idx; //인덱스
    public eType Type; //어떤 타입의 행동 할 것인지
    #endregion

    #region UI
    public string UI_Text; //UI텍스트
    public string AnnounceRes; //아나운서 음성
    public string DescriptionAnnounce; //아나운서 음성
    public string UI_Tip; //UITip텍스트
    public string UI_Image; //UI이미지
    public string UI_DescriptionImage; //UI이미지
    public eUIType UI_Type; //UI타입
    public bool UI_AutoClose; //UI오토클로즈 여부
    public bool UI_AutoSequence; //UI오토시퀀스흐름 여부
    public string UI_Action; //행동할 액션
    public string UI_Animation; //애니메이션
    public string UI_MirrorView;//보여질 미러뷰
    #endregion

    #region Move
    public string Move_Position; //이동할 포지션
    public string Move_PositionHMI; //HMI이동할 포지션
    public string Move_StartUISound; //이동할때 나는 사운드
    public string Move_ArriveSound; //이동 도착시 나는 사운드
    #endregion

    #region View
    public float View_Time; //바라보는 시간
    public string View_Target; //바라볼 오브젝트
    public eViewType View_Type; //뷰타입
    public eLightType LightType; //라이트 타입
    public string MirrorViewName;
    public string View_Pivot; //뷰피봇
    #endregion

    #region Button
    public string Button_Name; //버튼 이름
    #endregion

    #region Interact
    public string Interact_Target; //손을 갖다 댔을때 애니메이션이 작동할 대상
    public string Anim_Param;// 애니메이션 상태 파라미터 이름
    #endregion

    #region Grabbable
    public string Grabbable; //손을 갖다 댔을때 애니메이션이 작동할 대상
    #endregion

    #region Sound
    public string SoundA;
    #endregion
}
*/


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























/*
<Idx></Idx> <!--인덱스-->
<Type></Type> <!--타입-->
<Move_Position></Move_Position> <!--이동포지션-->
<Move_StartUISound></Move_StartUISound> <!--이동시 사운드-->
<Move_ArriveSound></Move_ArriveSound> <!--이동도착시 사운드-->
<View_Time></View_Time> <!--어떤 물체 확인 할  때 바라보는 시간-->
<View_Target></View_Target> <!--바라보는 물체-->
<UI_Text></UI_Text> <!--UI 텍스트-->
<UI_Type></UI_Type> <!--UI 타입-->
<UI_AutoClose></UI_AutoClose> <!--UI 자동 닫힐지 여부-->
<UI_AutoSequence></UI_AutoSequence> <!--UI 닫힐 때 시퀀스 넘길지 여부-->
<AlarmLight></AlarmLight> <!--알람 울릴 지 여부-->
 */

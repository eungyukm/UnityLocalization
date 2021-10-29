using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class Manager_XML : MonoBehaviour
{
    private const string xmlPath = "XML/XML_Text";
    //private const string 
    private Dictionary<string, XML_Text> textDic = new Dictionary<string, XML_Text>();

    public eLanguage language = eLanguage.KR; // -> �̺κ� �Ŵ��������� �Űܾ���

    [SerializeField] private TMP_Dropdown dropdown;

    public UnityAction<int> OnLanguageChagned;

    
    [SerializeField] private TextMeshProUGUI hello;

    private void Awake()
    {
        LoadXmlFile("XML_Text");
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

    private void ChangeLangue(TMP_Dropdown dropdown)
    {
        Debug.Log("dropdown.value : " + dropdown.value);
        switch(dropdown.value)
        {
            case (int)eLanguage.KR:
                Debug.Log("�ѱ�");
                language = eLanguage.KR;
                OnLanguageChagned?.Invoke((int)eLanguage.KR);
                break;

            case (int)eLanguage.EN:
                Debug.Log("����");
                language = eLanguage.EN;
                OnLanguageChagned?.Invoke((int)eLanguage.EN);
                break;

            case (int)eLanguage.JP:
                Debug.Log("�Ͼ�");
                language = eLanguage.JP;
                OnLanguageChagned?.Invoke((int)eLanguage.JP);
                break;

            case (int)eLanguage.CH:
                Debug.Log("�߱���");
                language = eLanguage.CH;
                OnLanguageChagned?.Invoke((int)eLanguage.CH);
                break;
        }
    }


    /// <summary>
    /// ��� Xml������ �ε�
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
            XmlNodeList xmlNodeList = GetXmlNodeList(xmlTextAsset.text, "XML_Text", "Text");

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
    /// ������ �̾Ƴ���
    /// </summary>
    /// <param name="xmlFile"></param>
    /// <returns></returns>
    /*
    List<SequenceInfo> LoadSequenceXml(string str)
    {
        TextAsset textAsset = (TextAsset)Resources.Load(Cons.Path_Xml + str + "Info"); //������ ����
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
            if (node.SelectSingleNode("UI_Action") != null) sequenceInfo.UI_Action = node.SelectSingleNode("UI_Action").InnerText;//�ൿ�� �׼�
            if (node.SelectSingleNode("UI_Animation") != null) sequenceInfo.UI_Animation = node.SelectSingleNode("UI_Animation").InnerText;//UI�ִϸ��̼� hsjoo �߰�
            if (node.SelectSingleNode("UI_MirrorView") != null) sequenceInfo.UI_MirrorView = node.SelectSingleNode("UI_MirrorView").InnerText;//������ �̷���
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
        XmlNodeList nodeList = GetXmlNodeList(xmlFile, "XML_Text", "Text");
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
    /// �� ���� �ؽ�Ʈ �����
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
            if (string.IsNullOrEmpty(text)) //�ε����� ������ �ش� ����� �ؽ�Ʈ�� ������ �ε�����ȣ ��ȯ
            {
                return Idx;
            }
            else //�ε����� �ְ� �ش� ����� �ؽ�Ʈ�� ������ �ؽ�Ʈ ��ȯ
            {
                return text;
            }
        }
        else //�ε����� ������ �ε�����ȣ ��ȯ
        {
            return Idx;
        }
    }

    #region �̻��

    /// <summary>
    /// �׽�Ʈ��..
    /// </summary>
    public void CreateXmlTest()
    {
        XmlDocument xmlDoc = new XmlDocument();

        // Xml�� �����Ѵ�(xml�� ������ ���ڵ� ����� �����ش�.)
        xmlDoc.AppendChild(xmlDoc.CreateXmlDeclaration("1.0", "utf-8", "yes"));

        //��Ʈ��忡 �������� ���� ������ �ǰ� �� �������� ���缭 �ൿ���� ��


        // ��Ʈ ��� ����
        XmlNode root = xmlDoc.CreateNode(XmlNodeType.Element, "ScenarioInfo", string.Empty);
        xmlDoc.AppendChild(root);

        //XmlNode child2 = xmlDoc.CreateNode(XmlNodeType.Element, "Character2", string.Empty);
        //root.AppendChild(child2);

        //// �ڽ� ��� ����
        //XmlNode child = xmlDoc.CreateNode(XmlNodeType.Element, "Character", string.Empty);
        //root.AppendChild(child);

        //// �ڽ� ��忡 �� �Ӽ� ����
        //XmlElement name = xmlDoc.CreateElement("�̸�");
        ////name.SetAttribute("����1", "up");
        ////name.SetAttribute("����2", "up");
        ////name.SetAttribute("����3", "up");
        //name.InnerText = "����";
        //child.AppendChild(name);

        //name = xmlDoc.CreateElement("����");
        //name.InnerText = "30";
        //child.AppendChild(name);

        //name = xmlDoc.CreateElement("�ּ�");
        //name.InnerText = "������";
        //child.AppendChild(name);

        // �ڽ� ��� ����
        XmlNode child = xmlDoc.CreateNode(XmlNodeType.Element, "Character", string.Empty);
        root.AppendChild(child);

        // �ڽ� ��忡 �� �Ӽ� ����
        XmlElement name = xmlDoc.CreateElement("�̸�");
        name.InnerText = "����2";
        child.AppendChild(name);

        XmlElement lv = xmlDoc.CreateElement("����");
        lv.InnerText = "302";
        child.AppendChild(lv);

        XmlElement exp = xmlDoc.CreateElement("�ּ�");
        exp.InnerText = "������2";
        child.AppendChild(exp);

        XmlNode child2 = xmlDoc.CreateNode(XmlNodeType.Element, "Character", string.Empty);
        root.AppendChild(child2);
        // �ڽ� ��忡 �� �Ӽ� ����
        XmlElement name2 = xmlDoc.CreateElement("�̸�");
        name2.InnerText = "����2";
        child2.AppendChild(name2);

        XmlElement lv2 = xmlDoc.CreateElement("����");
        lv2.InnerText = "302";
        child2.AppendChild(lv2);

        XmlElement exp2 = xmlDoc.CreateElement("�ּ�");
        exp2.InnerText = "������2";
        child2.AppendChild(exp2);

        xmlDoc.Save("./Assets/Resources/Character2.xml");
    }

    public void CreateXml()
    {
        XmlDocument xmlDoc = new XmlDocument();

        // Xml�� �����Ѵ�(xml�� ������ ���ڵ� ����� �����ش�.)
        xmlDoc.AppendChild(xmlDoc.CreateXmlDeclaration("1.0", "utf-8", "yes"));

        //��Ʈ��忡 �������� ���� ������ �ǰ� �� �������� ���缭 �ൿ���� ��


        // ��Ʈ ��� ����
        XmlNode root = xmlDoc.CreateNode(XmlNodeType.Element, "CharacterInfo", string.Empty);
        xmlDoc.AppendChild(root);

        // �ڽ� ��� ����
        XmlNode child = xmlDoc.CreateNode(XmlNodeType.Element, "Character", string.Empty);
        root.AppendChild(child);

        // �ڽ� ��忡 �� �Ӽ� ����
        XmlElement name = xmlDoc.CreateElement("Name");
        name.InnerText = "wergia";
        child.AppendChild(name);

        XmlElement lv = xmlDoc.CreateElement("Level");
        lv.InnerText = "1";
        child.AppendChild(lv);

        XmlElement exp = xmlDoc.CreateElement("Experience");
        exp.InnerText = "45";
        child.AppendChild(exp);

        xmlDoc.Save("./Assets/Resources/Character.xml");
    }

    public void LoadXml()
    {
        TextAsset textAsset = (TextAsset)Resources.Load("Character");
        Debug.Log(textAsset);
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(textAsset.text);

        XmlNodeList nodes = xmlDoc.SelectNodes("CharacterInfo/Character");

        foreach (XmlNode node in nodes)
        {
            Debug.Log("Name :: " + node.SelectSingleNode("Name").InnerText);
            Debug.Log("Level :: " + node.SelectSingleNode("Level").InnerText);
            Debug.Log("Exp :: " + node.SelectSingleNode("Experience").InnerText);
        }
    }

    #endregion
}


//public class BaseInfo
//{

//}

/// <summary>
/// ��ġ�� ����
/// </summary>
/*
[Serializable]
public class SequenceInfo : BaseInfo
{
    #region Base
    public int Idx; //�ε���
    public eType Type; //� Ÿ���� �ൿ �� ������
    #endregion

    #region UI
    public string UI_Text; //UI�ؽ�Ʈ
    public string AnnounceRes; //�Ƴ�� ����
    public string DescriptionAnnounce; //�Ƴ�� ����
    public string UI_Tip; //UITip�ؽ�Ʈ
    public string UI_Image; //UI�̹���
    public string UI_DescriptionImage; //UI�̹���
    public eUIType UI_Type; //UIŸ��
    public bool UI_AutoClose; //UI����Ŭ���� ����
    public bool UI_AutoSequence; //UI����������帧 ����
    public string UI_Action; //�ൿ�� �׼�
    public string UI_Animation; //�ִϸ��̼�
    public string UI_MirrorView;//������ �̷���
    #endregion

    #region Move
    public string Move_Position; //�̵��� ������
    public string Move_PositionHMI; //HMI�̵��� ������
    public string Move_StartUISound; //�̵��Ҷ� ���� ����
    public string Move_ArriveSound; //�̵� ������ ���� ����
    #endregion

    #region View
    public float View_Time; //�ٶ󺸴� �ð�
    public string View_Target; //�ٶ� ������Ʈ
    public eViewType View_Type; //��Ÿ��
    public eLightType LightType; //����Ʈ Ÿ��
    public string MirrorViewName;
    public string View_Pivot; //���Ǻ�
    #endregion

    #region Button
    public string Button_Name; //��ư �̸�
    #endregion

    #region Interact
    public string Interact_Target; //���� ���� ������ �ִϸ��̼��� �۵��� ���
    public string Anim_Param;// �ִϸ��̼� ���� �Ķ���� �̸�
    #endregion

    #region Grabbable
    public string Grabbable; //���� ���� ������ �ִϸ��̼��� �۵��� ���
    #endregion

    #region Sound
    public string SoundA;
    #endregion
}
*/


[Serializable]
/// �⺻�ؽ�Ʈ
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
<Idx></Idx> <!--�ε���-->
<Type></Type> <!--Ÿ��-->
<Move_Position></Move_Position> <!--�̵�������-->
<Move_StartUISound></Move_StartUISound> <!--�̵��� ����-->
<Move_ArriveSound></Move_ArriveSound> <!--�̵������� ����-->
<View_Time></View_Time> <!--� ��ü Ȯ�� ��  �� �ٶ󺸴� �ð�-->
<View_Target></View_Target> <!--�ٶ󺸴� ��ü-->
<UI_Text></UI_Text> <!--UI �ؽ�Ʈ-->
<UI_Type></UI_Type> <!--UI Ÿ��-->
<UI_AutoClose></UI_AutoClose> <!--UI �ڵ� ������ ����-->
<UI_AutoSequence></UI_AutoSequence> <!--UI ���� �� ������ �ѱ��� ����-->
<AlarmLight></AlarmLight> <!--�˶� �︱ �� ����-->
 */

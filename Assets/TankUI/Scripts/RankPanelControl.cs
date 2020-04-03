using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;

public class RankPanelControl : MonoBehaviour
{
    public Text[] usrObjs;
    public Text[] scoreObjs;

    private static XmlDocument rankXml;
    private static string xmlPath;

    void OnEnable()
    {
        Debug.Log(GameSession.getUserId() + ": rankPanelLoading...");
        xmlPath = Application.dataPath + "/TankUI/usrRank.xml";
        loadRankXml();
        loadRankPanel();
        Debug.Log("rankPanelLoaded!");
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void loadRankPanel(){
        List<RankInfo> ranks = getRankSort();
        if(ranks != null){
            int rankCnt = ranks.Count;
            rankCnt = rankCnt < 7 ? rankCnt : 7;

            for(int i = 0; i < rankCnt; i++){
                usrObjs[i].text = ranks[i].UserID;
                scoreObjs[i].text = ranks[i].Score + "";
            }
        }
    }

    private List<RankInfo> getRankSort()
    {
        List<RankInfo> ranks = null;
        //TO-DO:读取xml文件并键值对排序，取前10
        if (File.Exists(xmlPath))
        {
            rankXml.Load(xmlPath);
            XmlNodeList rankList = rankXml.GetElementsByTagName("usr");
            XmlNode usrNode = null;
            ranks = new List<RankInfo>();
            for (int i = 0; i < rankList.Count; i++)
            {
                usrNode = rankList.Item(i);
                RankInfo rankInfo = new RankInfo();
                rankInfo.UserID = usrNode.SelectSingleNode("usrId").InnerText;
                rankInfo.Score = int.Parse(usrNode.SelectSingleNode("usrScore").InnerText);
                ranks.Add(rankInfo);
            }
            //排序
            ranks.Sort((a,b) => {
                return b.Score.CompareTo( a.Score);
            });
        }
        return ranks;
    }

    private static void loadRankXml()
    {
        rankXml = new XmlDocument();
        if (File.Exists(xmlPath))
        {
            rankXml.Load(xmlPath);
        }
        else
        {
            rankXml = CreateXML();
            saveRank();
        }
    }

    private static void saveRank()
    {
        rankXml.Save(xmlPath);
    }

    public static void addUsrScoreInfo(string usrId, int usrScore)
    {
        xmlPath = Application.dataPath + "/TankUI/usrRank.xml";
        loadRankXml();
        bool usrExistFlg = false;
        XmlNodeList rankList = rankXml.GetElementsByTagName("usr");
        XmlNode usrNode = null;
        for (int i = 0; i < rankList.Count; i++)
        {
            usrNode = rankList.Item(i);
            if(usrNode.SelectSingleNode("usrId").InnerText.Equals(usrId))
            {
                usrExistFlg = true;
                usrNode.SelectSingleNode("usrScore").InnerText = usrScore + "";
                break;
            }
        }

        if(!usrExistFlg)
        {
            XmlNode rootNode = rankXml.SelectSingleNode("RankInfo");
            usrNode = rankXml.CreateElement("usr");

            XmlElement usrIdAttr = rankXml.CreateElement("usrId");
            usrIdAttr.InnerText = usrId;
            usrNode.AppendChild(usrIdAttr);

            XmlElement usrScoreAttr = rankXml.CreateElement("usrScore");
            usrScoreAttr.InnerText = usrScore + "";
            usrNode.AppendChild(usrScoreAttr);

            rootNode.AppendChild(usrNode);
        }
        saveRank();
    }

    public static XmlDocument CreateXML() 
    { 
        XmlDocument xml = new XmlDocument();
        xml.AppendChild(xml.CreateXmlDeclaration("1.0", "UTF-8", null));
        xml.AppendChild(xml.CreateElement("RankInfo"));
        return xml;
    }
}

public class RankInfo
{
    public string UserID { get; set; }
    public int Score { get; set; }
}

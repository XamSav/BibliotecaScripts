using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System;
using System.Xml.Serialization;
using System.Text;
[CreateAssetMenu(fileName = "ConexionXML", menuName = "Bases de Datos/ConexionXML", order = 1)]
public class XMLManager : ConexionBaseDeDatos
{
    XmlDocument xmlDoc = new XmlDocument();
    XmlNode rootNode;
    public override void CheckBase()
    {
        string filename = "InfoPlayer.xml";
        if (System.IO.File.Exists(filename))
        {
            xmlDoc.Load(filename);
            rootNode = xmlDoc.SelectSingleNode("Stats");
        }
        else
        {
            rootNode = xmlDoc.CreateElement("Stats");
            xmlDoc.AppendChild(rootNode);
            //Horas Jugadas
            XmlNode userNode = xmlDoc.CreateElement("hours_played");
            XmlAttribute attribute = xmlDoc.CreateAttribute("hours");
            attribute.Value = "0";
            userNode.Attributes.Append(attribute);
            attribute = xmlDoc.CreateAttribute("minutes");
            attribute.Value = "0";
            userNode.Attributes.Append(attribute);
            rootNode.AppendChild(userNode);
            //Veces que ha completado el juego
            userNode = xmlDoc.CreateElement("times_completed");
            userNode.InnerText = "0";
            rootNode.AppendChild(userNode);
            //Veces que ha muerto
            userNode = xmlDoc.CreateElement("deaths");
            userNode.InnerText = "0";
            rootNode.AppendChild(userNode);
            //Partida mas rapida
            userNode = xmlDoc.CreateElement("record_time");
            attribute = xmlDoc.CreateAttribute("hours");
            attribute.Value = "0";
            userNode.Attributes.Append(attribute);
            attribute = xmlDoc.CreateAttribute("minutes");
            attribute.Value = "0";
            userNode.Attributes.Append(attribute);
            attribute = xmlDoc.CreateAttribute("seconds");
            attribute.Value = "0";
            userNode.Attributes.Append(attribute);
            rootNode.AppendChild(userNode);
            //Partida mas larga
            userNode = xmlDoc.CreateElement("longest_game");
            attribute = xmlDoc.CreateAttribute("hours");
            attribute.Value = "0";
            userNode.Attributes.Append(attribute);
            attribute = xmlDoc.CreateAttribute("minutes");
            attribute.Value = "0";
            userNode.Attributes.Append(attribute);
            attribute = xmlDoc.CreateAttribute("seconds");
            attribute.Value = "0";
            userNode.Attributes.Append(attribute);
            rootNode.AppendChild(userNode);

            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Encoding = new UTF8Encoding(false); // Falso = no escribir BOM
            settings.Indent = true;
            XmlWriter writer = XmlTextWriter.Create(filename, settings);
            xmlDoc.Save(writer);
        }
    }
    //GETTERS
    public override int[,] GetAllRuns()
    {
        int[,] pepe;
        string[] nodos = new string[] { "num","Kills", "Rooms", "Win", "Score", "Hours", "Minutes", "Seconds"};
        XmlNodeList nodeList = rootNode.SelectNodes("//Stats/Run");
        int a = nodeList.Count;
        pepe = new int[a,8];
        for (int v = 0; v < a; v++)
        {
            pepe[v, 0] = Int32.Parse(nodeList[v].Attributes["num"].Value);
            for (int s = 1; s < 8; s++)
            {
                pepe[v, s] = Int32.Parse(nodeList[v].SelectSingleNode(nodos[s]).InnerText);
            }
        }
        return pepe;
    }
    public override int[] GetStats()
    {
        int[] data = new int[10];
        string[] nodes = new string[] { "hours_played", "times_completed", "deaths", "record_time", "longest_game" };
        string[] atributos = new string[] { "hours", "minutes", "seconds" };
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load("InfoPlayer.xml");
        int CurrentNode = 0; ;
        for (int s = 0; s < data.Length; s++)
        {
            XmlNode nodeXML = xmlDoc.SelectSingleNode("//Stats/" + nodes[CurrentNode]);
            if (nodeXML.Attributes.Count > 0)
            {
                for (int i = 0; i < nodeXML.Attributes.Count; i++)
                {
                    data[s] = Int32.Parse(nodeXML.Attributes[atributos[i]].Value);
                    if(i+1 != nodeXML.Attributes.Count)
                        s++;
                }
            }
            else
            {
                data[s] = Int32.Parse(nodeXML.InnerText);
            }
            CurrentNode++;
        }
        return data;
    }
    public override int[] GetRun(int runN)
    {
        
        string[] nodes = new string[] { "num", "Kills", "Rooms", "Win", "Score", "Hours", "Minutes", "Seconds" };
        int[] data = new int[nodes.Length];
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load("InfoPlayer.xml");
        XmlNodeList nodeXML = xmlDoc.SelectNodes("//Stats/Run[@num='"+runN+"']");
        if (nodeXML.Count > 0)
        {
            for (int s = 1; s < data.Length; s++)
            {
                data[0] = runN;
                data[s] = Int32.Parse(nodeXML[0].SelectSingleNode(nodes[s]).InnerText);
            }
        }
        else
        {
            data = null;
        }
        return data;
    }
    //SETTERS
    public override void UpdateStats(int[] time, int timeCompleted, int deaths, int[] recoredTime, int[] Longesttime)
    {
        XmlNode timeXML = rootNode.SelectSingleNode("hours_played");
        timeXML.Attributes["hours"].Value = time[0].ToString();
        timeXML.Attributes["minutes"].Value = time[1].ToString();
        XmlNode timeCompletedXML = rootNode.SelectSingleNode("times_completed");
        timeCompletedXML.InnerText = timeCompleted.ToString();
        XmlNode deathsXML = rootNode.SelectSingleNode("deaths");
        deathsXML.InnerText = deaths.ToString();
        XmlNode RecoredtimeXML = rootNode.SelectSingleNode("record_time");
        RecoredtimeXML.Attributes["hours"].Value = recoredTime[0].ToString();
        RecoredtimeXML.Attributes["minutes"].Value = recoredTime[1].ToString();
        RecoredtimeXML.Attributes["seconds"].Value = recoredTime[2].ToString();
        XmlNode LongesttimeXML = rootNode.SelectSingleNode("longest_game");
        LongesttimeXML.Attributes["hours"].Value = Longesttime[0].ToString();
        LongesttimeXML.Attributes["minutes"].Value = Longesttime[1].ToString();
        LongesttimeXML.Attributes["seconds"].Value = Longesttime[2].ToString();
        xmlDoc.Save("InfoPlayer.xml");
    }
    public override void InsertRun(int[] data)//Data 1 - Asesinatos, Data 2 - Salas Exploradas, Data 3 - Ganar, Data 4 - Puntuacion Final, Data 5 - Horas, Data 6 - Minutos, Data 7 - Segundos
    {
        int runs = xmlDoc.SelectNodes("//Stats//Run").Count;
        string[] elementos = new string[] { "Kills", "Rooms", "Win","Score", "Hours", "Minutes", "Seconds"};
        XmlNode userNode;
        XmlNode RunXML = xmlDoc.CreateElement("Run");
        XmlAttribute attribute = xmlDoc.CreateAttribute("num");
        attribute.Value = runs.ToString();
        RunXML.Attributes.Append(attribute);
        for (int s = 0; s < data.Length; s++)
        {
            userNode = xmlDoc.CreateElement(elementos[s]);
            userNode.InnerText = data[s].ToString();
            RunXML.AppendChild(userNode);
        }
        rootNode.AppendChild(RunXML);
        xmlDoc.Save("InfoPlayer.xml");
    }
}

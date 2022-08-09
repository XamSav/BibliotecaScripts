using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using System.IO;
using UnityEngine.UI;
[CreateAssetMenu(fileName = "ConexionSQLite", menuName = "Bases de Datos/ConexionSQLite", order = 1)]

public class ConexionesSQLite : ConexionBaseDeDatos
{
    IDbConnection dbconn;
    IDbCommand cmd;
    public override void CheckBase()
    {
        string path = "./Assets/Saves.db";
        if (!File.Exists(path))
        {
            File.CreateText(path);
            //FileSecurity fSecurity = File.GetAccessControl(path);
            //fSecurity.AddAccessRule(new FileSystemAccessRule(account, rights, controlType));
            //File.SetAccessControl(fileName, fSecurity);
            try
            {
                Open();
                cmd = dbconn.CreateCommand();
                string query = "CREATE TABLE stats ( hours_played_H INTEGER, hours_played_M INTEGER, times_completed INTEGER, deaths INTEGER, record_time_H INTEGER, record_time_M INTEGER, record_time_S INTEGER, longest_game_H INTEGER, longest_game_M INTEGER, longest_game_S INTEGER);";
                cmd.CommandText = query;
                IDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Debug.Log(reader.GetString(0));
                }
                //cmd.ExecuteNonQuery();
                query = "CREATE TABLE runs ( num INTEGER PRIMARY KEY, Kills INTEGER, Rooms INTEGER, Win INTEGER, Score INTEGER, Hours INTEGER, Minutes INTEGER, Seconds INTEGER);";
                cmd.CommandText = query;
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
            }
        }
        else
        {
            Open();
            cmd = dbconn.CreateCommand();
        }
    }
    #region Gets
    public override int[,] GetAllRuns()
    {
        int[,] data;
        try
        {
            Open();
            cmd = dbconn.CreateCommand();
            string query = "SELECT COUNT(*) FROM runs;";
            cmd.CommandText = query;
            IDataReader readerR = cmd.ExecuteReader();
            int countRows = 0;
            while (readerR.Read())
            {
                countRows = readerR.GetInt32(0);
            }
            readerR.Close();
            ///
            query = "SELECT * FROM runs";
            cmd.CommandText = query;
            IDataReader reader = cmd.ExecuteReader();
            int count = 0;
            data = new int[countRows,8];
            while (reader.Read())
            {
                for (int s = 0; s < 8; s++)
                    data[count, s] = reader.GetInt32(s);
                count++;
            }
            Close();
            return data;
        }
        catch (Exception e)
        {
            Close();
            Debug.LogError(e.Message);
            return null;
        }
    }
    public override int[] GetRun(int run)
    {
        int[] data;
        try
        {
            Open();
            data = new int[7];
            cmd = dbconn.CreateCommand();
            string query = "SELECT * FROM runs WHERE numRun ='" + run + "'";
            Debug.Log(query);
            cmd.CommandText = query;
            IDataReader reader = cmd.ExecuteReader();
            reader.Read();
            if (reader.FieldCount > 0)
            {
                for (int s = 0; s < 8; s++)
                    data[s] = reader.GetInt32(s);
            }
            else
                data = null;
            Close();
            return data;
        }
        catch (Exception e)
        {
            Close();
            Debug.LogError(e.Message);
            return null;
        }
    }

    public override int[] GetStats()
    {
        int[] data;
        try
        {
            data = new int[7];
            cmd = dbconn.CreateCommand();
            string query = "SELECT * FROM stats";
            Debug.Log(query);
            cmd.CommandText = query;
            IDataReader reader = cmd.ExecuteReader();
            reader.Read();
            for (int s = 0; s < 5; s++)
                data[s] = reader.GetInt32(s);
            return data;
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
            return null;
        }
    }
    #endregion
    #region Sets
    public override void InsertRun(int[] data)
    {
        try
        {
            Open();
            string query = "INSERT INTO runs(Kills, Rooms, Win, Score, Hours, Minutes, Seconds) VALUES(" + data[0] + "," + data[1] + "," + data[2] + "," + data[3] + "," + data[4] + "," + data[5] + "," + data[6] + ")";
            cmd.CommandText = query;
            cmd.ExecuteNonQuery();
            Close();
        }
        catch (Exception e)
        {
            Close();
            Debug.Log(e.Message);
        }
    }
    public override void UpdateStats(int[] time, int timeCompleted, int deaths, int[] recoredTime, int[] Longesttime)
    {
        try
        {
            Open();
            string query = "UPDATE stats SET hours_played_H = "+time[0]+ ", hours_played_M ="+time[1]+ ", times_completed ="+timeCompleted+", deaths = "+deaths+ ", record_time_H ="+recoredTime[0]+ ", record_time_M = "+ recoredTime[1]+ ", record_time_S = "+recoredTime[2]+ ", longest_game_H ="+ Longesttime[0]+ ", longest_game_M = " + Longesttime[1]+ ", longest_game_S ="+Longesttime[2];
            cmd.CommandText = query;
            cmd.ExecuteNonQuery();
            Close();
        }
        catch (Exception e)
        {
            Close();
            Debug.Log(e.Message);
        }
    }
    #endregion
    #region Conexiones
    //Conexiones
    public void Close()
    {
        dbconn.Close();
    }
    public void Open()
    {
        string conn = "URI=file:" + Application.dataPath + "/Scripts/ConexionesBasesDeDatos/Saves.db";
        dbconn = (IDbConnection)new SqliteConnection(conn);
        try
        {
            dbconn.Open();
            Debug.Log("Conexion establecida");
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
    }
    #endregion
}

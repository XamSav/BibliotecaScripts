using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class Driver : MonoBehaviour
{
    [SerializeField] private ConexionBaseDeDatos conexion;
    public static Driver dr;
    private void Start()
    {
        if(dr != null && dr != this)
        {
            Destroy(this);
        }
        else
        {
            dr = this;
        }
        conexion.CheckBase();
    }
    public void InsertNewRun(int kills, int rooms, int win, int score, int hours, int minutes, int seconds)
    {
        int[] data = new int[] { kills, rooms, win, score, hours, minutes, seconds };
        conexion.InsertRun(data);
    }
    public int[,] GetAllRuns()
    {
        int[,] a = conexion.GetAllRuns();
        if (a != null)
        {
            for (int s = 0; s < a.Length/8; s++)
            {
                for (int v = 0; v < 8; v++)
                    Debug.Log(a[s, v]);
            }
        }
        return a;
    }
    public void GetRun(int runN)
    {
        int[] data = conexion.GetRun(runN);
        if (data != null)
        {
            for (int s = 0; s < data.Length; s++)
                Debug.Log(data[s]);
        }
        else
        {
            Debug.Log("No existe esa run");
        }
    }
    public void GetStats()
    {
        conexion.GetStats();
        //conexion.GetUser(id);
    }
    public void Insertar()
    {
        conexion.InsertRun(new int[] { 1, 14, 1, 0, 50, 1, 1 });
    }
}

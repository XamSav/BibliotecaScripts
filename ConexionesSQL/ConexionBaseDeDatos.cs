using Mono.Data.Sqlite;
using System.Data;
using UnityEngine;
using System;
using UnityEngine.UI;
using System.Collections.Generic;
public abstract class ConexionBaseDeDatos : ScriptableObject, IConexion
{
    public abstract void CheckBase();
    public abstract int[,] GetAllRuns();
    public abstract int[] GetRun(int run);
    public abstract void InsertRun(int[] data);
    public abstract void UpdateStats(int[] time, int timeCompleted, int deaths, int[] recoredTime, int[] Longesttime);
    public abstract int[] GetStats();
}

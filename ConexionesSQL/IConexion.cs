using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public interface IConexion
{
    void CheckBase();
    int[,] GetAllRuns();
    int[] GetRun(int run);
    void InsertRun(int[] data);
    void UpdateStats(int[] time, int timeCompleted, int deaths, int[] recoredTime, int[] Longesttime);
    int[] GetStats();
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "DecideHuir", menuName = "Maquina Estados/Desicion/DecideHuir", order = 2)]
public class DecideScape : Desicion
{
    public override bool Decide(EnemyController e)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        return (Vector3.SqrMagnitude(e.transform.position - player.transform.position) < 20.0f);
    }
}

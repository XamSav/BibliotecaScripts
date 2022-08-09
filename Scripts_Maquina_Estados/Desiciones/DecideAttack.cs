using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Desicion", menuName = "Maquina Estados/Desicion/DecideAtacar", order = 1)]
public class DecideAttack : Desicion
{
    public override bool Decide(EnemyController e)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        return (Vector3.SqrMagnitude(e.transform.position - player.transform.position) > 25.0f);
    }
}

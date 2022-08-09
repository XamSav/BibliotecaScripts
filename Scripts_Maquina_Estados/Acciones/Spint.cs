using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Spint", menuName = "Maquina Estados/Acciones/Spint", order = 1)]
public class Spint : ActionFSM
{
    public override void Act(EnemyController enemyController)
    {
        enemyController.gameObject.transform.Rotate(0,1,0);
    }
}

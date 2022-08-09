using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Shoot", menuName = "Maquina Estados/Acciones/Shoot", order = 1)]
public class Shoot : ActionFSM
{
    public override void Act(EnemyController enemyController)
    {
        enemyController.GetComponent<ShootManager>().StartShoot(enemyController);
    }
}
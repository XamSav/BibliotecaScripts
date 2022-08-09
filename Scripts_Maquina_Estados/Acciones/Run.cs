using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Run", menuName = "Maquina Estados/Acciones/Run", order = 2)]
public class Run : ActionFSM
{
    public override void Act(EnemyController enemyController)
    {
        GameObject enemy = enemyController.gameObject;
        enemy.transform.Translate(Vector3.forward* 4 * Time.deltaTime, Space.Self);
        enemy.GetComponent<ShootManager>().StopShoot();
    }
}

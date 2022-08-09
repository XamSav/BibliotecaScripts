using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Estado", menuName = "Maquina Estados/Estado", order = 2)]
public class State : ScriptableObject
{
    public ActionFSM[] acciones;
    //public Desicion[] desiciones;
    public Transition[] transiciones;
    public void UpdateState(EnemyController enemyController)
    {
        ExecuteActions(enemyController);
        CheckTransitions(enemyController);
    }

    private void CheckTransitions(EnemyController enemyController)
    {
        foreach(Transition t in transiciones)
        {
            if (t.desicion.Decide(enemyController))
            {
                enemyController.estadoActual = t.estado;
            }
        }
    }
    private void ExecuteActions(EnemyController enemyController)
    {
        foreach(ActionFSM a in acciones)
        {
            a.Act(enemyController);
        }
    }
}

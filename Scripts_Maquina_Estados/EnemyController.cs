using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public State estadoActual;
    void Start()
    {
        
    }

    void Update()
    {
        estadoActual.UpdateState(this);
    }
}

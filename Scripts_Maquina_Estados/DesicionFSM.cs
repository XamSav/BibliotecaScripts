using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DesicionFSM : ScriptableObject
{
    public abstract bool Decide(EnemyController e);
}

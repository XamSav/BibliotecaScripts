using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootManager : MonoBehaviour
{
    [SerializeField] private GameObject _prefabBullet;
    [SerializeField] private GameObject _shootpoint;
    [SerializeField] private bool start = false;
    public void StartShoot(EnemyController enemyController)
    {
        if (!start)
        {
            StartCoroutine(shoot());
            start = true;
        }
    }
    IEnumerator shoot()
    {
        Instantiate(_prefabBullet, _shootpoint.transform.position, _shootpoint.transform.rotation);
        yield return new WaitForSeconds(1);
        StartCoroutine(shoot());
    }
    public void StopShoot()
    {
        StopAllCoroutines();
        start = false;
    }
}

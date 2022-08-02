using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] _platforms; //Prefabs de las plataformas a instanciar
    [SerializeField] private GameObject _parentPlatforms; //Contenedor de las plataformas, para limpiar en caso de necesitar
    [SerializeField] private float _distancePlatforms = 4f; // Distancia entre plataformas
    [SerializeField] private GameObject lastPlatform; // Ultima plataforma, para saber la posicion
    [SerializeField] private GameObject _player;  
    // More JumpForce = More Space
    // Jump = 1.53f
    // Pos Y Min = -6f
    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }
    public void SpawnPlatfom()
    {
        GameObject instancia = SelectPlatorm();
        Vector3 posPlatform = GeneratePosition();
        lastPlatform = Instantiate(instancia,posPlatform,Quaternion.identity, _parentPlatforms.transform);
    }
    public GameObject SelectPlatorm()
    {
        return _platforms[Random.Range(0, _platforms.Length)];
    }
    public Vector3 GeneratePosition()
    {
        //Player Y + Jump
        float yPosition = 0;
        if (lastPlatform.transform.position.y + 1.53f <= 0)
            yPosition = Random.Range(-6f, lastPlatform.transform.position.y + 1.53f);
        else
            yPosition = Random.Range(-6, 0f);
        float xPosition = lastPlatform.transform.position.x + _distancePlatforms;
        return new Vector3(xPosition, yPosition, 0);
    }
}

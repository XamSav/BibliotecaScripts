using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class Neurona1 : MonoBehaviour
{
    float[,] entradas = { { 1, 1 }, { 1, 0 }, { 0, 1 }, { 0, 0 } };
    float[] peso;
    float umbral;
    int[] salida = {1,0,0};
    bool proceso = true;
    int iteracciones = 0;
    bool firstime = true;
    float tasaAprendizaje = 0.03f;
    float error;
    private void Start()
    {
        peso = new float[2];
        for (int s = 0; s < peso.Length; s++)
        {
            peso[s] = Random.value;
            Debug.Log("Inicial: Peso " + s + ": " + peso[s]);
        }
    }
    private void Update()
    {
        if (firstime)
        {
            while (proceso)
            {
                iteracciones++;
                proceso = false;
                for (int v = 0; v < peso.Length; v++)
                {
                    float r = entradas[v,0] * peso[0] + entradas[v,1] * peso[1] + umbral;
                    int f = r < 0.7f ? 0 : 1;
                    error = salida[v] - f;
                    if(error != 0)
                    {
                        peso[0] += tasaAprendizaje * error * entradas[v, 0];
                        peso[1] += tasaAprendizaje * error * entradas[v, 1];
                        umbral += tasaAprendizaje * error * 1;
                        proceso = true;
                    }
                }
            }
            firstime = false;
            for (int s = 0; s < peso.Length; s++)
            {
                Debug.Log("Acabado: Peso " + s + ": " + peso[s]);
                Debug.Log("Perceptron" + s + ": " + salida[s]);
            }
            Debug.Log("Salio con "+ iteracciones + " iteracciones");
        }
    }
}

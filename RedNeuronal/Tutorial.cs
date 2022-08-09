using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    int[,] entradas = { { 1, 1 }, { 1, 0 }, { 0, 1 }, { 0, 0 } };
    int[] salidas = { 1, 0, 0, 0 };
    float P0, P1, U;
    bool proceso = true;
    int iteracion = 0;
    private void Start()
    {
        P0 = Random.value;
        P1 = Random.value;
        U = Random.value;
        Debug.Log("P0: " + P0 + " P1: " + P1 +" U: "+U);
        while (proceso)
        {
            iteracion++;
            proceso = false;
            for (int cont = 0; cont <= 3; cont++)
            {
                float operacion = entradas[cont, 0] * P0 + entradas[cont, 1] * P1 + U;
                int salidaEntera = operacion > 0.7 ? 1 : 0;
                if (salidaEntera != salidas[cont])
                {
                    P0 = Random.value;
                    P1 = Random.value;
                    U = Random.value;
                    proceso = true;
                    Debug.Log("Nº:"+iteracion+" P0: " + P0 + " P1: " + P1 + " U: " + U);
                }
            }
        }






        for (int cont = 0; cont <= 3; cont++)
        {
            float operacion = entradas[cont, 0] * P0 + entradas[cont, 1] * P1 + U;
            int salidaEntera;
            if (operacion > 0.7)
            {
                salidaEntera = 1;
            }
            else { 
                salidaEntera = 0;
            }
            Debug.Log("Entradas: " + entradas[cont, 0].ToString() + " y " +
                entradas[cont, 1].ToString() + " = " + salidas[cont].ToString() + " perceptron: " + salidaEntera.ToString());
        }
        Debug.Log("Pesos encontrados P0= " + P0.ToString() + " P1= " + P1.ToString() + " U= " + U.ToString());
        Debug.Log("Iteraciones requeridas: " + iteracion.ToString());
    }
}
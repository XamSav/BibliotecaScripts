using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimo : MonoBehaviour
{
    static void Main(string[] args)
    {
        float x = 1;
        //valor inicial
        float Yini = (float)Ecuacion(x);
        float variacion = 1;
        while (Mathf.Abs(variacion) > 0.00001)
        {
            float Ysigue = (float)Ecuacion(x + variacion);
            if (Ysigue > Yini)
            { 
                //Si no disminuye, cambia de direccion a un paso menor
                variacion *= -1; variacion /= 10; 
            } else {
                Yini = Ysigue; //Disminuye
                x += variacion;
                Debug.Log("x: " + x.ToString() + " Yini:" + Yini.ToString());
            }
        }
        Debug.Log("Respuesta: " + x.ToString()); 
    }
    static double Ecuacion(double x) 
    {
        return 5 * x * x - 7 * x - 13;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RedUnity
{
    public class RedN : MonoBehaviour
    {
        Transform you;
        float jumpForce = 0.5f;
        Perceptron perceptron;
        private void Start()
        {
            you = GetComponent<Transform>();
            perceptron = new Perceptron();
            int numEntradas = 2; //Número de entradas
            int NeuronasCapa0 = 3; //Total neuronas en la capa 0
            int NeuronasCapa1 = 4; //Total neuronas en la capa 1
            int NeuronasCapa2 = 1; //Total neuronas en la capa 2
            perceptron.creaCapas(numEntradas, NeuronasCapa0, NeuronasCapa1, NeuronasCapa2);
            List<float> entradas = new List<float>();   //Primera Entradas
            //entradas.Add(you.position.x);
            entradas.Add(you.position.y);
            //entradas.Add(you.position.z);
            entradas.Add(jumpForce);
            //Se hace el cálculo
            perceptron.calculaSalida(entradas);
        }
        private void Update()
        {
            List<float> entradas = new List<float>();
            entradas.Add(you.position.y);
            entradas.Add(jumpForce);
            Debug.Log(perceptron.calculaSalida(entradas));
        }
    }
    class Perceptron
    {
        List<Capa> capas;
        public void creaCapas(int numEntrada, int capa0, int capa1,int capa2)
        {
            capas = new List<Capa>();
            capas.Add(new Capa(capa0, numEntrada)); //Crea la capa 0
            capas.Add(new Capa(capa1, capa0)); //Crea la capa 1
            capas.Add(new Capa(capa2, capa1)); //Crea la capa 2
        }
        public float calculaSalida(List<float> entrada)
        {
            capas[0].CalculaCapa(entrada);
            capas[1].CalculaCapa(capas[0].salidas);
            capas[2].CalculaCapa(capas[1].salidas);
            return capas[2].salidas[0];
        }
    }

    class Capa
    {
        List<Neurona> neuronas;
        public List<float> salidas;
        public Capa(int NNeuronas, int Entradas){
            salidas = new List<float>();
            neuronas = new List<Neurona>();
            for (int s = 0; s < NNeuronas; s++)
            {
                neuronas.Add(new Neurona(Entradas));
                salidas.Add(0);
            }
        }
        public void CalculaCapa(List<float> entradas)
        {
            for (int s = 0; s < neuronas.Count; s++)
                salidas[s] = neuronas[s].calculaSalida(entradas);
        }

    } 
    class Neurona
    {
        float umbral;
        List<float> pesos;
        //float[] salidas = { 0, 1, 0};
        //float error;
        //float tazaAprendizaje = 0.03f;
        public Neurona(int TotalEntradas)
        {
            pesos = new List<float>();
            for (int s = 0; s < TotalEntradas; s++)
                pesos.Add(Random.value);
            umbral = Random.value;
        }
        public float calculaSalida(List<float> entradas)
        {
            float valor = 0;
            for (int cont = 0; cont < pesos.Count; cont++)
            {
                valor += entradas[cont] * pesos[cont];
            }
            valor += umbral;
            
            return 1 / (1 + Mathf.Exp(-valor));
        }



    }/*
    public class TestMemoriaSam : MonoBehaviour
    {
        float[] entradas;
        float umbral;
        List<float> pesos;
        float[] salidas = { 1, 1, 1, 0 };
        bool proceso = true;
        int iteraccion = 0;
        float error;
        float tazaAprendizaje = 0.03f;
        




        private void Start()
        {
            entradas = new float[] { transform.position.x, transform.position.y, transform.position.z };
            for (int s = 0; s < pesos.Count; s++)
            {
                pesos[s] = Random.value;
            }
            umbral = Random.value;
            while (proceso)
            {
                iteraccion++;
                proceso = false;
                for (int s = 0; s <= 3; s++)
                {
                    float r = entradas[s] * pesos[0];
                    r += umbral;
                    int f = r < 0.7f ? 0 : 1;
                    error = salidas[s] - f;
                    if (error != 0)
                    {
                        pesos[0] += tazaAprendizaje * error * entradas[s];
                        pesos[1] += tazaAprendizaje * error * entradas[s];
                        umbral += tazaAprendizaje * error * 1;
                        proceso = true;
                    }
                }
            }
            ATexto();
        }
        public float calculaSalida(List<float> entradas)
        {
            float valor = 0;
            for (int cont = 0; cont < pesos.Count; cont++)
            {
                valor += entradas[cont] * pesos[cont];
            }
            valor += umbral;
            return 1 / (1 + Mathf.Exp(-valor));
        }
        private void ATexto()
        {
            for (int cont = 0; cont <= 3; cont++)
            {
                float operacion = entradas[cont] * pesos[0] + entradas[cont] * pesos[1] + umbral;
                int salidaEntera = operacion < 0.7f ? 0 : 1;
                Debug.Log("Entradas: " + entradas[cont].ToString() + " y " +
                    entradas[cont].ToString() + " = " + salidas[cont].ToString() + " perceptron: " + salidaEntera.ToString());
            }
            Debug.Log("Pesos encontrados P0= " + pesos[0].ToString() + " P1= " + pesos[1].ToString() + " U= " + umbral.ToString());
            Debug.Log("Iteraciones requeridas: " + iteraccion.ToString());
        }
    }*/
}
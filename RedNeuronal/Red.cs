using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RedNeuronal
{
    public class Red : MonoBehaviour
    {
        static void Main(string[] args)
        {
            Perceptron perceptron = new Perceptron();
            int numEntradas = 2; //Número de entradas
            int capa0 = 3; //Total neuronas en la capa 0
            int capa1 = 4; //Total neuronas en la capa 1
            int capa2 = 1; //Total neuronas en la capa 2
            perceptron.creaCapas(Random.value, numEntradas, capa0, capa1, capa2);
            //Estas serán las entradas externas al perceptrón
            List<float> entradas = new List<float>();
            entradas.Add(1);
            entradas.Add(0);
            //Se hace el cálculo
            perceptron.calculaSalida(entradas);
        }
    }
    class Perceptron{
        List<Capa> capas;
        //Crea las diversas capas
        public void creaCapas(float azar, int numEntrada, int capa0, int capa1, int capa2)
        {
            capas = new List<Capa>();
            capas.Add(new Capa(capa0, numEntrada)); //Crea la capa 0
            capas.Add(new Capa(capa1, capa0)); //Crea la capa 1
            capas.Add(new Capa(capa2, capa1)); //Crea la capa 2
        }
        public void calculaSalida(List<float> entradas)
        {
            capas[0].CalculaCapa(entradas);
            capas[1].CalculaCapa(capas[0].salidas);
            capas[2].CalculaCapa(capas[1].salidas);
        }
    }
    class Capa
    {
        List<Neurona> neuronas; //Las neuronas que tendrá la capa
        public List<float> salidas; //Almacena las salidas de cada neurona
        public Capa(int totalNeuronas, int totalEntradas)
        {
            neuronas = new List<Neurona>();
            salidas = new List<float>();
            //Genera las neuronas e inicializa sus salidas
            for (int cont = 0; cont < totalNeuronas; cont++)
            {
                neuronas.Add(new Neurona(totalEntradas));
                salidas.Add(0);
            }
        }
        //Calcula las salidas de cada neurona de la capa
        public void CalculaCapa(List<float> entradas)
        {
            Debug.Log(" ");
            for (int cont = 0; cont < neuronas.Count; cont++)
            {
                salidas[cont] = neuronas[cont].calculaSalida(entradas);
            }
        }
    }
    class Neurona
    {
        private List<float> pesos; //Los pesos para cada entrada
        float umbral; //El peso del umbral
                      //Inicializa los pesos y umbral con un valor al azar
        public Neurona(int totalEntradas)
        {
            pesos = new List<float>();
            for (int cont = 0; cont < totalEntradas; cont++)
            {
                pesos.Add(Random.value);
            }
            umbral = Random.value;
        }
        //Calcula la salida de la neurona dependiendo de las entradas
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
    }
}
namespace RedNeuronal_Sam
{
    #region Red_Unity
    public class Mains : MonoBehaviour
    {
        private void Start()
        {
            Perceptron perceptron = new Perceptron();
            int numEntradas = 3; //Número de entradas
            int capa0 = 3; //Total neuronas en la capa 0
            int capa1 = 4; //Total neuronas en la capa 1
            int capa2 = 1; //Total neuronas en la capa 2
            perceptron.creaCapas(Random.value, numEntradas, capa0, capa1, capa2);
            //Estas serán las entradas externas al perceptrón
            List<float> entradas = new List<float>();
            entradas.Add(transform.position.x);
            entradas.Add(transform.position.y);
            entradas.Add(transform.position.z);
            //Se hace el cálculo
            perceptron.calculaSalida(entradas);
        }
    }
    class Perceptron
    {
        List<Capa> capas;
        public void creaCapas(float azar, int numEntrada, int capa0, int capa1, int capa2)
        {
            capas = new List<Capa>();
            Debug.Log("Genera Capa 0");
            capas.Add(new Capa(azar, capa0, numEntrada)); //Crea la capa 0
            Debug.Log("Genera Capa 1");
            capas.Add(new Capa(azar, capa1, capa0)); //Crea la capa 1
            Debug.Log("Genera Capa 2");
            capas.Add(new Capa(azar, capa2, capa1)); //Crea la capa 2
        }
        public void calculaSalida(List<float> entradas)
        {
            Debug.Log("\nCálculos capa 0");
            capas[0].CalculaCapa(entradas);
            Debug.Log("\nCálculos capa 1");
            capas[1].CalculaCapa(capas[0].salidas);
            Debug.Log("\nCálculos capa 2");
            capas[2].CalculaCapa(capas[1].salidas);
        }
    }
    class Capa
    {
        List<Neurona> neuronas;
        public List<float> salidas;
        public Capa(float azar, int NNeuronas, int NEntradas)
        {
            neuronas = new List<Neurona>();
            salidas = new List<float>();
            for (int s = 0; s < NNeuronas; s++)
                neuronas.Add(new Neurona(azar, NEntradas));
            salidas.Add(0);
        }
        public void CalculaCapa(List<float> entradas)
        {
            for(int s = 0; s < neuronas.Count; s++)
            {
                salidas[s] = neuronas[s].SumaRetograda(entradas);
            }
        }
    }
    class Neurona
    {
        private List<float> pesos;
        float umbral;
        public Neurona(float azar ,int NEntradas)
        {
            pesos = new List<float>();
            for (int s = 0; s < NEntradas; s++)
                pesos.Add(Random.value);
            umbral = Random.value;
        }

        public float SumaRetograda(List<float> entradas)
        {
            float valor = 0;
            for(int s = 0; s < entradas.Count; s++)
                valor += entradas[s] * pesos[s];
            valor += umbral;
            return 1 / (1 + Mathf.Exp(-valor)); //De vuelve elevado el Mathf.Exp
        }
    }
    #endregion
}
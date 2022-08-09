using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime);
        Destroy(this.gameObject, 5f);
    }
    private void OnTriggerEnter(Collider ot)
    {
        if(ot.gameObject.tag == "Player")
        {
            Debug.Log("Toque al player");
            ot.gameObject.GetComponent<PlayerController>().Muerte();
        }
    }
}
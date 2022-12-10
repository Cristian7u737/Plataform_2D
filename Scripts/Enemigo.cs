using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    public Jugador Rex;
    public Rigidbody2D meteorito;
    public float velocidad = 3.0f;
    // Start is called before the first frame update
    void Start()
    {
        meteorito = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        meteorito.velocity =  new Vector2(-velocidad, meteorito.velocity.y);
    }
    private void OnTriggerEnter2D(Collider2D c1){
        
        if(c1.tag == "limite"){
            velocidad*=-1;
            Vector3 escala=transform.localScale;
            escala.x*=-1;
            transform.localScale = escala;
        }
        if(c1.tag == "patas"){
            Destroy(gameObject);
        }
    }
}

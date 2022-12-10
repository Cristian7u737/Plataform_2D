using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuerpoJugador : MonoBehaviour
{
    public Jugador Rex;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D c1){
        if(c1.tag == "moneda"){
            Rex.puntaje++;
            Rex.audioPierna.Play();
        }
        if(c1.tag == "vida"){
            Rex.vidas++;
            Rex.audioPierna.Play();
        }
        if(c1.tag == "enemigo"){
            Rex.miAnimacion.SetBool( "Muerte", true );
            Rex.audioMorir.Play();
            Rex.Muerte=true;
            /* Rex.musicaFondo.Stop(); */
            Rex.vidas--;
            if(Rex.vidas==0){
                Rex.Muerte= true;
                Rex.miAnimacion.SetBool("Muerte", false);
            }
            Rex.gameObject.transform.position = Rex.PuntoInicio.transform.position;
        }
        if(c1.tag == "banderaNivel"){
            Rex.FinNivel = true;
        }
    }
}

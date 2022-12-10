using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jugador : MonoBehaviour
{

    public float fuerzaSalto = 8.2f;
    public Rigidbody2D Rex;
    public LayerMask Plataforma;
    public float velocidad = 2.5f;
    public SpriteRenderer srenderer;
    public Animator miAnimacion;
    public bool EnPiso = false;
    public AudioSource audioSaltar;
    public AudioSource audioMorir;
    public AudioSource audioFondo;
    public AudioSource audioPierna;
    public bool Muerte = false;
    public int puntaje = 0;
    public int vidas = 3;
    public GameObject PuntoInicio;
    public bool FinNivel =false;

    void Awake(){

        Rex = GetComponent<Rigidbody2D>();
        srenderer = GetComponent<SpriteRenderer>();
        miAnimacion = GetComponent<Animator>();

    }

    // Start is called before the first frame update
    void Start()
    {

        velocidad = 0;
        miAnimacion.SetFloat( "Velocidad", velocidad );
        
    }

    // Update is called once per frame
    void Update()
    {
        
        fuerzaSalto = 0;
        miAnimacion.SetFloat( "FuerzaSalto", fuerzaSalto );
        velocidad = 0;
        miAnimacion.SetFloat( "Velocidad", 0 );
        miAnimacion.SetBool( "Muerte", false);

        if(Muerte){

        } else {

            if( Input.GetKeyDown("space") && EnPiso ){
                Saltar();
            }

            if( Input.GetKey("right") || Input.GetKey("d") ){
                WalkRight();
            }

            if( Input.GetKey("left") || Input.GetKey("a")  ){
                WalkLeft();
            }
        }


    }

    void Saltar(){
        audioSaltar.Play();
        fuerzaSalto= 6.2f;
        miAnimacion.SetFloat( "FuerzaSalto", fuerzaSalto );
        Rex.AddForce( Vector2.up * fuerzaSalto, ForceMode2D.Impulse );

    }

    void WalkRight(){
        velocidad= 2.5f;
        miAnimacion.SetFloat( "Velocidad", velocidad );
        float movimientoH = Input.GetAxisRaw("Horizontal");
        Rex.transform.localRotation=Quaternion.Euler( 0 , 0, 0 );
        Rex.velocity = new Vector2( movimientoH * velocidad, Rex.velocity.y );
    }

    void WalkLeft(){
        velocidad= 2.5f;
        miAnimacion.SetFloat( "Velocidad", velocidad );
        float movimientoH = Input.GetAxisRaw("Horizontal");
        Rex.transform.localRotation=Quaternion.Euler( 0 , 180, 0 );
        Rex.velocity = new Vector2( movimientoH * velocidad, Rex.velocity.y );
    }

}
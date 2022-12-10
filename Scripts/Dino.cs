using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rex : MonoBehaviour
{
    public float fuerzaSalto = 8.5f;
    public Rigidbody2D dino;
    public LayerMask plataforma; //definio un layer
    public float velocidad = 5.0f;
    public SpriteRenderer sRenderer; // definir un spriterenderer para cambiar la vista
    public Animator Animacion;
    public bool EnPiso = false;
    public AudioSource audioSaltar;
    public AudioSource audioMorir;
    public bool Muerte = false;
    public AudioSource audioFondo;
    public AudioSource audioPierna;
    // Start is called before the first frame update
    void Awake(){
        dino = GetComponent<Rigidbody2D>(); // asignar valores
        sRenderer = GetComponent<SpriteRenderer>();
        Animacion = GetComponent<Animator>();
    }

    void Start(){
        velocidad = 0;
        Animacion.SetFloat("velocidad", velocidad); // inicializar la animación con una velocidad del animator y la del personaje
    }

    // Update is called once per frame
    void Update(){
        /* fuerzaSalto = 0; */
        velocidad = 0;
        Animacion.SetFloat("fuerzaSalto", fuerzaSalto);
        Animacion.SetFloat("velocidad", velocidad); // para que deje de asignarse la fuerza de salto 
        /* if(Input.GetMouseButtonDown(0) && EnPiso){
           Saltar();
        } */
        if(Muerte){

        }else{

            if(Input.GetKeyDown("space") && EnPiso){
            Saltar();
            }

            if(Input.GetKey("right")){
                CaminarDer();
            }

            if(Input.GetKey("left")){
                CaminarIzq();
            }
        }
    }
    //metodo para saltar
    void Saltar(){
        audioSaltar.Play();
        Animacion.SetFloat("fuerzaSalto", fuerzaSalto); //inicializar la animación de saltar con una fuerza de salto del animator y la del personaje
        dino.AddForce(Vector2.up * fuerzaSalto, ForceMode2D.Impulse); // agrega una fuerza de salto 
    }

    /* bool EstaSuelo(){ //metodo para dectectar si esta en el suelo y delimita el salto infinito
        if(Physics2D.Raycast(this.transform.position, Vector2.down, 1.8f, plataforma.value)){
            return true;
        }else{
            return false;
        }
    } */
    void CaminarIzq(){ // metodo para caminar hacia la izquierda
        velocidad = 2.5f;
        Animacion.SetFloat("velocidad", velocidad);
        float movimientoL = Input.GetAxisRaw("Horizontal");
        dino.transform.localRotation = Quaternion.Euler(0,180,0);
        dino.velocity = new Vector2(movimientoL * velocidad, dino.velocity.x);
    }

    void CaminarDer(){ // metodo para caminar hacia la derecha
        velocidad = 2.5f;
        Animacion.SetFloat("velocidad", velocidad);
        float movimientoH = Input.GetAxisRaw("Horizontal");
        dino.transform.localRotation = Quaternion.Euler(0,0,0);
        dino.velocity = new Vector2(movimientoH * velocidad, dino.velocity.y);
    }
}


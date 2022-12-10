using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Net.Http;
using UnityEngine.Networking;
using Newtonsoft.Json;

public class ControlNiveles : MonoBehaviour
{
    public Jugador Rex;
    public GameObject[] Niveles; //se almacenan los niveles
    private int indiceNivel;
    private GameObject NivelActual; //almacena el nivel actual
    public Text TextoPuntos;
    public Text TextoVidas;
    public Text TextoNivel;
    public Text TextoGameOver;
    public Text textotitulo;
    public Text TextDatos;
    public InputField txtid;
    public Button btnenviar;
    public Image image;
    public Text TextGame;
    // Start is called before the first frame update
    void Start()
    {
        NivelActual = Instantiate(Niveles[indiceNivel]);
        NivelActual.transform.SetParent(this.transform);
        if (btnenviar){
            btnenviar.GetComponent<Button>().onClick.AddListener(
                () =>{StartCoroutine(getRequest("http://localhost:5000/player/" + txtid.text));
                btnenviar.enabled = false;
                btnenviar.gameObject.SetActive(false);
                txtid.enabled=false;
                txtid.gameObject.SetActive(false);
                }
            );
        }else{
            Debug.Log("Error");
        }
        /* StartCoroutine(getRequest("https://jsonplaceholder.typicode.com/todos/1")); */
    }
    IEnumerator getRequest( string uri ){
        UnityWebRequest uwr = UnityWebRequest.Get( uri );
        yield return uwr.SendWebRequest();
        if ( uwr.isNetworkError ){
            Debug.Log( "Error de conexión: " + uwr.error );
        } else {
            Player player = JsonUtility.FromJson<Player>( uwr.downloadHandler.text );
            TextGame.text = "Nombre: " + player.name + "\n Correo: " + player.email + "\n Usuario: " + player.nickname;
            UnityWebRequest request = UnityWebRequestTexture.GetTexture( player.avatar_url );
            yield return request.SendWebRequest();
       

        if( request.isNetworkError || request.isHttpError ) {
            Debug.Log(request.error );
        } else {
            Texture2D myTexture = ( ( DownloadHandlerTexture ) request.downloadHandler ).texture;
            Sprite newSprite = Sprite.Create( myTexture, new Rect( 0, 0, myTexture.width, myTexture.height ), new Vector2( 0.5f, 0.5f ) );
            image.sprite = newSprite;
        }
    } }

    IEnumerator postRequest(string uri, string bodyjsonString){
        var request = new UnityWebRequest(uri, "POST");
        byte[] bodyRaw =  System.Text.Encoding.UTF8.GetBytes(bodyjsonString);
        request.uploadHandler = (UploadHandler) new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        yield return request.SendWebRequest();
        // Debug.Log("Status Code : " + request.responseCode);
    }

    IEnumerator putRequest(string uri, string bodyjsonString){
        var request = new UnityWebRequest(uri, "PUT");
        byte[] bodyRaw =  System.Text.Encoding.UTF8.GetBytes(bodyjsonString);
        request.uploadHandler = (UploadHandler) new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        yield return request.SendWebRequest();
        // Debug.Log("Status Code : " + request.responseCode);
    }   

    [ System.Serializable ]
    public class Game {
        public int score;
        public int coins;
    }
    [ System.Serializable ]
    public class Player {
        public string  email;
        public string  password;
        public string  name;
        public string  birthdate;
        public string  nickname;
        public string  avatar_url;
    }
    

    // Update is called once per frame
    void Update()
    {
        Game objeto = new Game();
        objeto.score = Rex.vidas;
        objeto.coins = Rex.puntaje;
            /* objeto.levels = indiceNivel+1; */
        string jsonString = JsonUtility.ToJson(objeto);

        StartCoroutine(putRequest("http://localhost:5000/game/update/"+txtid.text, jsonString));
        /* TextGame.text = "Estadisticas del Game : " + "\n Nivel : "+ (indiceNivel+1) + "\n Mondedas : " + Rex.puntaje + "\n Vidas : " + Rex.vidas; */
        TextGame.text="Nivel: "+(indiceNivel+1)+"\nPuntaje: "+Rex.puntaje;
        TextoNivel.text="Nivel : "+(indiceNivel+1);
        TextoPuntos.text="Monedas : "+Rex.puntaje;
        TextoVidas.text="Vidas : "+Rex.vidas;
        if(Rex.Muerte){
            if(Rex.vidas==0){          
            TextoGameOver.text="GameOver \n Para reiniciar precione R";
            if(Input.GetKeyDown("r")){
                Rex.puntaje=0;
                Rex.vidas=0;
                TextoGameOver.text="";
                Rex.gameObject.transform.position=Rex.PuntoInicio.transform.position;
                Destroy(NivelActual);
                NivelActual=Instantiate(Niveles[indiceNivel]);
                NivelActual.transform.SetParent(this.transform);
                Rex.Muerte=false;
                Rex.miAnimacion.SetBool( "Muerte", false);
            }
             }else{
                Rex.gameObject.transform.position=Rex.PuntoInicio.transform.position;
                Destroy(NivelActual);
                NivelActual=Instantiate(Niveles[indiceNivel]);
                NivelActual.transform.SetParent(this.transform);
                Rex.Muerte=false;
             }

        }if ((Rex.FinNivel && Rex.puntaje>1)){
            TextoGameOver.text = "Ganaste Nivel Completado \n Pulsa R para pasar al siguiente nivel";
            if(Input.GetKeyDown("r")){
                /* Rex.audioFondo.Start(); */
                TextoGameOver.text = "";
                Rex.puntaje=0;
                Rex.gameObject.transform.position = Rex.PuntoInicio.transform.position;
                Destroy(NivelActual);
                indiceNivel++;
                NivelActual = Instantiate(Niveles[indiceNivel]);
                NivelActual.transform.SetParent(this.transform);
                Rex.FinNivel=false;
                
            }else if(indiceNivel == Niveles.Length-1){
                TextoGameOver.text = "Juego terminado \n pulsa R para iniciar nuevamente";
                if(Input.GetKeyDown("r")){
                    Rex.puntaje=0;
                    Rex.gameObject.transform.position = Rex.PuntoInicio.transform.position;
                    Destroy(NivelActual);
                    indiceNivel = 0;
                    NivelActual = Instantiate(Niveles[indiceNivel]);
                    NivelActual.transform.SetParent(this.transform);
                    Rex.FinNivel=false;
                }
            }
        }/* else if(Rex.puntaje<1){
                TextoGameOver.text = "";
            } */

    
    }
    
}


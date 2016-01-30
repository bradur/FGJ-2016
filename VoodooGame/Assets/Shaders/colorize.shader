 using UnityEngine;
 using System.Collections;
 
 public class myClass : MonoBehaviour {
     private SpriteRenderer myRenderer;
     private Shader shaderGUItext;
     private Shader shaderSpritesDefault;
 
     void Start () {
         myRenderer = gameObject.GetComponent<SpriteRenderer>();
         shaderGUItext = Shader.Find("GUI/Text Shader");
         shaderSpritesDefault = Shader.Find("Sprites/Default"); // or whatever sprite shader is being used
 
     }
 }
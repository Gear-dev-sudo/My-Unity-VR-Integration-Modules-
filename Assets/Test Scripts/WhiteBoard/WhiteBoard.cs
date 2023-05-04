using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace my_unity_integration
{
    public class WhiteBoard : MonoBehaviour
    {
        public Texture2D texture;
        public Vector2 textureSize = new Vector2(2048, 2048);




        // Start is called before the first frame update
        void Start()
        {
            var r = GetComponent<Renderer>();
            texture = new Texture2D((int)textureSize.x, (int)textureSize.y);
            
            r.material.mainTexture = texture;
            r.material.SetColor("_Color", Color.white);
          
        }


    }
}

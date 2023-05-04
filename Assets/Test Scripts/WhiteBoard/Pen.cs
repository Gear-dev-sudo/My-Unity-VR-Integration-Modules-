using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace my_unity_integration
{
    public class Pen : MonoBehaviour
    {
        public Transform tip;
        public int penSize = 10;
        private Renderer penRenderer;
        private Color[] colors;
        private float tipHeight;
        public WhiteBoard whiteBoard;

   
        private RaycastHit touch;
        private Vector2 touchPos;
        private bool touchedLastFrame=false;
        private Vector2 lastTouchPos;
        private Quaternion lastTouchRot;

        // Start is called before the first frame update
        void Start()
        {
            penRenderer = tip.GetComponent<Renderer>();
            colors = Enumerable.Repeat(penRenderer.material.color, penSize * penSize).ToArray();
            tipHeight = tip.localScale.x;

        }

        // Update is called once per frame
        void Update()
        {
            Draw();
        }

        private void Draw()
        {

            
           if(Physics.Raycast(tip.position,transform.right,out touch,tipHeight))
            {
                //Debug.LogWarning(touch.transform.gameObject.name);

                if (touch.transform.CompareTag("WhiteBoard"))
                {
                   // Debug.LogWarning("HIT WHITE");
                    if(whiteBoard==null)
                    {
                        whiteBoard = touch.transform.GetComponent<WhiteBoard>();

                    }

                    touchPos = new Vector2(touch.textureCoord.x, touch.textureCoord.y);
                    var x = (int)(touchPos.x * whiteBoard.textureSize.x - (penSize /2));
                    var y = (int)(touchPos.y * whiteBoard.textureSize.y - (penSize /2));
                    if(touchedLastFrame)
                    {
                        whiteBoard.texture.SetPixels(x, y, penSize, penSize, colors);
                        for(float f=0.01f; f<1.00f;f+=0.03f)
                        {
                            var lerpX = (int)Mathf.Lerp(lastTouchPos.x, x, f);
                            var lerpY = (int)Mathf.Lerp(lastTouchPos.y, y, f);
                            whiteBoard.texture.SetPixels(lerpX, lerpY, penSize, penSize,colors);

                        }

                        transform.rotation = lastTouchRot;

                        whiteBoard.texture.Apply();


                    }

                    lastTouchPos = new Vector2(x,y);
                    lastTouchRot = transform.rotation;
                    touchedLastFrame = true;
                    return;

                }
            }

            
            touchedLastFrame = false;
            whiteBoard = null;
        }
    }
}
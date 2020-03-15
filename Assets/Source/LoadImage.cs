using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadImage : MonoBehaviour
{
    public static LoadImage Instance;


    public Texture2D texture2;
 
 
    void Start()
    {
        Instance = this;

        Texture2D ro = RotateImage (texture2 , 90);

        gameObject.GetComponent<SpriteRenderer>().sprite = Sprite.Create (ro , 
                                                                            gameObject.GetComponent<SpriteRenderer>().sprite.rect , 
                                                                            new Vector2 (0.5f,0.5f)); 
    }

    public Texture2D RotateImage(Texture2D originTexture, int angle)
    {
         Texture2D textureCopy;

         textureCopy = new Texture2D(originTexture.width, originTexture.height);

         Color32[] pix1 = textureCopy.GetPixels32(); //rgba32

         Color32[] pix2 = originTexture.GetPixels32();

         int originalWidth = originTexture.width;

         int originalHeight = originTexture.height;

         int x = 0;

         int y = 0;

         Color32[] pix3 = rotateSquare(pix2, (Math.PI/180*(double)angle), originTexture);

         for (int j = 0; j < originalHeight; j++)
         {
             for (var i = 0; i < originalWidth; i++)
            {
                 pix1[textureCopy.width/2 - 
                        originalWidth/2 + x + i + 
                        textureCopy.width*(textureCopy.height/2-originalHeight/2+j+y)] 
                                                            = pix3[i + j*originalWidth];
            }
         }

         textureCopy.SetPixels32(pix1);

         textureCopy.Apply();

         return textureCopy;
     }
     public Color32[] rotateSquare(Color32[] arr, double radian, Texture2D originTexture)
     {
         int x;

         int y;

         int i;
         
         int j;

         double sine = Math.Sin(radian);

         double cose = Math.Cos(radian);

         Color32[] rgba32 = originTexture.GetPixels32();

         int originalWidth = originTexture.width;

         int originalHeight = originTexture.height;

         int xc = originalWidth/2;

         int yc = originalHeight/2;

         for (j = 0; j < originalHeight ; j++)
         {
             for (i = 0; i < originalWidth ; i++)
             {
                 rgba32[j * originalWidth + i] = new Color32(0,0,0,0);

                 x = (int)(cose *(i - xc) + sine * (j - yc) +xc);

                 y = (int)(-sine * (i - xc) + cose * (j - yc) + yc);

                 if ((x > -1) && ( x < originalWidth) && (y > -1) && ( y < originalHeight))
                 { 
                     rgba32[j * originalWidth + i] = arr[y * originalWidth + x];
                 }
             }
         }

         return rgba32;
     }
 }

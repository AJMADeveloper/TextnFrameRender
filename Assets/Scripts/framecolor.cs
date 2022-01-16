using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking ;
using Color=UnityEngine.Color;

public class framecolor: MonoBehaviour  {
   public RawImage img;
   public Color color1;
   RawImage rt;
   string jsonURL = "http://lab.greedygame.com/arpit-dev/unity-assignment/templates/frame_color.json" ;
   
    void Update()
   {
       
      StartCoroutine (Getcolor (jsonURL));
   }

   IEnumerator Getcolor (string url) {
     
     UnityWebRequest request = UnityWebRequest.Get (url) ;

      yield return request.SendWebRequest() ;

      if (request.isNetworkError || request.isHttpError) {
         // error ...

      } else {
         
         jsonText data = JsonUtility.FromJson<jsonText> (request.downloadHandler.text) ;
        
         foreach(tLayer l in data.layers)
         {
            foreach(color cl in l.operations) 
            {
               if(ColorUtility.TryParseHtmlString(cl.argument,out color1))
               {
                   color1.a=1;
                   rt=img.GetComponent<RawImage>();
                  rt.color = color1 ;
               }
            }
         }
            
         }
      request.Dispose () ;
   }
}

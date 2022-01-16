using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking ;
using Color=UnityEngine.Color;

public class textcolor: MonoBehaviour  {
   public Text mytxt;
   public Color color1;
   string jsonURL = "http://lab.greedygame.com/arpit-dev/unity-assignment/templates/text_color.json" ;
   
    void Update()
   {
      StartCoroutine (Gettxtcolor (jsonURL));
   }

   IEnumerator Gettxtcolor (string url) {
     
     UnityWebRequest request = UnityWebRequest.Get (url) ;

      yield return request.SendWebRequest() ;

      if (request.isNetworkError || request.isHttpError) {
         

      } else {
         
         jsonText data = JsonUtility.FromJson<jsonText> (request.downloadHandler.text) ;
        
         foreach(tLayer l in data.layers)
         {
            foreach(color cl in l.operations) 
            {
               if(ColorUtility.TryParseHtmlString(cl.argument,out color1))
               {
                  mytxt.color = color1;
               }
            }
         }
            
         }
      request.Dispose () ;
   }
}

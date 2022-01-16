using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking ;

public class Frameload : MonoBehaviour {
   [SerializeField] RawImage uiImage ;
   
   public RawImage img;
   private float r,u,h,w;
   RectTransform rt;

   string jsonURL = "http://lab.greedygame.com/arpit-dev/unity-assignment/templates/frame_only.json" ;
   
   public void Start () {
      
      StartCoroutine (GetImg (jsonURL)) ;
      
   }

IEnumerator GetImg (string url) {
      UnityWebRequest request = UnityWebRequest.Get (url) ;

      yield return request.SendWebRequest() ;

      if (request.isNetworkError || request.isHttpError) {
         // error ...

      } else {
         
         jsonText data = JsonUtility.FromJson<jsonText> (request.downloadHandler.text) ;
         
         
         foreach(tLayer l in data.layers)
         {
             StartCoroutine (GetImgURL(l.path)) ;
             
            foreach(place po in l.placement)
            {
               r=po.position.x;
               u=po.position.y;
               h=po.position.height;
               w=po.position.width;

               rt=img.GetComponent<RectTransform>();
               rt.anchoredPosition=new Vector2(r,u);
               if(h>0 && w>0)
               {
                  rt.sizeDelta= new Vector2(w,h);
               }
            }
         }
      }
      
      request.Dispose () ;
   }

   IEnumerator GetImgURL (string url) {
      UnityWebRequest request = UnityWebRequestTexture.GetTexture (url) ;

      yield return request.SendWebRequest() ;

      if (request.isNetworkError || request.isHttpError) {
         

      } else {

         uiImage.texture = ((DownloadHandlerTexture)request.downloadHandler).texture ;
      }

      request.Dispose () ;
   }


 

}
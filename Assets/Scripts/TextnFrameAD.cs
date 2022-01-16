using System.Drawing;
using System;
using UnityEngine ;
using UnityEngine.Networking ;
using UnityEngine.UI ;
using System.Collections.Generic ;
using System.Collections ;

[Serializable]
public class jsonText {
   public List<tLayer> layers ;
   
}

[Serializable]
public class tLayer{
   public string type;
   public string path;
   public List<color> operations;
   public List<place> placement;
}

[Serializable]
public class color{
   public string name;
   public string argument;
}

[Serializable]
public class place{
   public Position position;

}

[Serializable]
public class Position
{
    public float x;
    public float y;
    public float width;
    public float height;

}


public class TextnFrameAD : MonoBehaviour {
   [SerializeField] Text uiNameText ;
   
   public Text mytxt;
   private float r,u,h,w;
   RectTransform rt;

   string jsonURL = "http://lab.greedygame.com/arpit-dev/unity-assignment/templates/text_only.json" ;
   
   public void Start () {
      
      StartCoroutine (GetText(jsonURL)) ;
      
   }

IEnumerator GetText (string url) {
      UnityWebRequest request = UnityWebRequest.Get (url) ;

      yield return request.SendWebRequest() ;

      if (request.isNetworkError || request.isHttpError) {
        

      } else {
         
         jsonText data = JsonUtility.FromJson<jsonText> (request.downloadHandler.text) ;
         
         foreach(tLayer l in data.layers)
         {
            uiNameText.text="";

            foreach(place po in l.placement)
            {
               r=po.position.x;
               u=po.position.y;
               h=po.position.height;
               w=po.position.width;

               rt=mytxt.GetComponent<RectTransform>();
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

 

}
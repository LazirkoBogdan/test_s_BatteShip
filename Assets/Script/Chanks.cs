using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Chanks : MonoBehaviour {
  
    public Sprite[] imgs;
    public int Index = 0;
    public bool HideChank = false;

    void ChangeImgs()
    {
        if(imgs.Length>Index)
        {
            if((HideChank)&&(Index==1)) GetComponent<SpriteRenderer>().sprite = imgs[0];
            else
            GetComponent<SpriteRenderer>().sprite = imgs[Index];
        }
    }

	void Start () {
        ChangeImgs();
    }
	
	void Update () {
        ChangeImgs();
	}
}

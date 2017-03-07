using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickField : MonoBehaviour {

    public GameObject Parent = null;

    public int CoorDX, CoorDY;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnMouseDown()
    {
        if(Parent != null)
        {
            Parent.GetComponent<GameFiedl>().WhoClick(CoorDX, CoorDY);
        }
    }
}

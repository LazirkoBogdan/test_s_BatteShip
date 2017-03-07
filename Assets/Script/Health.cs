using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

    public GameObject GameField, HealthChank;

    GameObject[] HealthBar = new GameObject[20];
    void CreateHealthBar()
    {
        Vector3 GetPositionOnScreen = this.transform.position;
        float Dx = 0.5f;

        for (int I = 0; I< 20; I++ )
        {
            HealthBar[I] = Instantiate(HealthChank) as GameObject;
            HealthBar[I].transform.position = GetPositionOnScreen;
            GetPositionOnScreen.x += Dx;


        }
    }

    void RefreshHealth()

    {
        int L = 0;
        for (int I = 0; I < 20; I++) HealthBar[I].GetComponent<Chanks>().Index = 0;

        if (GameField != null) L = GameField.GetComponent<GameFiedl>().LifeShip();
        for (int I = 0; I < L; I++) HealthBar[I].GetComponent<Chanks>().Index = 1;

    }
// Use this for initialization
void Start () {

        if(HealthChank!=null)
        CreateHealthBar();


    }
	
	// Update is called once per frame
	void Update () {
        if((GameField != null) && (HealthChank != null))
        {
            RefreshHealth();
        }
		
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGame : MonoBehaviour {
    public int GameMode = 0;
    bool whoMove = true;
    public GameObject PlayerField, ComputerField, Player;

    private void OnGUI()
    {
        float CentreScreenX = Screen.width / 2;
        float CentreScreenY = Screen.height / 2;
        GameFiedl PlayerFieldController = PlayerField.GetComponent<GameFiedl>();
        Rect LocationButton;
        Camera cam = GetComponent<Camera>();
        switch (GameMode)
        {
            case 0:
                
                cam.orthographicSize = 8;
                this.transform.position = new Vector3(0, 0, -10);
                LocationButton = new Rect(new Vector2(CentreScreenX - 150, CentreScreenY - 50), new Vector2(300, 160));

                GUI.Box(LocationButton, "");
                LocationButton = new Rect(new Vector2(CentreScreenX - 40, CentreScreenY -40), new Vector2(300, 30));
                GUI.Label(LocationButton, "Main Menu");

                LocationButton = new Rect(new Vector2(CentreScreenX - 100, CentreScreenY ), new Vector2(200, 30));
                if (GUI.Button(LocationButton, "Start"))
                    GameMode = 1;

                LocationButton = new Rect(new Vector2(CentreScreenX - 100, CentreScreenY+40), new Vector2(200, 30));
                if (GUI.Button(LocationButton, "Exit"))
                    Application.Quit();

                break;
            case 1:
                this.transform.position = new Vector3(30, 0, -20);
                cam.orthographicSize = 8;
                LocationButton = new Rect(new Vector2(CentreScreenX - 150, 0), new Vector2(300, 180));
                GUI.Box(LocationButton, "");

                LocationButton = new Rect(new Vector2(CentreScreenX - 10, 10), new Vector2(300, 30));
                GUI.Label(LocationButton, "Hangar ");

                LocationButton = new Rect(new Vector2(CentreScreenX - 100, 50), new Vector2(200, 30));
                if (GUI.Button(LocationButton, "Main Menu"))
                {
                    PlayerFieldController.ClearField();
                    GameMode = 0;
                }

                LocationButton = new Rect(new Vector2(CentreScreenX - 100, 90), new Vector2(200, 30));
                if (GUI.Button(LocationButton, "Generate NAVY"))
                {
                    PlayerFieldController.EnterRandomShip();

                }

                if (PlayerFieldController.LifeShip() == 20)
                {
                    LocationButton = new Rect(new Vector2(CentreScreenX - 100, 130), new Vector2(200, 30));
                    if (GUI.Button(LocationButton, "Start Game"))
                    {
                        GameMode = 3;
                        PlayerField.GetComponent<GameFiedl>().CopyField();
                        ComputerField.GetComponent<GameFiedl>().EnterRandomShip();
                    }
                }
                break;
            case 2:

                break;
            case 3:

                this.transform.position = new Vector3(30, -30, -10);
                cam.orthographicSize = 14;


                break;
            case 4:

                this.transform.position = new Vector3(60, 0, -10);
                cam.orthographicSize = 8;

                LocationButton = new Rect(new Vector2(CentreScreenX - 150, 0), new Vector2(300, 180));
                GUI.Box(LocationButton, "");

                LocationButton = new Rect(new Vector2(CentreScreenX - 10, 10), new Vector2(300, 30));
                GUI.Label(LocationButton, "Win ");

                LocationButton = new Rect(new Vector2(CentreScreenX - 100, 50), new Vector2(200, 30));
                if (GUI.Button(LocationButton, "Main Menu"))
                {
                    PlayerFieldController.ClearField();
                    GameMode = 0;
                }

                break;

            case 5:

                this.transform.position = new Vector3(100, 0, -10);
                cam.orthographicSize = 8;

                LocationButton = new Rect(new Vector2(CentreScreenX - 150, 0), new Vector2(300, 180));
                GUI.Box(LocationButton, "");

                LocationButton = new Rect(new Vector2(CentreScreenX - 10, 10), new Vector2(300, 30));
                GUI.Label(LocationButton, "Win ");

                LocationButton = new Rect(new Vector2(CentreScreenX - 100, 50), new Vector2(200, 30));
                if (GUI.Button(LocationButton, "Main Menu"))
                {
                    PlayerFieldController.ClearField();
                    GameMode = 0;
                }

                break;
        }
    }

    void  TestWhoWin()
    {
        int PC_SHIP = ComputerField.GetComponent<GameFiedl>().LifeShip();
        int Player_SHIP = Player.GetComponent<GameFiedl>().LifeShip();

        if (PC_SHIP == 0) 
        {
            GameMode = 4;
        }

        if (Player_SHIP == 0)
        {
            GameMode = 5;
        }


    }
    public void UserClick(int X , int Y)
    {
        if(whoMove)
        {
            whoMove = ComputerField.GetComponent<GameFiedl>().Shoot(X, Y);
        }
        Debug.Log("Click");
    }
    void AI()
    {
        if(!whoMove)
        {
            TestWhoWin();
            int ShootX = Random.RandomRange(0, 9);
            int ShootY = Random.RandomRange(0, 9);

            whoMove = !Player.GetComponent<GameFiedl>().Shoot(ShootX, ShootY);


        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
        if(GameMode == 3)
        {
            AI();
        }
	}
}

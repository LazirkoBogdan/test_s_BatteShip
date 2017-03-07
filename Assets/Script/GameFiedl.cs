using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

public class GameFiedl : MonoBehaviour {

    public GameObject GameMain;
    public GameObject eLiters, eNumbers, eBlock, eState;
    public GameObject[,] Block;
    public GameObject MapDes;
    public bool IsLan = false;
    public bool HideShip = false;

    GameObject[] Liters;
    GameObject[] Numbs;

    int LenghtField = 10;
    int time = 80, DeltaTime = 0;
    public int[] ShipCount = { 0, 4, 3, 2, 1 };
    


    bool CountShips()
    {
        int Amaunt = 0;
        foreach (int Ship in ShipCount) Amaunt += Ship;
        if (Amaunt != 0) return true;
        
        return false;
    }

    public void ClearField()
    {
        ShipCount =  new int[]{ 0, 4, 3, 2, 1 };
        ListShip.Clear();

        for (int Y = 0; Y < LenghtField; Y++)
        {

            for (int X = 0; X < LenghtField; X++)
            {
                Block[X, Y].GetComponent<Chanks>().Index = 0;
            }
        }
    }

    public void EnterRandomShip()
    {
        ClearField();
        int SelectShip = 4;
        int X, Y;
        int Direction;
        int MinRandomValue = 0;
        int MaxRandomValue = 10;

        while (CountShips())
        {
            X =  Random.RandomRange(MinRandomValue, MaxRandomValue);
            Y =  Random.RandomRange(MinRandomValue, MaxRandomValue);
            Direction = Random.RandomRange(0, 2);

            if (EnterSDeck(SelectShip, Direction, X, Y))
            {
                ShipCount[SelectShip]--;
                if (ShipCount[SelectShip] == 0)
                {
                    SelectShip--;
                }
            }
        }
    }


    public struct TestCoord
    {
        public int X, Y;
    }
    public void CopyField()
    {
        if(MapDes !=null)
        {
            for(int Y = 0; Y< LenghtField;Y++)
            {
                for(int X = 0; X< LenghtField;X++)
                {
                    MapDes.GetComponent<GameFiedl>().Block[X, Y].GetComponent<Chanks>().Index = Block[X, Y].GetComponent<Chanks>().Index;
                }
            }

            MapDes.GetComponent<GameFiedl>().ListShip.Clear();
            MapDes.GetComponent<GameFiedl>().ListShip.AddRange(ListShip);


        }
    }

    public struct Ship
    {
        public TestCoord[] ShipCoord;
    }

    List<Ship> ListShip = new List<Ship>();

    void CreateField()
    {
        Vector3 StartPos = transform.position;
        float XX = StartPos.x + 1;
        float YY = StartPos.y - 1;
        Liters = new GameObject[LenghtField];
        Numbs = new GameObject[LenghtField];

        for (int Inscription = 0; Inscription<LenghtField;Inscription++)
        {
            Liters[Inscription] = Instantiate(eLiters);

            Liters[Inscription].transform.position = new Vector3(XX, StartPos.y, StartPos.z);
            Liters[Inscription].GetComponent<Chanks>().Index = Inscription;
            XX++;

            Numbs[Inscription] = Instantiate(eNumbers);
  ;
            Numbs[Inscription].transform.position = new Vector3(StartPos.x, YY, StartPos.z);
            Numbs[Inscription].GetComponent<Chanks>().Index = Inscription;
            YY--;
        }
        XX = StartPos.x + 1;
        YY = StartPos.y - 1;
        Block = new GameObject[LenghtField,LenghtField];


        for (int Y = 0; Y < LenghtField; Y++)
        {
            for(int X = 0; X<LenghtField;X++)
            {
                Block[X, Y] = Instantiate(eBlock);
                if (IsLan)
                { Block[X, Y].transform.parent = GameObject.Find("playerTestPref(Clone)").transform; }

                Block[X, Y].GetComponent<Chanks>().Index = 0;
                Block[X, Y].GetComponent<Chanks>().HideChank = HideShip;
                Block[X, Y].transform.position = new Vector3(XX, YY, StartPos.z);
                if(HideShip)
                Block[X, Y].GetComponent<ClickField>().Parent = this.gameObject;
                Block[X, Y].GetComponent<ClickField>().CoorDX = X;
                Block[X, Y].GetComponent<ClickField>().CoorDY = Y;
                XX++;
            }
            XX = StartPos.x + 1;
            YY--;
        }
    }


    bool  EnteDeck(int X , int Y)
    {
        if ((X > -1) && (Y > -1) && (X < 10) && (Y < 10))
        {
            int[] XX = new int[9], YY = new int[9];

            XX[0] = X + 1; XX[1] = X; XX[2] = X -1;
            YY[0] = Y + 1; YY[1] = Y; YY[2] = Y + 1;

            XX[3] = X + 1; XX[4] = X; XX[5] = X - 1;
            YY[3] = Y; YY[4] = Y; YY[5] = Y;

            XX[6] = X + 1; XX[7] = X; XX[8] = X - 1;
            YY[6] = Y-1; YY[7] = Y-1; YY[8] = Y-1;

            for (int I = 0; I < 9; I++) 
            {
                if ((XX[I] > -1) && (YY[I] > -1) && (XX[I] < 10) && (YY[I] < 10))
                {
                    if (Block[XX[I], YY[I]].GetComponent<Chanks>().Index != 0) return false;
                }

            }
            return true;



        }
        return false;

    }

    TestCoord[] EnterShipDirection(int ShipType,int XD, int YD , int X, int Y)
    {
        TestCoord[] ResultCoord = new TestCoord[ShipType];
        for(int P = 0;P<ShipType; P++)
        {
            if(EnteDeck(X,Y))
            {
                ResultCoord[P].X = X;
                ResultCoord[P].Y = Y;
            }
            else
            {
                return null;
            }
            X += XD;
            Y += YD;
        }
        return ResultCoord;

    }


    TestCoord[] EnterShip(int ShipType, int Direction, int X, int Y)
    {
        TestCoord[] ResultCoord = new TestCoord[ShipType];
        if(EnteDeck(X,Y))
        {
                switch (Direction)
                {
                case 0:
                    ResultCoord = EnterShipDirection(ShipType, 1, 0, X, Y);
                    if(ResultCoord == null)
                    {
                        ResultCoord = EnterShipDirection(ShipType, -1, 0, X, Y);
                    }
                    break;
                case 1:
                    ResultCoord = EnterShipDirection(ShipType, 0, 1, X, Y);
                    if (ResultCoord == null)
                    {
                        ResultCoord = EnterShipDirection(ShipType, 0, -1, X, Y);
                    }
                    break;
            }
            return ResultCoord;
        }
        return null;
    }


    bool EnterSDeck(int ShipType,int Direction,int X,int Y)
    {
        TestCoord[] P = EnterShip(ShipType, Direction, X, Y);

        if(P !=null)
        {
            foreach(TestCoord T in P)
            {
                Block[T.X, T.Y].GetComponent<Chanks>().Index = 1;
            }
            Ship Deck;
            Deck.ShipCoord = P;
            ListShip.Add(Deck);

            return true;
        }
        return false;
    }

    public void WhoClick(int X, int Y)
    {


        if (GameMain != null)
        {
           GameMain.GetComponent<MainGame>().UserClick(X, Y);
        }
    }

  public bool Shoot(int X, int Y)
    {
        eState.GetComponent<Chanks>().Index = 0; 
        int FieldSelect = Block[X, Y].GetComponent<Chanks>().Index;
        bool Result = false;
        switch(FieldSelect)
        {
            case 0:
                Block[X, Y].GetComponent<Chanks>().Index = 2;
                Result = false;
                eState.GetComponent<Chanks>().Index = 3;

                break;
            case 1:
                Block[X, Y].GetComponent<Chanks>().Index = 3;
                Result = true;
                if(TestShoot(X,Y))
                {
                    eState.GetComponent<Chanks>().Index = 1;

                }
                else
                {
                    eState.GetComponent<Chanks>().Index = 2;

                }

                break;
        }
        return Result;

    }

    bool TestShoot(int X, int Y)
    {
        bool Result = false;

        foreach(Ship Test in ListShip)
        {
            foreach(TestCoord FieldPaluba in Test.ShipCoord)
            {

              if((FieldPaluba.X == X) && (FieldPaluba.Y == Y))
                {
                    int CountKill = 0;

                    foreach(TestCoord KillFieldPaluba in Test.ShipCoord)
                    {
                        int TestBlocks = Block[KillFieldPaluba.X , KillFieldPaluba.Y].GetComponent<Chanks>().Index;
                        if (TestBlocks == 3) CountKill++;

                    }

                    if (CountKill == Test.ShipCoord.Length)
                        Result = true;
                    else
                        Result = false;

                    return Result;
                }
            }
        }

        return Result;
    }

    public int LifeShip()
    {
        int CountLife = 0;

        foreach(Ship Test in ListShip)
        {

            foreach(TestCoord FieldPaluba in Test.ShipCoord)
            {
                int TestBlocks = Block[FieldPaluba.X, FieldPaluba.Y].GetComponent<Chanks>().Index;
                if (TestBlocks == 1) CountLife++;
            }

        }
        return CountLife;
    }


    void Start ()
    {
 
        CreateField();
        if (HideShip) EnterRandomShip();


    }
	
	// Update is called once per frame
	void Update ()
    {
        DeltaTime++;

        if (DeltaTime>time)
        {
            if(eState!=null) eState.GetComponent<Chanks>().Index = 0;
            DeltaTime = 0;
        }
	}
}

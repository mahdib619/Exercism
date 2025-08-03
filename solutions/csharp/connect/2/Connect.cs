using System;
public enum ConnectWinner
{
    White,      //Player ID = 1
    Black,      //Player ID = 2
    None        //Player ID = 0
}
//Global Informations 
public static class Game
{
    private static ConnectWinner state;
    private static int sideWinner;
    public static ConnectWinner State { get { return state; } set { state = value; } }
    public static int SideWinner { get { return sideWinner; } set { sideWinner = value; } }
}
//Class that represents a single piace of map
public class Hexagon
{
    private int player;
    private int x;
    private int y;
    private bool check;
    private Hexagon a, b, c, d, e, f;
    public Hexagon Next(ConnectWinner direction)
    {
        if (direction == ConnectWinner.White)
        {
            return c;
        }
        else
        {
            return d;
        }
    }
    public void Set(int hexPlayer, int hexX, int hexY, Hexagon hexA = null, Hexagon hexB = null, Hexagon hexC = null, Hexagon hexD = null, Hexagon hexE = null, Hexagon hexF = null)
    {
        player = hexPlayer;
        x = hexX; y = hexY;
        a = hexA; b = hexB; c = hexC; d = hexD; e = hexE; f = hexF;
        check = false;
    }
    //Checks Connections
    public void CheckHexagon(ConnectWinner direction, Hexagon parent)
    {
        if (parent.player == this.player && player != 0 && check == false)
        {
            check = true;
            if (player == 1 && y == Game.SideWinner && direction == ConnectWinner.White)
            {
                Game.State = ConnectWinner.White;
            }
            else if (player == 2 && x == Game.SideWinner && direction == ConnectWinner.Black)
            {
                Game.State = ConnectWinner.Black;
            }
            else
            {
                a?.CheckHexagon(direction, this);
                b?.CheckHexagon(direction, this);
                c?.CheckHexagon(direction, this);
                d?.CheckHexagon(direction, this);
                e?.CheckHexagon(direction, this);
                f?.CheckHexagon(direction, this);
            }
        }
    }
}
public class Connect
{
    Hexagon hexHead1 = new Hexagon();
    Hexagon hexHead2 = new Hexagon();
    private int dim;
    private Hexagon[,] map;
    public Connect(string[] input)
    {
        //Map Inizialization
        dim = input[0].Replace(" ", "").Length;
        map = new Hexagon[dim, dim];
        for (int i = 0; i < dim; i++) { for (int j = 0; j < dim; j++) { map[i, j] = new Hexagon(); } }
        Game.SideWinner = dim - 1;
        Game.State = ConnectWinner.None;
        //Map Setup
        int x = 0, y = 0;
        for (int i = 0; i < dim; i++)
        {
            for (int j = 0; j < input[i].Length; j++)
            {
                char[] hexID = { '.', 'O', 'X' };
                int player = Array.FindIndex(hexID, t => t == input[i][j]);
                if (player != -1)
                {
                    //Check Connection Coordinates
                    Hexagon ConnectHex(int x, int y)
                    {
                        if (x >= 0 && x < dim && y >= 0 && y < dim)
                        {
                            return map[y, x];
                        }
                        else
                        {
                            return null;
                        }
                    }
                    //Establish Connections
                    Hexagon a, b, c, d, e, f;
                    int newX, newY;
                    newX = x; newY = y - 1; a = ConnectHex(newX, newY);   //Connect with A 
                    newX = x + 1; newY = y - 1; b = ConnectHex(newX, newY);   //Connect with B 
                    newX = x + 1; newY = y; c = ConnectHex(newX, newY);   //Connect with C 
                    newX = x; newY = y + 1; d = ConnectHex(newX, newY);   //Connect with D 
                    newX = x - 1; newY = y + 1; e = ConnectHex(newX, newY);   //Connect with E 
                    newX = x - 1; newY = y; f = ConnectHex(newX, newY);   //Connect with F 
                    map[y, x].Set(player, x, y, hexA: a, hexB: b, hexC: c, hexD: d, hexE: e, hexF: f);
                    x++;
                }
            }
            y++;
            x = 0;
        }
        //Head Setup
        hexHead1.Set(1, -1, -1, hexC: map[0, 0]);
        hexHead2.Set(2, -1, -1, hexD: map[0, 0]);
        //Map Checking for Winner
        for (int i = 0; i < dim; i++)
        {
            map[0, i].CheckHexagon(ConnectWinner.White, hexHead1);
            map[i, 0].CheckHexagon(ConnectWinner.Black, hexHead2);
        }
    }
    public ConnectWinner Result()
    {
        return Game.State;
    }
}
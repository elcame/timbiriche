using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BoardManager : MonoBehaviour
{
    public static BoardManager Instance;
    public int Width, max,Height,n,cont,p1,p2,ganador;
    public Point PointPrefab;
    public Line LinePrefab;
    public Square SquarePrefab;
    Point temporal;
    nodo [,] matriz;

    private void Awake()
    {
        Instance = this;
        
    }

    void Start()
    {
        Width = menu.instance.n;
        Height = menu.instance.n;
        n = menu.instance.n;
        for (int i = 0; i < Height; i++)
        {
            for (int j = 0; j < Width; j++)
            {
                var Position = new Vector2(i, j);
                Point newPoint = Instantiate(PointPrefab, Position, Quaternion.identity);
                newPoint.SetGridPosition(i, j);
            }
        }
        
        var Center = new Vector2((float)Height / 2 - 0.5f, (float)Width / 2 - 0.5f);
        Camera.main.transform.position = new Vector3(Center.x, Center.y, -5.0f);
        cont = 0;
        crear_matriz();
    }



    public void SetPoint(Point P)
    {
        if(temporal != null)
        {
            P.transform.DOShakePosition(1.0f, 0.2f);
            if (estan_cerca(temporal,P))
            {
                logica_de_juego(P);
            }
            else
            {
                temporal = P;
            }
        }
        else
        {
            temporal = P;
            P.transform.DOShakePosition(1.0f, 0.2f);
        }
    }

    void logica_de_juego(Point p)  
    {
        crear_linea(p);
        if (p.X == temporal.X)
        {
            if (p.Y < temporal.Y)
            {
                matriz[p.X, p.Y].up = true;
                matriz[temporal.X, temporal.Y].down = true;
            }
            else
            {
                matriz[p.X, p.Y].down = true;
                matriz[temporal.X, temporal.Y].up = true;

            }
        }
        else
        {
            if (p.X < temporal.X)
            {
                matriz[p.X, p.Y].right = true;
                matriz[temporal.X, temporal.Y].left = true;
            }
            else
            {
                matriz[p.X, p.Y].left = true;
                matriz[temporal.X, temporal.Y].right = true;
            }
        }

        for (int i = 0; i <= n - 1; i++)
        {
            for (int j = 0; j <= n - 1; j++)
            {
                if (matriz[i, j].up && matriz[i, j + 1].right && matriz[i + 1, j + 1].down && matriz[i + 1, j].left && !matriz[i + 1, j].encontrado)
                {
                    matriz[i + 1, j].encontrado = true;
                    cont += 1;
                    Debug.Log(max);
                    if (cont == max)
                    {
                        if(p1 == p2)
                        {
                            ganador = 0;
                        }
                        else
                        {
                            if (p1 > p2)
                            {
                                ganador = 1;
                            }
                            else
                            {
                                ganador = 2;
                            }
                        }
                        SceneManager.LoadScene("ganador");
                    }
                    Vector2 PointPos = new Vector2(i + 0.5f, j + 0.5f);
                    Square square = Instantiate(SquarePrefab, PointPos, Quaternion.identity);
                    square.transform.localScale = new Vector3(0.0f, 0.0f, 1.0f);
                    square.transform.DOScaleX(0.5f, 1.0f);
                    square.transform.DOScaleY(0.5f, 1.0f);
                    
                    if (GameManager.Instance.GetGameState == GameManager.GameState.player1)
                    {
                        square.GetComponent<SpriteRenderer>().DOColor(Color.green, 0.5f);
                        p1++;
                    }
                    else
                    {
                        square.GetComponent<SpriteRenderer>().DOColor(Color.magenta, 0.5f);
                        p2++;
                    }
                }
                else
                {
                    GameManager.Instance.SwitchPlayer();
                }
            }
        }
    }

    bool estan_cerca(Point p1,Point p2)
    {
        if (p1.X == p2.X)
        {
        return (Mathf.Abs(p1.Y - p2.Y) == 1);
        }
        else
        {
        if (p2.Y == p1.Y)
        {
        return (Mathf.Abs(p1.X - p2.X) == 1);

        }
        else
        {
        return false;
        }
        }

    }

    void crear_linea(Point P)
     {
  
        float X, Y;
        Quaternion Q = Quaternion.identity; ;
        if (P.X == temporal.X)
        {
            if(P.Y < temporal.Y)
            {
                Y = P.Y;
            }
            else
            {
                Y = temporal.Y;
            }
            Y += 0.5f;
            X = P.X;
        }
        else
        {
            if(P.X < temporal.X)
            {
                X = P.X;
            }
            else
            {
                X = temporal.X;

            }
            X += 0.5f;
            Y = temporal.Y;
            Q = Quaternion.Euler(0.0f, 0.0f, 90.0f);
        }
        Vector2 pos = new Vector2(X, Y);
        Line line = Instantiate(LinePrefab, pos, Q);
        line.transform.localScale = new Vector3(0.0f, 0.0f, 1.0f);
        line.transform.DOScaleX(0.5f, 1.0f);
        line.transform.DOScaleY(1.0f, 1.0f);

    }

    void crear_matriz()
    {
        int n = menu.instance.n;
        max = (n-1) * (n-1);
        matriz = new nodo[n, n];

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                matriz[i, j] = new nodo();
            }
        }
    }

    struct nodo
    {
        public bool up, right, left, down, encontrado;

        public nodo(bool hola)
        {
            this.up = false;
            this.right = false;
            this.left = false;
            this.down = false;
            this.encontrado = false;
        }
    }
}

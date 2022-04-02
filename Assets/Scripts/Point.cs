using UnityEngine;

public class Point : MonoBehaviour
{

    public int X,Y;


    private void OnMouseDown()
    {
        BoardManager.Instance.SetPoint(this);     
    }


    public void SetGridPosition(int X,int Y)
    {
        this.X = X;
        this.Y = Y;
    }

}

using TMPro;
using UnityEngine;
using DG.Tweening;

public class ganador : MonoBehaviour
{
    public TextMeshProUGUI texto;

    void Start()
    {
        if(BoardManager.Instance.ganador == 0)
        {
            texto.text = "EMPATE";
        }
        else
        {
            if (BoardManager.Instance.ganador == 1)
            {
                texto.text = "JUGADOR 1 GANÓ";
            }
            else
            {
                texto.text = "JUGADOR 2 GANÓ";
            }
        }
        texto.transform.DOShakeScale(10.0f,0.5f);
    }

}

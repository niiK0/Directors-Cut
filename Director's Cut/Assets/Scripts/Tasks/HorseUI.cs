using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class HorseUI : MonoBehaviour
{
    //Refer�ncias � UI
    [SerializeField] private RectTransform sliderBar;
    [SerializeField] private RectTransform sliderHandle;
    [SerializeField] private RectTransform woodenHorse;

    //Evento de conclus�o da task
    public event EventHandler OnHorseTaskCompleted;

    //Limites verticais da sliderHandle
    const float maxHandleY = 1016f;
    const float minHandleY = 180f;

    //Multiplicador para regular o balan�o do cavalinho
    const float rockingMultiplier = (21 / (maxHandleY - minHandleY));

    //Alturas da sliderBar qu devem ser atignidas para contar um balan�o
    const float upScoreThreshold = maxHandleY - 250;
    const float downScoreThreshold = minHandleY + 250;

    //Vari�veis relativas � quantidade de balan�os necess�rios para concluir a task
    const int amountOfRocking = 5; //Total de balan�os a se obter
    private int currRockingAmount = 0;
    private bool scoredUp = false;

    private bool barIsHeld = false;

    private float oldMY;

    private void Update()
    {
        float mX = Input.mousePosition.x;
        float mY = Input.mousePosition.y;

        if (Input.GetMouseButtonDown(0))
        {
            //Define um quadrado ao redor da sliderHandle
            float sliderHandleX1Pos = sliderHandle.anchoredPosition.x;
            float sliderHandleX2Pos = sliderHandleX1Pos + sliderHandle.rect.width;
            float sliderHandleY1Pos = sliderHandle.anchoredPosition.y;
            float sliderHandleY2Pos = sliderHandleY1Pos - sliderHandle.rect.height;

            //Checa se o cursor est� sobre a sliderHandle
            if (mX > sliderHandleX1Pos && mX < sliderHandleX2Pos && mY < sliderHandleY1Pos && mY > sliderHandleY2Pos)
            {
                barIsHeld = true;
                oldMY = mY;
            }
        }

        if (barIsHeld)
        {
            //Dist�ncia percorrida pela barra
            float Ydif = mY - oldMY;
            //Move a barra a dist�ncia percorrida
            sliderHandle.anchoredPosition = new Vector2(sliderHandle.anchoredPosition.x, sliderHandle.anchoredPosition.y + Ydif);
            //Inclina o cavalinho de acordo com a dist�ncia percorrida
            woodenHorse.eulerAngles = new Vector3(0f, 0f, woodenHorse.eulerAngles.z + Ydif * rockingMultiplier);

            //Impede a sliderHandle de sair da barra
            if (sliderHandle.anchoredPosition.y > maxHandleY)
            {
                sliderHandle.anchoredPosition = new Vector2(sliderHandle.anchoredPosition.x, maxHandleY);
            }
            if (sliderHandle.anchoredPosition.y < minHandleY)
            {
                sliderHandle.anchoredPosition = new Vector2(sliderHandle.anchoredPosition.x, minHandleY);
            }

            //Contagem dos balan�os
            if (scoredUp)
            {
                if (sliderHandle.anchoredPosition.y < downScoreThreshold)
                {
                    currRockingAmount++;
                    scoredUp = false;
                }
            } else {
                if (sliderHandle.anchoredPosition.y > upScoreThreshold)
                {
                    currRockingAmount++;
                    scoredUp = true;
                }
            }

            oldMY = mY;
        }

        if (Input.GetMouseButtonUp(0))
        {
            barIsHeld = false;
        }

        if (currRockingAmount >= amountOfRocking)
        {
            Debug.Log("Completo!");
            OnHorseTaskCompleted(this, EventArgs.Empty);
        }
    }
}

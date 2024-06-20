using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

[Serializable]
public class Colors
{
    public String Name;
    public Color Color;
}

public class Game_Controller : MonoBehaviour
{
    public List<Colors> Color_List;

    public TextMeshProUGUI Random_Color_Name;

    public List<Color_Butten> Right_Images, Left_Images, Up_Images, Down_Images;

    public int Win_Index;

    [Header("Score : ")]
    public int Score;
    public TextMeshProUGUI Score_Text;

    [Header("Sounds : ")]
    public AudioSource Win_Sound;
    public AudioSource Loos_Sound;

    [Header("\n Timer : \n")]
    public TextMeshProUGUI Timer_Text;
    public float Time_For_Timer = 5;
    public bool Timer_Is_Active = false;
    public float Takhir = 3;

    [Header("Panels : \n")]

    public GameObject Win_Panel;
    public GameObject Lost_Panel;

    public bool Game_Is_Over = false;


    void Start()
    {
        Randomaiser();
    }

    private void SetupColorButtons(List<Color_Butten> buttons, int index)
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            buttons[i].Color_Name = Color_List[index].Name;
            buttons[i].GetComponent<Image>().color = Color_List[index].Color;
        }
    }

    public void Randomaiser()
    {
        if (Game_Is_Over == false)
        {


            List<int> chosenIndices = new List<int>();

            int rightIndex, leftIndex, upIndex, downIndex;

            do
            {
                rightIndex = UnityEngine.Random.Range(0, Color_List.Count);
            } while (chosenIndices.Contains(rightIndex));
            chosenIndices.Add(rightIndex);

            do
            {
                leftIndex = UnityEngine.Random.Range(0, Color_List.Count);
            } while (chosenIndices.Contains(leftIndex));
            chosenIndices.Add(leftIndex);

            do
            {
                upIndex = UnityEngine.Random.Range(0, Color_List.Count);
            } while (chosenIndices.Contains(upIndex));
            chosenIndices.Add(upIndex);

            do
            {
                downIndex = UnityEngine.Random.Range(0, Color_List.Count);
            } while (chosenIndices.Contains(downIndex));
            chosenIndices.Add(downIndex);

            SetupColorButtons(Right_Images, rightIndex);
            SetupColorButtons(Left_Images, leftIndex);
            SetupColorButtons(Up_Images, upIndex);
            SetupColorButtons(Down_Images, downIndex);

            int randomSelectedIndex = UnityEngine.Random.Range(0, chosenIndices.Count);
            Win_Index = chosenIndices[randomSelectedIndex];
            Random_Color_Name.text = Color_List[Win_Index].Name;

            Invoke("Start_Game_After_Waite", Takhir);
        }
    }

    public void Start_Game_After_Waite()
    {
        Timer_Is_Active = true;
    }

    void Update()
    {
        Timer();
    }

    public void Check_The_Color(string Color)
    {
        if (Color == Color_List[Win_Index].Name)
        {
            Win_Sound.Play();
            Set_Score(5);
            Time_For_Timer = 10;
        }
        else
        {
            Loos_Sound.Play();
            Set_Score(-5);
            Time_For_Timer = 10;
        }

        Randomaiser();
    }

    public void Set_Score(int Cheng)
    {
        Score += Cheng;

        if(Score >= 40 )
        {
            Win_Panel.SetActive(true);
            Timer_Is_Active = false;
            Game_Is_Over = true;
        }

        if (Score <= 0)
        {
            Score = 0;
            Lost_Panel.SetActive(true);
            Timer_Is_Active = false;
            Game_Is_Over = true;
        }

        Score_Text.text = Score.ToString();

        Timer_Is_Active = false;
        Timer_Text.text = "";
        CancelInvoke();
    }

    public void Timer()
    {
        if (Timer_Is_Active)
        {
            Time_For_Timer -= Time.deltaTime;

            if (Time_For_Timer <= 0)
            {
                Set_Score(-5);
                Time_For_Timer = 5;
                Randomaiser();
            }

            Timer_Text.text = "Timer : " + Time_For_Timer.ToString("N0");
        }
    }


    public void Reset_Game()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

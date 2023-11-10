using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UI;

public class Bar : MonoBehaviour
{
    public Slider HP_Slider;
    public Slider Mental_Slider;
    public Slider Exp_Slider;
    public Text HP_Text;
    public Text Mental_Text;
    public Text Exp_Text;
    public Player player;
    // Start is called before the first frame update
    void Start()
    {
        HP_Slider = GameObject.Find("HP_Slider").GetComponent<Slider>();
        Mental_Slider = GameObject.Find("Mental_Slider").GetComponent<Slider>();
        Exp_Slider = GameObject.Find("Exp_Slider").GetComponent<Slider>();
        HP_Slider.minValue = 0;
        Mental_Slider.minValue = 0;
        Exp_Slider.minValue = 0;

    }
    void Update()
    {
        HP_Slider.maxValue = player.MaxHealth;//슬라이더의 최대값을 스텟의 최대체력으로 지정
        Mental_Slider.maxValue = player.MaxMental;//슬라이더의 최대값을 스텟의 최대멘탈로 지정
        Exp_Slider.maxValue = player.MaxExp;//슬라이더의 최대값을 스텟의 최대경험치로 지정
        HP_Slider.value = player.Health;//슬라이더의 값을 스텟의 체력으로 지정
        Mental_Slider.value = player.Mental;//슬라이더의 값을 스텟의 마나으로 지정
        Exp_Slider.value = player.Exp;//슬라이더의 값을 스텟의 멘탈로 지정
        HP_Text.text = (player.Health.ToString() + "/" + player.MaxHealth.ToString());//텍스트의 값을 스텟의 체력으로 지정
        Mental_Text.text = (player.Mental.ToString() + "/" + player.MaxMental.ToString());//텍스트의 값을 스텟의 마나으로 지정
        Exp_Text.text = (player.Exp.ToString() + "/" + player.MaxExp.ToString());//텍스트의 값을 스텟의 마나으로 지정
    }
}
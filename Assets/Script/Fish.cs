using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Fish : MonoBehaviour
{
    //array of bad fish text
    private string[] BadFish = { "I bully!", "I push people around.", "I make fun of my class mates.", "I yell at my parents.", "I never give back." };
    //array of good fish text
    private string[] GoodFish = { "I help my friends with the homework.", "I help people in need.", "I love my parents.", "I share with my friends." };
    private bool HarmfulFish;

    public Animator animator;

    public bool Harmful
    {
        get
        {
            return HarmfulFish;
        }
    }
    private Text TextBubble;
    public string Text
    {
        get
        {
            return TextBubble.text;
        }
        set
        {
            TextBubble.text = value;
        }
    }

    void Start()
    {
        TextBubble = GameObject.Find("Fish_text").GetComponent<Text>();
        animator = GetComponent<Animator>();
        //set if the fish is bad or good
        if (Random.value > 0.5f)
        {
            //good fish
            HarmfulFish = false;
            TextBubble.text = GoodFish[Random.Range(0, GoodFish.Length)];
        }
        else
        {
            //bad fish
            HarmfulFish = true;
            TextBubble.text = BadFish[Random.Range(0, BadFish.Length)];
        }
    }
}

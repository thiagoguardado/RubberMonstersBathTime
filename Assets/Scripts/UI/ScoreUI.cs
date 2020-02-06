using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
   private Text text;
   private Animator animator;
   private void Awake()
   {
       text = GetComponentInChildren<Text>();
       animator = GetComponent<Animator>();

       Events.Level.Start += Reset;
       Events.Score.Update += UpdateScore;
   }

   private void OnDestroy()
   {
       Events.Level.Start -= Reset;
   }

   private void Reset()
   {
       text.text = "0";
   }

   private void UpdateScore(int newScore)
   {
       text.text = newScore.ToString();
       animator.SetTrigger("pulse");
   }
}

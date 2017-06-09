﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCounter : MonoBehaviour {

	public static ScoreCounter instance;
	public float InitialScore = 0f;

	private float score;
	private float[] towerScore;
	private Text scoreText;
	private BuildManager buildManager;

	public void SetScore(float value){
		score = value;
	}

	public float GetScore(){
		return score;
	}

	public void KillEnemy(float scr){
		score += scr;
	}

	public void BuildTower(int index){
		score += towerScore[index];
	}

	private void Start () {
		buildManager = GameObject.Find ("GameMaster").GetComponent<BuildManager> ();
		scoreText = GameObject.Find ("ScoreNum").GetComponent<Text> ();
		towerScore = new float[buildManager.initialTowerScore.Length];
		SetTowerInitialScores ();
		SetScore (InitialScore);
		instance = this;
	}

	private void SetTowerInitialScores(){
		for(int i = 0 ; i < buildManager.initialTowerScore.Length; i ++){
			towerScore[i] = buildManager.initialTowerScore[i];
		}
	}

	private void Update(){
		UpdateUI ();
	}

	private void UpdateUI(){
		scoreText.text = Mathf.Round(GetScore()).ToString();
	}
}

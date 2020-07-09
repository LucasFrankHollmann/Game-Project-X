using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class ScriptGameController : MonoBehaviour
{
	static ScriptGameController instance;
	
	public enum enumGameState {
		Paused=0,
		UnpausedTimeRunning=1,
		UnpausedTimeStopped=2
	}

	enumGameState gameState;
	enumGameState lastState;
	
	int currentDay;
	
	void Awake(){
		currentDay = 5;
		if(instance != null){
			Destroy(gameObject);
			
		}
		else{
			instance = this;
			DontDestroyOnLoad(gameObject);
			this.gameState = enumGameState.UnpausedTimeStopped;
			this.lastState = enumGameState.UnpausedTimeStopped;
		}
	}

	public void pauseGameStateTrigger(){
		if(gameState == enumGameState.Paused){
			gameState = lastState;
			Time.timeScale = 1f;
		}
		else{
			Time.timeScale = 0f;
			lastState = gameState;
			gameState = enumGameState.Paused;
		}
	}

	public void timerGameStateTrigger(){
		if(gameState == enumGameState.Paused){
			if(lastState == enumGameState.UnpausedTimeStopped){
				lastState = enumGameState.UnpausedTimeRunning;
			}
			else{
				lastState = enumGameState.UnpausedTimeStopped;
			}
		}
		else{
			if(gameState == enumGameState.UnpausedTimeStopped){
				gameState = enumGameState.UnpausedTimeRunning;
			}
			else{
				gameState = enumGameState.UnpausedTimeStopped;
			}
		}
	}

	public int getGameState(){
		return (int)this.gameState;
	}

	public int getDay(){
		return currentDay;
	}
}

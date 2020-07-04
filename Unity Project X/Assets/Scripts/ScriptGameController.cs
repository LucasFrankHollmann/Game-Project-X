using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class ScriptGameController : MonoBehaviour
{
	static ScriptGameController instance;
	
	enum enumGameState {
		Paused=0,
		UnpausedTimeRunning=1,
		UnpausedTimeStopped=2
	}

	enumGameState gameState;
	
	void Awake(){
		if(instance != null){
			Destroy(gameObject);
			
		}
		else{
			instance = this;
			DontDestroyOnLoad(gameObject);
			this.gameState = enumGameState.UnpausedTimeStopped;
		}
	}

	public void setGameState(int newState){
		try{
			this.gameState = (enumGameState) newState;
		}
		catch (System.Exception){
			this.gameState = enumGameState.Paused;
			print("Wrong GameState Value passed, gameState set to Paused");
		}
	}
	public int getGameState(){
		return (int)this.gameState;
	}
}

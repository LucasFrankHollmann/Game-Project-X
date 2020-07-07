using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class ScriptGameController : MonoBehaviour
{
	static ScriptGameController instance;
	public enum enumAreaChunks{
		Home=0,
		Forest=1,
		Mountain=2
	}
	
	public enum enumGameState {
		Paused=0,
		UnpausedTimeRunning=1,
		UnpausedTimeStopped=2
	}

	enumGameState gameState;
	enumAreaChunks currentAreaChunk;
	int currentDay;
	
	void Awake(){
		currentAreaChunk = enumAreaChunks.Forest;
		currentDay = 5;
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

	//começa a mudar de cena/mapa
	public void changeAreaChunk(int nextScene){
		switch((int)nextScene){
			case 0: //Home

			break;
			case 1: //Forest
				currentAreaChunk = enumAreaChunks.Forest;
			break;
			case 2: //Mountain

			break;
		}
	}

	public int getDay(){
		return currentDay;
	}
}

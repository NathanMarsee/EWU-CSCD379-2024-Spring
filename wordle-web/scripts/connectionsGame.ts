import { ConnectionsList } from "./connectionsList";
import { Connection } from "./connection";

export class ConnectionsGame {
  public maxAttempts: number;
  public maxConnections: number;
  public gameState: GameState = GameState.Playing;
  public isBusy: boolean = false;
  public chosenConnections: Connection[] = [];
  public correctGuesses: number = 0;
  public numOfGuesses: number = 0;
  public guesses: string[] = [];

  constructor(maxAttempts: number = 5, maxConnections: number = 4) {
    this.maxAttempts = maxAttempts;
    this.maxConnections = maxConnections;
    this.isBusy = true;
    this.gameState = GameState.Playing;
  }
  public startNewGame() {
    this.numOfGuesses = 0;
    this.chosenConnections = [];
    this.guesses = [];
    this.correctGuesses = 0;
    this.gameState = GameState.Playing;

    // Get 4 random connections
    for (let i = 0; i < this.maxConnections; i++) {
      const randomIndex = Math.floor(Math.random() * ConnectionsList.length);
      this.chosenConnections.push(
        new Connection(
          ConnectionsList[randomIndex][0],
          ConnectionsList[randomIndex][1]
        )
      );
      console.log(
        "Connection at index " +
          randomIndex +
          " " +
          this.chosenConnections[i].description
      );
    }
  }

  public addGuess(word: string): boolean {
    if (this.guesses.includes(word)) {
      return false;
    } else if (this.guesses.length < this.chosenConnections[0].words.length) {
      this.guesses.push(word);
      console.log("Guesses: " + this.guesses);
      return true;
    }
    console.log("Guesses: " + this.guesses);
    return false;
  }

  public removeGuess(word: string): boolean {
    if (!this.guesses.includes(word)) {
      return false;
    } else if (this.guesses.length === 0) {
      return false;
    }
    this.guesses = this.guesses.filter((guess) => guess !== word);
    console.log("Guesses: " + this.guesses);
    return true;
  }

  public checkIfWon(){
    if(this.correctGuesses === this.maxConnections){
      this.gameState = GameState.Won;
    }else if(this.numOfGuesses === this.maxAttempts){
      this.gameState = GameState.Lost;
    }
  }
  public checkGuess(): boolean {
    if (this.guesses.length !== this.chosenConnections[0].words.length) {
      return false;
    }
    // check if all the guess make up a connection
    for (let i = 0; i < this.chosenConnections.length; i++) {
      if (this.chosenConnections[i].words.every((word) =>this.guesses.includes(word))) {
        this.correctGuesses++;
        this.checkIfWon();
        return true;
      }
    }
    this.numOfGuesses++;
    this.checkIfWon();
    return false;
    
  }
}

export enum GameState {
  Playing,
  Won,
  Lost,
}

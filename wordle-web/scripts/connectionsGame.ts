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

  constructor(maxAttempts: number = 5, maxConnections: number = 4) {
    this.maxAttempts = maxAttempts;
    this.maxConnections = maxConnections;
    this.isBusy = true;
    this.gameState = GameState.Playing;
  }
  public startNewGame() {
    this.numOfGuesses = 0;
    this.chosenConnections = [];

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
  public checkGuess(guess: string[]): boolean {
    if (guess.length !== this.chosenConnections[0].words.length) {
      return false;
    }
    let isCorrect = false;
    // check if all the guess make up a connection
    for (let i = 0; i < this.chosenConnections.length; i++) {
      if (
        this.chosenConnections[i].words.every((word) => guess.includes(word))
      ) {
        isCorrect = true;
        break
      }
    }
    if (isCorrect) {
      ++this.correctGuesses;
      if (this.correctGuesses === this.maxConnections) {
        this.gameState = GameState.Won;
      }
      return true;
    } else {
      this.numOfGuesses++;
      if (this.numOfGuesses === this.maxAttempts) {
        this.gameState = GameState.Lost;
      }
      return false;
    }
  }
}

export enum GameState {
  Playing,
  Won,
  Lost,
}

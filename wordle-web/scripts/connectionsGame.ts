import { ConnectionsList } from "./connectionsList";
import { Connection, ConnectionState, ConnectionsWord } from "./connection";
import Axios from "axios";

export class ConnectionsGame {
  public maxAttempts: number;
  public maxConnections: number;
  public gameState: GameState = GameState.Playing;
  public isBusy: boolean = false;
  public chosenConnections: Connection[] = [];
  public correctGuesses: number = 0;
  public numOfGuesses: number = 0;
  public guesses: string[] = [];
  public correctGuessesList: string[] = [];

  constructor(maxAttempts: number = 5, maxConnections: number = 4) {
    this.maxAttempts = maxAttempts;
    this.maxConnections = maxConnections;
    this.isBusy = true;
    this.gameState = GameState.Playing;
  }
  public async startNewGame(withApi: boolean) {
    this.numOfGuesses = 0;
    this.chosenConnections = [];
    this.guesses = [];
    this.correctGuesses = 0;
    this.gameState = GameState.Playing;
    this.correctGuessesList = [];

    if (withApi) {
      this.chosenConnections = await this.getConnectionOfTheDayFromApi();
    } else {
      this.chosenConnections = await this.getRandomConnectionFromApi();
    }
  }
  public startNewGameOffline() {
    this.numOfGuesses = 0;
    this.chosenConnections = [];
    this.guesses = [];
    this.correctGuesses = 0;
    this.gameState = GameState.Playing;
    this.correctGuessesList = [];
    this.chosenConnections = this.getRandomConnection();
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

  public checkIfWon() {
    if (this.correctGuesses === this.maxConnections) {
      this.gameState = GameState.Won;
    } else if (this.numOfGuesses === this.maxAttempts) {
      this.gameState = GameState.Lost;
    }
  }
  public checkGuess(): boolean {
    if (this.guesses.length !== this.chosenConnections[0].words.length) {
      return false;
    }
    // check if all the guess make up a connection
    for (let i = 0; i < this.chosenConnections.length; i++) {
      if (
        this.chosenConnections[i].words.every((word) =>
          this.guesses.includes(word.word)
        )
      ) {
        for (let j = 0; j < this.chosenConnections[i].words.length; j++) {
          this.chosenConnections[i].words[j].state = ConnectionState.Correct;
        }
        this.correctGuesses++;
        this.checkIfWon();
        this.guesses = [];

        console.log("the guess is correct");
        return true;
      }
    }
    for (let i = 0; i < this.chosenConnections.length; i++) {
      for (let j = 0; j < this.chosenConnections[i].words.length; j++) {
        for (let k = 0; k < this.guesses.length; k++) {
          if (this.chosenConnections[i].words[j].word === this.guesses[k]) {
            this.chosenConnections[i].words[j].state = ConnectionState.Wrong;
          }
        }
      }
    }
    console.log("the guess is wrong");
    this.numOfGuesses++;
    this.checkIfWon();
    this.guesses = [];
    return false;
  }

  public parseAPIResponse(response: any): Connection[] {
    let rawConnections = response.data as ConnectionsFromApi;
    let connectionsFromApi: Connection[] = [];
    this.maxConnections = rawConnections.count;
    for (
      let i = 0;
      i < rawConnections.connections.length;
      i += rawConnections.count + 1
    ) {
      let ConnectionsArray: ConnectionsWord[] = [];
      console.log("index i " + i + " " + rawConnections.connections[i]);
      for (let j = i + 1; j < rawConnections.count + i + 1; j++) {
        console.log("index j " + j + " " + rawConnections.connections[j]);
        ConnectionsArray.push(
          new ConnectionsWord(rawConnections.connections[j])
        );
      }
      console.log("connections array " + ConnectionsArray);
      let connection = new Connection(
        rawConnections.connections[i],
        ConnectionsArray
      );
      connectionsFromApi.push(connection);
    }
    for (let i = 0; i < connectionsFromApi.length; i++) {
      console.log(
        "connection parsed to return " +
          connectionsFromApi[i].description +
          " " +
          connectionsFromApi[i].words
      );
    }
    return connectionsFromApi;
  }
  public async getConnectionOfTheDayFromApi(): Promise<Connection[]> {
    try {
      let connectionUrl = "Connection/ConnectionsOfTheDay";
      const response = await Axios.get(connectionUrl);
      console.log("response from api " + response.data);
      return this.parseAPIResponse(response);
    } catch (error) {
      console.error("error getting connections ofr today " + error);
      return this.getRandomConnection();
    }
  }
  public async getRandomConnectionFromApi(): Promise<Connection[]> {
    try {
      let connectionUrl = "Connection/RandomConnections?count=4";
      const response = await Axios.get(connectionUrl);
      return this.parseAPIResponse(response);
    } catch (error) {
      console.error("error getting random connection " + error);
      return this.getRandomConnection();
    }
  }
  public getRandomConnection(): Connection[] {
    let randomCollection: Connection[] = [];
    let randomWrd: ConnectionsWord[] = [];
    for (let i = 0; i < this.maxConnections; i++) {
      const randomIndex = Math.floor(Math.random() * ConnectionsList.length);
      for (let j = 0; j < ConnectionsList[randomIndex][1].length; j++) {
        randomWrd.push(new ConnectionsWord(ConnectionsList[randomIndex][1][j]));
      }
      randomCollection.push(
        new Connection(ConnectionsList[randomIndex][0], randomWrd)
      );
    }
    return randomCollection;
  }
}
interface ConnectionsFromApi {
  count: number;
  connections: string[];
}
export enum GameState {
  Playing,
  Won,
  Lost,
}
/* API returns
{
  "count": 4,
  "connections": [
    "U.S. cities",
    "billings",
    "buffalo",
    "mobile",
    "phoenix",
    "what 'digs' might mean",
    "apartment",
    "insults",
    "likes",
    "shovels",
    "remove, as body hair",
    "laser",
    "pluck",
    "thread",
    "wax",
    "b-_ _ _",
    "ball",
    "movie",
    "school",
    "vitamin"
  ]
}*/

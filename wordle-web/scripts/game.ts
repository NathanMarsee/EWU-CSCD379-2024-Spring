import axios from "~/plugins/axios";
import { LetterState, type Letter } from "./letter";
import { Word } from "./word";
import { WordList } from "./wordList";
import { findValidWords } from "./ValidWordList";
import Axios from "axios";
import { GameStats } from "./gameStats";


export class Game {
  public maxAttempts: number;
  public guesses: Word[] = [];
  public guessIndex: number = 0;
  public gameState: GameState = GameState.Playing;
  public guessedLetters: Letter[] = [];
  public isBusy: boolean = false;
  public stats: GameStats | null = null;

  private _secretWord: string = "";
  private set secretWord(value: string) {
    this._secretWord = value.toUpperCase();
  }
  public get secretWord(): string {
    return this._secretWord;
  }

  constructor(maxAttempts: number = 6) {
    this.maxAttempts = maxAttempts;
    this.isBusy = true;
    this.gameState = GameState.Playing;
  }

  public async startNewGameAPI(word?: string | undefined) {
    // Load the game
    this.isBusy = true;

    // Reset default values
    this.guessIndex = 0;
    this.guessedLetters = [];
    this.stats = null;

    // Get a word
    if (!word) {
      this.secretWord = await this.getWordOfTheDayFromApi();
    } else {
      this.secretWord = word;
    }

    // Populate guesses with the correct number of empty words
    this.guesses = [];
    for (let i = 0; i < this.maxAttempts; i++) {
      this.guesses.push(
        new Word({ maxNumberOfLetters: this.secretWord.length })
      );
    }

    // Start the game
    this.gameState = GameState.Playing;
    this.isBusy = false;
  }
  public async startNewGameRandom(word?: string | undefined) {
    // Load the game
    this.isBusy = true;

    // Reset default values
    this.guessIndex = 0;
    this.guessedLetters = [];
    this.stats = null;

    // Get a word
    if (!word) {
      this.secretWord =
        WordList[Math.floor(Math.random() * WordList.length)].toUpperCase();
      console.log("Word " + this.secretWord);
    } else {
      this.secretWord = word;
    }

    // Populate guesses with the correct number of empty words
    this.guesses = [];
    for (let i = 0; i < this.maxAttempts; i++) {
      this.guesses.push(
        new Word({ maxNumberOfLetters: this.secretWord.length })
      );
    }

    // Start the game
    this.gameState = GameState.Playing;
    this.isBusy = false;
  }

  public get guess() {
    return this.guesses[this.guessIndex];
  }

  public setGuessLetters(word: string) {
    // Loop through the word and add new letters
    this.guess.clear();
    for (let i = 0; i < word.length; i++) {
      this.addLetter(word[i].toUpperCase());
    }
  }

  public removeLastLetter() {
    if (this.gameState === GameState.Playing) {
      this.guess.removeLastLetter();
    }
  }

  public addLetter(letter: string) {
    if (this.gameState === GameState.Playing) {
      this.guess.addLetter(letter);
    }
  }
  public updateGuessedLetters() {
    for (const letter of this.guess.letters) {
      // Find the index of the letter in the guessed letters array
      const index = this.guessedLetters.findIndex(
        (existingLetter) => existingLetter.char === letter.char
      );
      if (index !== -1) {
        // Do not update the letter if it is already correct
        if (this.guessedLetters[index].state !== LetterState.Correct) {
          // Do not update the letter if it is wrong
          if (letter.state !== LetterState.Wrong) {
            this.guessedLetters[index] = letter;
          }
        }
      } else {
        // If letter does not already exist, add it to the array
        this.guessedLetters.push(letter);
      }
    }
  }

  public async submitGuess(withApi: boolean, userName: string, time: number) {
    if (this.gameState !== GameState.Playing) return;
    if (!this.guess.isFilled) return;
    if (!this.guess.isValidWord()) {
      this.guess.clear();
      return;
    }

    const isCorrect = this.guess.compare(this.secretWord);
    this.updateGuessedLetters();
    //this.validWordList = findValidWords(this.guesses, this.guessIndex, this.validWordList);

    if (isCorrect) {
      this.gameState = GameState.Won;
    } else {
      if (this.guessIndex === this.maxAttempts - 1) {
        this.gameState = GameState.Lost;
      } else {
        this.guessIndex++;
      }
    }

    if (this.gameState === GameState.Won || this.gameState === GameState.Lost && withApi) {
      this.isBusy = true;
      console.log("Data for Game/Result: attempts: " + (this.guessIndex+ 1) + " isWin: " + (this.gameState === GameState.Won) + " word: " +  this.secretWord + " time: " + time + " playerName: " + userName);
      if (withApi) {
        var result = await Axios.post("Game/Result", {
          attempts: this.guessIndex + 1,
          isWin: this.gameState === GameState.Won,
          word: this.secretWord,
          time: time,
          playerName: userName
        
        });
       
        console.log("Result from API Game/Result: " + result.data + " " + result.status);
        this.stats = new GameStats();
        console.log("Result from API Game/Result: " + result.data);
        Object.assign(this.stats, result.data);
        this.isBusy = false;
      }
    }

  }

  public async getWordOfTheDayFromApi(): Promise<string> {
    try {
      let wordUrl = "Word/WordOfTheDay";

      const response = await Axios.get(wordUrl);

      console.log("Response from API get word of the day: " + response.data);
      return response.data;
    } catch (error) {
      console.error("Error fetching word of the day:", error);
      return "ERROR"; // Probably best to print the error on screen, but this is kind of funny. :)
    }
  }
}

export enum GameState {
  Playing,
  Won,
  Lost,
}

//@vitest-environment nuxt
import { beforeEach, expect, test } from "vitest";
import { ConnectionsGame, GameState } from "~/scripts/connectionsGame";
import { Connection } from "~/scripts/connection";

//create game and check if it's created
test("connections-game", () => {
  const game = new ConnectionsGame();
  game.startNewGame();
  expect(game.chosenConnections.length).toBe(4);
});

//test if the guess is correct 
test("check-guess", () => {
  const game = new ConnectionsGame();
  game.chosenConnections.push(new Connection("car", ["engine", "wheels", "gas", "brakes" ]));
  const guess = ["engine", "wheels", "gas", "brakes" ];
  expect(game.checkGuess(guess)).toBe(true);
});
//test if the guess is correct if the order is different
test("check-guessCorrectOnDiffOrder", () => {
  const game = new ConnectionsGame();
  game.chosenConnections.push(new Connection("car", ["engine", "wheels", "gas", "brakes" ]));
  const guess = ["wheels", "engine", "brakes", "gas" ];
  expect(game.checkGuess(guess)).toBe(true);
});
//test if the guess is incorrect  
test("check-guessIncorrect", () => {
  const game = new ConnectionsGame();
  game.chosenConnections.push(new Connection("car", ["engine", "wheels", "gas", "brakes" ]));
  const guess = ["engine", "wheels", "door", "brakes", "tires" ];
  expect(game.checkGuess(guess)).toBe(false);
});
//test if the game is won
test("check-GameIsWon", () => {
  const game = new ConnectionsGame(1,1);
  console.log(`Test initialized with maxConnections: ${game.maxConnections}`);
  game.chosenConnections.push(new Connection("car", ["engine", "wheels", "gas", "brakes" ]));
  const guess = ["engine", "wheels", "gas", "brakes" ];
  game.checkGuess(guess);
  expect(game.gameState).toBe(GameState.Won);
});
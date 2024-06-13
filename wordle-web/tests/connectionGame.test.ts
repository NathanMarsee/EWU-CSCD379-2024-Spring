//@vitest-environment nuxt
import { beforeEach, expect, test } from "vitest";
import { ConnectionsGame, GameState } from "~/scripts/connectionsGame";
import { Connection, ConnectionsWord } from "~/scripts/connection";

//create game and check if it's created
test("connections-game", () => {
  const game = new ConnectionsGame();
  game.startNewGameOffline();
  expect(game.chosenConnections.length).toBe(game.maxConnections);
});

//test if the guess is correct
test("check-guess", () => {
  const game = new ConnectionsGame();
  game.chosenConnections.push(
    new Connection("car", [
      new ConnectionsWord("engine"),
      new ConnectionsWord("wheels"),
      new ConnectionsWord("gas"),
      new ConnectionsWord("brakes"),
    ])
  );
  game.guesses = ["engine", "wheels", "gas", "brakes"];
  expect(game.checkGuess()).toBe(true);
});
//test if the guess is correct if the order is different
test("check-guessCorrectOnDiffOrder", () => {
  const game = new ConnectionsGame();
  game.chosenConnections.push(
    new Connection("car", [
      new ConnectionsWord("engine"),
      new ConnectionsWord("wheels"),
      new ConnectionsWord("gas"),
      new ConnectionsWord("brakes"),
    ])
  );
  game.guesses = ["wheels", "engine", "brakes", "gas"];
  expect(game.checkGuess()).toBe(true);
});
//test if the guess is incorrect
test("check-guessIncorrect", () => {
  const game = new ConnectionsGame();
  game.chosenConnections.push(
    new Connection("car", [
      new ConnectionsWord("engine"),
      new ConnectionsWord("wheels"),
      new ConnectionsWord("gas"),
      new ConnectionsWord("brakes"),
    ])
  );
  game.guesses = ["engine", "wheels", "door", "brakes"];
  expect(game.checkGuess()).toBe(false);
});
//test if the game is won
test("check-GameIsWon", () => {
  const game = new ConnectionsGame(1, 1);
  console.log(`Test initialized with maxConnections: ${game.maxConnections}`);
  game.chosenConnections.push(
    new Connection("car", [
      new ConnectionsWord("engine"),
      new ConnectionsWord("wheels"),
      new ConnectionsWord("gas"),
      new ConnectionsWord("brakes"),
    ])
  );
  game.guesses = ["engine", "wheels", "gas", "brakes"];
  game.checkGuess();
  expect(game.gameState).toBe(GameState.Won);
});

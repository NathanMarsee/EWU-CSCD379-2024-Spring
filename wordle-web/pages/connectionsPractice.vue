<template>
  <v-alert
    v-if="game.gameState != GameState.Playing"
    :color="game.gameState == GameState.Won ? 'success' : 'error'"
    class="mb-5"
    tile
  >
    <div></div>
    <h3>
      You've
      {{ game.gameState == GameState.Won ? "Won" : "Lost" }}
    </h3>
    <v-card-text v-for="(connection, k) of game.chosenConnections">
      The connection was: <strong>{{ connection.description }}</strong>
      <div v-for="(word, k) of connection.words">
        The words were: <strong>{{ word }}</strong>
      </div>
    </v-card-text>
    <v-btn variant="outlined" @click="startNewGame()">
      <v-icon size="large" class="mr-2"> mdi-restart </v-icon> Restart Game
    </v-btn>
  </v-alert>
  <v-card>
    <div class="my-5"></div>
    <ConnectionBoard
      v-for="(connection, k) of game.chosenConnections"
      :key="k"
      :guess="connection"
      :state="connection.connectionState"
    />
    <div class="d-flex justify-center my-5">your selected guesses are {{ game.guesses }}</div>
    <div class="d-flex justify-center my-5">
      <v-btn
        color="primary"
        :disabled="game.gameState != GameState.Playing"
        @click="game.checkGuess()"
        >Submit your guess</v-btn
      >
    </div>
    
    <div class="d-flex justify-center my-5">Number of guesses left {{   game.maxAttempts - game.numOfGuesses}}</div>
  </v-card>
</template>
<script setup lang="ts">
import { ConnectionsGame, GameState } from "~/scripts/connectionsGame";
import { ConnectionState } from "~/scripts/connection";
const game = reactive(new ConnectionsGame());
game.startNewGame(false);
provide("ConnectionGame", game);
const boxColor = computed(() => {
  if (game.gameState == GameState.Playing) {
    return "wrong";
  }
  return game.gameState == GameState.Won ? "success" : "error";
});
function startNewGame() {
  game.startNewGame(false);
}
</script>

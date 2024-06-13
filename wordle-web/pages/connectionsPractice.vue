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
    <v-btn variant="outlined" @click="game.startNewGame()">
      <v-icon size="large" class="mr-2"> mdi-restart </v-icon> Restart Game
    </v-btn>
  </v-alert>
  <v-card>
    <div class="my-5"></div>
    <ConnectionBoard
      v-for="(connection, k) of game.chosenConnections"
      :key="k"
      :guess="connection"
    />
    <div class="d-flex justify-center my-5">
      <v-btn
        color="primary"
        :disabled="game.gameState != GameState.Playing"
        @click="checkGuess()"
        >Submit your guess</v-btn
      >
    </div>
  </v-card>
</template>
<script setup lang="ts">
import { ConnectionsGame, GameState } from "~/scripts/connectionsGame";
const game = reactive(new ConnectionsGame());
const isCorrect = ref(false);
game.startNewGame();
provide("ConnectionGame", game);
provide("isCorrect", isCorrect);
function checkGuess() {
  isCorrect.value = game.checkGuess();
}
</script>

<template>
  <v-progress-linear v-if="game.isBusy" color="primary" indeterminate />
  <HowToPlayConnectionsDialog v-model="showHelpDialog" />
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
      The words were:
      <div v-for="(word, k) of connection.words">
        <strong>{{ word.word }}</strong>
      </div>
    </v-card-text>
    <v-btn variant="outlined" @click="startNewGame()">
      <v-icon size="large" class="mr-2"> mdi-restart </v-icon> Restart Game
    </v-btn>
  </v-alert>
  <v-card>
    <v-btn
      icon="mdi-help-box"
      class="d-flex justify left"
      @click="showHelpDialog = true"
    />
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
        @click="game.checkGuess()"
        >Submit your guess</v-btn
      >
    </div>

    <div class="d-flex justify-center my-5">
      Number of incorrect guesses left:
      {{ game.maxAttempts - game.numOfGuesses }}
    </div>

    <v-row>
      <v-col>
        <v-text-field
          label="Max wrong answers"
          v-model="maxAttempts"
          type="number"
          min="1"
          max="10"
          step="1"
          outlined
        ></v-text-field>
      </v-col>
      <v-col>
        <v-text-field
          label="Number of connections to guess"
          v-model="maxConnections"
          type="number"
          min="1"
          max="10"
          step="1"
          outlined
        ></v-text-field>
      </v-col>
      <v-col>
        <v-btn @click="startNewGame()" color="primary">Restart Game</v-btn>
      </v-col>
    </v-row>
  </v-card>
</template>
<script setup lang="ts">
import { ConnectionsGame, GameState } from "~/scripts/connectionsGame";
import { ConnectionState } from "~/scripts/connection";
const game = reactive(new ConnectionsGame());
const maxAttempts = ref(game.maxAttempts);
const maxConnections = ref(game.maxConnections);
game.startNewGame(false);
provide("ConnectionGame", game);
const showHelpDialog = ref(false);

function startNewGame() {
  game.startNewGame(false, maxAttempts.value, maxConnections.value);
}
</script>

<template>
  <HelpDialog v-model="showHelpDialog" />
  <v-container>
    <v-card class="text-center">
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
        <v-card-text>
          The word was: <strong>{{ game.secretWord }}</strong>
        </v-card-text>
        <v-btn variant="outlined" @click="game.startNewGameRandom()">
          <v-icon size="large" class="mr-2"> mdi-restart </v-icon> Restart Game
        </v-btn>
      </v-alert>
      <v-btn
        icon="mdi-help-box"
        class="d-flex justify left"
        @click="showHelpDialog = true"
      />
      <v-card-title>Practice Wordle</v-card-title>
      <v-card-subtitle
        >Practice your wordle skills with a different word each
        time!</v-card-subtitle
      >
      <div class="my-5"></div>
      <GameBoardGuess
        v-for="(guess, i) of game.guesses"
        :key="i"
        :guess="guess"
      />

      <div class="my-10">
        <Keyboard />
      </div>

      <div class="d-flex justify-center mt-3 mb-5">
        <ValidWord />
        <div class="mx-3"></div>
        <v-btn @click="game.submitGuess(false, '', 0)" color="primary"
          >Guess!</v-btn
        >
        <div class="mx-3"></div>
      </div>
    </v-card>
  </v-container>
</template>

<script setup lang="ts">
import { Game, GameState } from "../scripts/game";

const showHelpDialog = ref(false);
const router = useRouter();
const game = reactive(new Game());
game.startNewGameRandom();
provide("GAME", game);

const showUserNameDialog = ref(false);

onMounted(() => {
  // Get random word from word list

  window.addEventListener("keyup", onKeyup);
});

onUnmounted(() => {
  window.removeEventListener("keyup", onKeyup);
});
function onKeyup(event: KeyboardEvent) {
  if (event.key === "Enter") {
    game.submitGuess(false, "", 0);
  } else if (event.key == "Backspace") {
    game.removeLastLetter();
  } else if (event.key.match(/[A-z]/) && event.key.length === 1) {
    game.addLetter(event.key.toUpperCase());
  }
}
</script>

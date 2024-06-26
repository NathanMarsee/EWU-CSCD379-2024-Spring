<template>
  <UserNameDialog v-model="showUserNameDialog" />

  <HelpDialog v-model="showHelpDialog" />
  <v-container>
    <v-progress-linear v-if="game.isBusy" color="primary" indeterminate />
    <v-card v-else class="text-center">
      <v-btn
        icon="mdi-help-box"
        class="d-flex justify left"
        @click="showHelpDialog = true"
      />
      <v-alert
        v-if="game.gameState != GameState.Playing"
        :color="game.gameState == GameState.Won ? 'success' : 'error'"
        class="mb-5"
        tile
      >
        <h3>
          You've
          {{ game.gameState == GameState.Won ? "Won!" : "Lost :()" }}
        </h3>
        <v-card-text>
          The word was: <strong>{{ game.secretWord }}</strong>
        </v-card-text>
        <v-row v-if="game.stats" class="mb-1" justify="center">
          <v-col cols="auto">
            <v-progress-circular
              size="75"
              width="10"
              v-model="game.stats.winPercentage"
            >
              {{ game.stats.winPercentage }} %
            </v-progress-circular>
            <br />
            <i class="text-caption"> Success Rate </i>
          </v-col>
          <v-col cols="auto">
            <v-progress-circular
              size="75"
              width="10"
              :model-value="game.stats.averageGuessesPercent(game.maxAttempts)"
            >
              {{
                game.stats.averageGuessesPercent(game.maxAttempts).toFixed(0)
              }}
              %
            </v-progress-circular>
            <br />
            <i class="text-caption"> Average Guesses </i>
          </v-col>
        </v-row>
        <v-btn
          variant="outlined"
          @click="game.startNewGameAPI(), (startTime = new Date().getTime())"
        >
          <v-icon size="large" class="mr-2"> mdi-restart </v-icon> Restart Game
        </v-btn>
      </v-alert>
      <v-card-title v-else>Wordle</v-card-title>
    <v-card-subtitle>Play the wordle of the day</v-card-subtitle>
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
        <v-btn
          @click="game.submitGuess(true, userName, calcSecond())"
          class="mb-5"
          color="primary"
          >Guess!</v-btn
        >
        <div class="mx-3"></div>
        <v-btn
          class="mb-5 ml-5"
          color="primary"
          variant="flat"
          @click="router.push('/leaderboard')"
          >Leaderboard</v-btn
        >
      </div>
    </v-card>
  </v-container>
</template>

<script setup lang="ts">
import { Game, GameState } from "../scripts/game";
import Axios from "axios"; //npm install axios

const router = useRouter();
const showHelpDialog = ref(false);
const userName: Ref<string> = inject("userName")! as Ref<string>;
const game = reactive(new Game());
game.startNewGameAPI();
provide("GAME", game);
const showUserNameDialog = ref(false);
var startTime = new Date().getTime();

//watch for the route to change to this page, then check if the user is a guest or not then prompt the user to enter a name

onMounted(() => {
  checkUserName();
  window.addEventListener("keyup", onKeyup);
});

function onKeyup(event: KeyboardEvent) {
  if (event.key === "Enter") {
    game.submitGuess(true, userName.value, calcSecond());
  } else if (event.key == "Backspace") {
    game.removeLastLetter();
  } else if (event.key.match(/[A-z]/) && event.key.length === 1) {
    game.addLetter(event.key.toUpperCase());
  }
}
function calcAttempts() {
  var attempts = 0;
  if (game.gameState == GameState.Won) {
    attempts = game.guessIndex + 1;
  } else {
    attempts = game.guesses.length + 5;
  }
  return attempts;
}
function postScore(playerNameIn: string, attemptsIn: number, timeIn: number) {
  console.log(
    "data for Player/UpdateScore: " +
      playerNameIn +
      " " +
      attemptsIn +
      " " +
      timeIn
  );
  let postScoreUrl = "Player/UpdateScore";
  Axios.post(postScoreUrl, {
    playerName: playerNameIn,
    attempts: attemptsIn,
    time: timeIn,
  }).then((response) => {
    console.log(
      "response from API Player/UpdateScore " +
        response.data +
        " " +
        response.status
    );
  });
}
function calcSecond() {
  var endTime = new Date().getTime();
  var timeDiff = endTime - startTime;
  return timeDiff / 1000;
}
function checkUserName() {
  if (userName.value === "guest" || userName.value === "") {
    showUserNameDialog.value = true;
  }
}
watch(
  () => game.gameState,
  (value) => {
    if (value == GameState.Won || value == GameState.Lost) {
      if (userName.value === "guest" || userName.value === "") {
        showUserNameDialog.value = true;
        watch(
          () => showUserNameDialog.value,
          (value) => {
            if (value == false) {
              if (userName.value === "") {
                userName.value = "guest";
              }
              postScore(userName.value, calcAttempts(), calcSecond());
            }
          }
        );
      } else {
        postScore(userName.value, calcAttempts(), calcSecond());
      }
    }
  }
);
</script>

<template>
  <v-card>
    <v-sheet color="primary">
      <v-card-text class="text-center"> How to play Connections </v-card-text>
    </v-sheet>
    <v-card-text class="text-center">
      Connections is a word puzzle where the goal is to select common words
      related by a connection
      <v-spacer></v-spacer>

      To select a word for a guess, click on it and the color will change to
      <v-btn size="x-small" icon color="primary" /><br />

      Once you have selected 4 guesses click submit your guess The game will let
      you know if the connection is correct by turning
      <v-btn size="x-small" icon color="correct" /> or
      <v-btn size="x-small" icon color="wrong" /> if incorrect
    </v-card-text>
    <v-card-text class ="text-center">
      For example, the connection "things cars have" might have the words:
    </v-card-text>
    <div class="d-flex justify-center mt-3 mb-5">
      <ConnectionBoard :guess="exampleConnection" />
    </div>

    <v-divider />
    <v-card-actions>
      <v-spacer></v-spacer>
      <v-btn
          color="primary"
          variant="flat"
          text="home"
          @click="router.push('/')"
        />
    </v-card-actions>
  </v-card>
</template>

<script setup lang="ts">
import { ConnectionsWord, Connection } from "~/scripts/connection";
const router = useRouter();
let exampleConnection: Connection = new Connection("things cars have", [
  new ConnectionsWord("wheels"),
  new ConnectionsWord("engine"),
  new ConnectionsWord("locks"),
  new ConnectionsWord("windows"),
]);
</script>

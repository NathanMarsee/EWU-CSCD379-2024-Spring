<template>
  <v-card>
    <ConnectionBoard v-for="(connection, k) of game.chosenConnections" :key="k" :guess="connection" />
  </v-card>
</template>
<script setup lang="ts">
import { ConnectionsGame } from "~/scripts/connectionsGame";
import { Connection } from "~/scripts/connection";
const game = reactive(new ConnectionsGame());
game.startNewGame();
provide("ConnectionGame", game);
</script>

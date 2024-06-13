<template>
  <v-card class="text-center">
    <v-card-title>Pentagram</v-card-title>
    <v-card-subtitle
      >This is our final project called Pentagram. Penta for five, and gram for
      letter</v-card-subtitle
    >
    <v-card-text class="text-center"
      >This is a clone of a couple of the New York Time word games.
      <br>We wanted to make a version that lets you Practice and play as much as you want. 
      <br>There is also have a leader board if you want to see how you compare to others.
    </v-card-text>

    <v-card-text class="text-center">
      The pages for each game are divided into two menus at the top of the page one for wordle and one for connections. 
      <br> You can just enter a user name to have your scores saved to the leader board, or you can log in to edit the word list
      <br>Click on <v-icon>mdi-bullseye-arrow</v-icon> to go to the wordle menu and click on connections to go to the connections menu.
      <br> On the Game pages you can click on the <v-icon>mdi-help-box</v-icon> to see how to play the game
    </v-card-text>
    <div class="d-flex justify-center mt-3 mb-5">
      <v-btn
        variant="flat"
        color="primary"
        @click="router.push('/connectionsPractice')"
        >Play Practice Connections</v-btn
      >
      <div class="mx-3"></div>
      <v-btn
        variant="flat"
        color="primary"
        @click="router.push('/neverEndingWordle')"
        >Play Practice Wordle</v-btn
      >
    </div>
  </v-card>
</template>
<script setup lang="ts">
const router = useRouter();
</script>

<template>
  <v-container>
    <v-card class="text center">
      <v-card-title class="text center">Hello, Welcome to Pentagram</v-card-title>
      <v-card-subtitle class="text center"> This is our final project called Pentagram. Penta for five, and gram for letter
      </v-card-subtitle>
      <v-card-text class="text center">
        This website was built for a school project. we are using vuetify and nuxt to build this website.
        we are recreating a few of the games from: https://www.nytimes.com/games/wordle/index.html
      </v-card-text>
      <v-card-text class="text center">One of our instructors is Meghan Woodford, 
        She has been very helpful in teaching the basics and giving us the knowledge to build this website.
        I have never built a website before, so this is all new to me.
      </v-card-text>
      <v-btn color ="primary" @click="router.push('/')">Home</v-btn>
    </v-card>
  </v-container>
</template>

<script setup lang="ts">
const router = useRouter();
</script>
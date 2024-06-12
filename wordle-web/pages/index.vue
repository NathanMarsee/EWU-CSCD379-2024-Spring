<template>
  <v-card class="text-center">
    <v-card-title>Pentagram</v-card-title>
    <v-card-subtitle
      >This is our final project called Pentagram. Penta for five, and gram for
      letter</v-card-subtitle
    >
    <v-card-text
      >This is a clone of a couple of the New York Time word games. We wanted to
      make a version that lets you Practice and play as much as you want. We
      also have a leader bard if you want to see how you compare to others.
    </v-card-text>
    <div class="d-flex justify-center mt-3 mb-5">
      <v-btn
        variant="flat"
        color="primary"
        @click="router.push('/wordleOfTheDay')"
        >Play Wordle of the Day</v-btn
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

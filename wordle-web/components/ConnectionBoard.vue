<template>
  <div>
    <v-row dense class="justify-center">
      <v-col  cols="auto" v-for="(word, k) of guess.words" :key="k">
        <ConnectionWordBox :word="word" />
      </v-col>
    </v-row>
  </div>
</template>
<script setup lang="ts">
import { defineProps } from "vue";
import { Connection } from "~/scripts/connection";
import { ConnectionState } from "~/scripts/connection";

const props = defineProps<{
  guess: Connection;
}>();
</script>

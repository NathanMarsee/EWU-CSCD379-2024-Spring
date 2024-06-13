<template>
  <v-card
    :height="boxSize"
    :width="boxSize"
    :elevation="4"
    flat
    :color="boxColor"
    :class="['align-center d-flex justify-center']"
    @click="onClicked()"
  >
    {{ word }}
  </v-card>
</template>
<script setup lang="ts">
import { defineProps } from "vue";
import { useDisplay } from "vuetify";
import { ConnectionsGame } from "~/scripts/connectionsGame";

const props = withDefaults(
  defineProps<{
    word: string;
    clickable?: boolean;
    widthPercentOfHeight?: string;
  }>(),
  {
    clickable: false,
  }
);

const game: ConnectionsGame | undefined = inject("ConnectionGame", undefined);
const boxSize = ref(100);
const display = useDisplay();
const boxHeight = ref(60);
const boxWidth = ref(80);
const boxColor = ref("wrong");
updateWidth();

function onClicked() {
  if (!game) {
    return;
  }
  let added = false;
  if (boxColor.value === "wrong") {
    added = game.addGuess(props.word);
    if (added) {
      boxColor.value = "primary";
    } else {
      boxColor.value = "wrong";
    }
  } else {
    boxColor.value = "wrong";
    game.removeGuess(props.word);
  }
}

watch([display.sm, display.xs, display.md], () => {
  if (display.xs.value) {
    boxSize.value = 60;
  } else if (display.sm.value) {
    boxSize.value = 60;
  } else {
    boxSize.value = 60;
  }
  updateWidth();
});

function updateWidth() {
  if (props.widthPercentOfHeight !== undefined) {
    boxWidth.value = Math.floor(
      (parseInt(props.widthPercentOfHeight) / 100) * boxHeight.value
    );
  } else {
    boxWidth.value = boxHeight.value;
  }
}
</script>

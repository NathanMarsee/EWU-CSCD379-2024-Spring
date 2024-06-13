<template>
  <v-card
    :height="boxSize"
    :width="boxSize"
    :elevation="4"
    flat
    :boxColor="boxColor"
    :class="[correctState(word.state), 'align-center d-flex justify-center']"
    @click="onClicked()"
  >
    {{ word.word }}
  </v-card>
</template>
<script setup lang="ts">
import { defineProps } from "vue";
import { useDisplay } from "vuetify";
import { ConnectionsGame } from "~/scripts/connectionsGame";
import { ConnectionState, ConnectionsWord } from "~/scripts/connection";
const props = withDefaults(
  defineProps<{
    word: ConnectionsWord;
    clickable?: boolean;
    widthPercentOfHeight?: string;
  }>(),
  {
    clickable: false,
  }
);

const game: ConnectionsGame = inject("ConnectionGame") as ConnectionsGame;
const boxSize = ref(100);
const display = useDisplay();
const boxHeight = ref(60);
const boxWidth = ref(80);
const boxColor = ref("wrong");
updateWidth();

function correctState(connectionState: ConnectionState) {
  switch (connectionState) {
    case ConnectionState.Correct:
      return "correct-letter";
    case ConnectionState.Wrong:
      return "wrong-letter";
    case ConnectionState.Clicked:
      return "clicked-letter";
    default:
      return "unknown-letter";
  }
}
function onClicked() {
  if (!game) {
    return;
  }
  let added = false;
  if (props.word.state === ConnectionState.Unknown || props.word.state === ConnectionState.Wrong) {
    added = game.addGuess(props.word.word);
    if (added) {
      props.word.state = ConnectionState.Clicked;
    }
  } else if (props.word.state === ConnectionState.Clicked) {
    game.removeGuess(props.word.word);
    props.word.state = ConnectionState.Unknown;
  }
}

watch([display.sm, display.xs, display.md], () => {
  if (display.xs.value) {
    boxSize.value = 100;
  } else if (display.sm.value) {
    boxSize.value = 100;
  } else {
    boxSize.value = 100;
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
<style scoped>
.no-pointer {
  pointer-events: none;
}
.correct-letter {
  background: linear-gradient(
    to bottom,
    rgba(var(--v-theme-correct), 0.6),
    rgba(var(--v-theme-correct), 0.9)
  );
}
.wrong-letter {
  background: linear-gradient(
    to bottom,
    rgba(var(--v-theme-wrong), 0.6),
    rgba(var(--v-theme-wrong), 0.9)
  );
}

.unknown-letter {
  background: linear-gradient(
    to bottom,
    rgba(var(--v-theme-unknown), 0.6),
    rgba(var(--v-theme-unknown), 0.9)
  );
}
.clicked-letter {
  background: linear-gradient(
    to bottom,
    rgba(var(--v-theme-primary), 0.6),
    rgba(var(--v-theme-primary), 0.9)
  );
}
</style>

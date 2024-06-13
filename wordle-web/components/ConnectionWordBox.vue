<template>
  <v-card
    :height="boxHeight"
    :width="boxWidth"
    :elevation="4"
    flat
    :class="['align-center d-flex justify-center']"
    @click="onClicked()"
  >
    {{ word }}
  </v-card>
</template>
<script setup lang="ts">
import { Connection } from "~/scripts/connection";
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
const boxSize = ref(60);
const display = useDisplay();
const boxHeight = ref(60);
const boxWidth = ref(80);
updateWidth();

function onClicked() {
  console.log("Clicked");
}

watch([display.sm, display.xs, display.md], () => {
  if (display.xs.value) {
    boxHeight.value = 30;
  } else if (display.sm.value) {
    boxHeight.value = 40;
  } else {
    boxHeight.value = 60;
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
.misplaced-letter {
  background: linear-gradient(
    to bottom,
    rgba(var(--v-theme-misplaced), 0.6),
    rgba(var(--v-theme-misplaced), 0.9)
  );
}
.unkown-letter {
  background: linear-gradient(
    to bottom,
    rgba(var(--v-theme-unknown), 0.6),
    rgba(var(--v-theme-unknown), 0.9)
  );
}
</style>

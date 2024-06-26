<template>
  <NuxtLayout>
    <v-app>
      <v-app-bar color="primary">
        <v-app-bar-title>Pentagram</v-app-bar-title>
        <template v-slot:prepend>
          <v-btn icon @click="router.push('/')">
            <v-icon size="x-large">mdi-bullseye-arrow</v-icon>
          </v-btn>
        </template>
        <v-btn @click="showUserNameDialog = true">{{ userName }}</v-btn>
        <UserNameDialog v-model="showUserNameDialog" />
        <v-btn icon @click="showSettingsDialog = true">
          <v-icon>mdi-cog</v-icon>
        </v-btn>
        <v-menu>
          <template v-slot:activator="{ props }">
            <v-btn v-bind="props">
              Wordle
            </v-btn>
          </template>
          <v-list>
            <v-list-item v-for="page in wordlePages" :key="page.name" @click="router.push(page.path)">
              <v-list-item-title>{{ page.name }}</v-list-item-title>
            </v-list-item>
          </v-list>
        </v-menu>
        <v-menu>
          <template v-slot:activator="{ props }">
            <v-btn v-bind="props">
              connections
            </v-btn>
          </template>
          <v-list>
            <v-list-item v-for="page in connectionsPages" :key="page.name" @click="router.push(page.path)">
              <v-list-item-title>{{ page.name }}</v-list-item-title>
            </v-list-item>
          </v-list>
        </v-menu>
        <SettingsDialog v-model="showSettingsDialog" />
      </v-app-bar>
      <v-main>
        <NuxtPage />
      </v-main>
    </v-app>
  </NuxtLayout>
</template>



<script setup lang="ts">
import { useTheme } from "vuetify";
import nuxtStorage from "nuxt-storage";
const router = useRouter();
const theme = useTheme();
const showUserNameDialog = ref(false);
const showSettingsDialog = ref(false);
const userName = ref("guest");
var dark = ref(false);

provide("userName", userName);

const wordlePages = [
  { name: "Home", path: "/" },
  {name: "Practice Wordle", path: "/neverEndingWordle"},
  { name: "Wordle Of The Day", path: "/wordleOfTheDay" },
  { name: "Leaderboard", path: "/leaderboard" },
  { name: "Word Stats", path: "/wordStats" },
  { name: "Word List", path: "/wordList"},
  { name: "How To Play", path: "/wordleHowToPlay"},
  { name: "About", path: "/about" }
];

const connectionsPages = [
  {name: "Home", path: "/"},
  {name: "Practice Connections", path: "/connectionsPractice"},
  {name: "Connections" , path: "/connectionsOfTheDay"},
  {name: "How To Play", path: "/connectionsHowToPlay"},
]
onMounted(() => {
  var defaultTheme = nuxtStorage.localStorage.getData('theme');
  if (defaultTheme) { changeTheme(defaultTheme); }
  setDark();
  var userNameStored = nuxtStorage.localStorage.getData('userName');
  if (userNameStored && userNameStored !== "guest") {
    userName.value = userNameStored;
    showUserNameDialog.value = false;
  }
  

});
function changeTheme(themeName: string) {
  if (dark.value === false) {
    var themeToSet = themeName;
    if (themeToSet === "dark") {
      themeToSet = "light";
    } else {
      themeToSet = themeToSet.replace("Dark", "Light");
      console.log(themeToSet);
    }
    dark.value = false;
    theme.global.name.value = themeToSet;

  } else {
    theme.global.name.value = themeName;
    dark.value = true;
  }
  nuxtStorage.localStorage.setData('theme', theme.global.name.value);
}
function setDark() {
  if (theme.global.name.value.includes("Dark") || theme.global.name.value === "dark") {
    dark.value = true;
  } else {
    dark.value = false;
  }
}
</script>

<!--<style>
.v-main{
  background-image: url('/public/assets/Pentagram.jpg');
  background-size: cover;
  
  }
</style> -->
<script>
export default {
    name: 'GameItem',
    props: ['game', 'category', 'gameType']
}
</script>

<template>
  <div class="game-list-item">
      <div class="game-list-logo">
          <img :src="game.imageURL" alt="test"/>
          <span> {{ game.user.username }} </span>
      </div>
      <div class="game-list-content">
          <div class="game-list-header"> {{ game.name }} </div>
          <div class="game-list-information">
              <span class="tag" v-if="category"> {{ category.title }} </span>
              <span class="tag" v-if="gameType"> {{ gameType.name }} </span>
              <router-link :to="`/game/${game.id}`"> Show </router-link>
          </div>
      </div>
  </div>
</template>

<style>
.game-list-item {
    margin: 5px;
    background-color: #fbfffe;
    width: 260px;
    display: flex;
    flex-direction: column;
    padding: 10px;
}

.game-list-item .game-list-logo {
    width: 100%;
    display: flex;
    align-items: center;
    justify-content: space-between;
    color: #094074;
}

.game-list-item .game-list-logo img {
    width: 45px;
    border-radius: 100px;
} 

.game-list-content {
    display: flex;
    flex-direction: row;
    flex-wrap: wrap;
}

.game-list-content div {
    width: 100%;
}

.game-list-content .game-list-header {
    border-bottom: 1px solid #725ac1;
    color: #0a0908;
    font-size: 20px;
    font-weight: bold;
}

.game-list-content .game-list-information {
    display: flex;
    justify-content: space-around;
    padding-top: 10px;
}

</style>
<script>
export default {
    name: 'GameItem',
    props: {
        game: {
            type: Object,
            required: true
        }
    }
}
</script>

<template>
  <route-link class="game-list-item">
      <div class="game-list-logo">
          <img :src="game.imageURL" />
      </div>
      <div>
          {{ game.categoryId }} - {{ game.gameRootId }}
      </div>
  </route-link>
</template>

<style>
.game-list-item {
    width: 100px;
    height: 50px;
    display: flex;
    flex-direction: column;
}
</style>
<script>
import GameItem from '../shared/GameItem';
import { mapActions } from 'vuex';
export default {
    name: 'MyGames',
    components: {
        GameItem
    },
    methods: {
        ...mapActions([
            'fetchGamesByUserId',
            'fetchGameTypeById',
            'fetchCategoryById'
        ]),
    },
    data() {
        return {
            games: null,
        }
    },
    async created() {
        try {
            const {data} = await this.fetchGamesByUserId(this.$route.params.id);
            this.games = data;
        } catch(error) {
            this.$modal.show('dialog', { 
                title: "Error", 
                text: 'Something went wrong.'
            });
        }
    },
}
</script>

<template>
  <div class="game-grid" v-if="games">
      <game-item 
        v-for="game in this.games" 
        v-bind:key="game.id"
        :game="game"
        :category="game.category" 
        :gameType="game.gameRoot"/>
        
  </div>
  <div v-else>
      There are not any games that you posted.
      <v-dialog/>
  </div>
</template>

<style>

</style>
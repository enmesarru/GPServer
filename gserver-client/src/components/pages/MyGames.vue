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
    created() {
        try {
            const username = this.$route.params.id;
            this.fetchGamesByUserId(username).then(({data}) => {
                this.games = data;
            });
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
<div class="container">
    <div class="row">
        <router-link
            tag="button"
            class="full-button button button-black"
            :to="{name: 'create-game'}"
            >
            Create a game post
        </router-link>
    </div>
    <div class="row game-grid" v-if="games">
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
</div>
</template>

<style>

</style>
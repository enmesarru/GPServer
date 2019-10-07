<script>
import { mapGetters, mapActions } from 'vuex';
import GameItem from '../shared/GameItem';

export default {
    name: 'Home',
    components: {
        GameItem
    },
    methods: {
        ...mapActions(['fetchAllGames', 'fetchCategories', 'fetchGameTypes']),
    },
    computed: {
        ...mapGetters([
            'allGames',
            'categoryById',
            'gameTypeById',
        ]),
    },
    created() {
        this.fetchCategories();
        this.fetchGameTypes();
        this.fetchAllGames();
    },
}
</script>

<template>
    <div class="game-grid">
        <!-- Will be game list, here-->
            <game-item 
                v-for="game in allGames" 
                v-bind:key="game.id"
                :game="game" 
                :category="categoryById(game.categoryId)"
                :gameType="gameTypeById(game.gameRootId)" />
    </div>
</template>

<style>
.game-grid {
    display: flex;
    flex-direction: row;
    justify-content: space-evenly;
    flex-wrap: wrap;
}
</style>
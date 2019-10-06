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
    <div>
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

</style>
<script>
import { mapGetters, mapActions } from 'vuex';
import GameItem from '../shared/GameItem';

export default {
    name: 'Home',
    components: {
        GameItem
    },
    data() {
        return {
            filter: {
                category: null,
                gameroot: null
            }
        }
    },
    methods: {
        ...mapActions(['fetchAllGames', 'fetchCategories', 'fetchGameTypes']),
    },
    computed: {
        ...mapGetters([
            'categories',
            'gameTypes',
            'allGames',
            'filterGames',
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
<div class="container">
    <div class="row">
        <div class="column column-50">
            <label for="gamerootField"> Root of game </label>
            <v-select 
                :reduce="root => root.id"
                v-model="filter.gameroot"
                :options="this.gameTypes">
            </v-select>
        </div>

        <div class="column column-50">
            <label for="gamerootField"> Category </label>
            <v-select 
                :reduce="root => root.id"
                v-model="filter.category"
                :options="this.categories">
            </v-select>
        </div>
    </div>
    <div class="game-grid">
        <!-- Will be game list, here-->
        <game-item 
            v-for="game in filterGames(filter)" 
            v-bind:key="game.id"
            :game="game" 
            :category="categoryById(game.categoryId)"
            :gameType="gameTypeById(game.gameRootId)" />
    </div>
</div>
</template>

<style>
.inline-form {
    display: flex;
    align-items: center;
}
</style>
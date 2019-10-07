<script>
import { mapActions  } from 'vuex';
export default {
    name: 'Game',
    methods: {
        ...mapActions([
            'fetchGameById', 
            'fetchGameTypeById',
            'fetchCategoryById'
        ]),
    },
    data() {
        return {
            game: null,
            category: null,
            gameType: null,
        }
    },
    async created() {
        try {
            const {data} = await this.fetchGameById(this.$route.params.id);
            this.game = data;

            const gameType = await this.fetchGameTypeById(data.gameRootId);
            this.gameType = gameType.data;

            const category = await this.fetchCategoryById(data.categoryId);
            this.category = category.data;
        } catch(error) {
            this.$modal.show('dialog', { 
                title: "Error", 
                text: 'Something went wrong.'
            });
        }
    }
}
</script>

<template>
  <article class="container" v-if="game">
    <div class="row"> 
        <div class="column column-75">
            <h1>{{game.name}}</h1>
        </div>
        <div class="column column-25">
            <div class="game-header-info">
                <span> <strong> User: </strong> {{ game.user.username }}</span>              
                <img :src="game.imageURL" :alt="game.name" class="game-image">
            </div>
        </div>
    </div>
    <div class="row">
        <div class="column">
            <div class="game-tags">
                <span class="tag" v-if="category"> {{ category.title }}</span>
                <span class="tag" v-if="gameType"> {{ gameType.name }}</span>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="column" v-html="game.description"></div>
    </div>
    <v-dialog/>
  </article>
</template>

<style>
.game-image {
    width: 50px;
    height: 50px;
    border-radius: 100%;
}
.game-header-info {
    display: flex;
    justify-content: space-between;
    align-items: center;
}
.game-tags {
    display: flex;
    padding: 10px;
    flex-direction: row-reverse;
}
</style>
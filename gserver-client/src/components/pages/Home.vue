<script>
import { mapGetters, mapActions } from 'vuex';
import GameItem from '../shared/GameItem';
export default {
    name: 'Home',
    created() {
        this.fetchAllGames();
    },
    methods: {
        ...mapActions(['fetchAllGames'])
    },
    computed: {
        ...mapGetters(['allGames']),
    }
}
</script>

<template>
    <div>
        <!-- Will be game list, here-->
    </div>
</template>

<style>

</style>
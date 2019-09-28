<script>
import { mapActions, mapGetters } from 'vuex';
import { VueEditor } from 'vue2-editor';
import validationMixin from '../../mixins/validator';

export default {
    name: 'AddGame',
    mixins: [validationMixin],
    components: {
        VueEditor,
    },
    created() {
        this.fetchCategories();
        this.fetchGameTypes();
    },
    data() {
        return {
            formData: {
                name: '',
                link: '',
                description: '',
                imageURL: '',
                categoryId: null,
                gameRootId: null,
                userId: ''
            },
            rules: {
                name: { type: "string", required: true, message: 'Name cannot be empty' },
                link: { type: "string", required: true, message: 'Link cannot be empty' },
                description: { type: "string", required: true, message: 'Description cannot be empty' },
                imageURL: { type: "string", required: true, message: 'Image url cannot be empty' },
                categoryId: { type: "number", required: true, message: 'Category cannot be empty' },
                gameRootId: { type: "number", required: true, message: 'Root of game cannot be empty' },
            }
        }
    },
    methods: {
        ...mapActions(['fetchCategories', 'fetchGameTypes', 'createNewGame']),
        validate() {
            if(!this.validateForm()) {
                const messages = Object.values(this.errorMessages)
                    .map(message => `<li class="modal-error-message"> ${message} </li>`);
                const errorContent = `<ul> ${messages.join('')} </ul>`;
                this.$modal.show('dialog', { title: "Errors", text: errorContent});
            }
        },
        async add_new_game() {
            if(!this.isFail) {
                await this.createNewGame(this.formData)
                    .then(() => {
                        this.$modal.show('dialog', {
                            title: 'Information',
                            text: 'Your game has been created successfully.'
                        });
                        setTimeout(() => {
                            this.$router.push('/');
                        }, 1500);
                    }).catch(error => {
                        this.$modal.show('dialog', { 
                            title: "Error", 
                            text: 'Something went wrong.'
                        });
                    });
            }
        }
    },
    computed: {
        ...mapGetters(['categories', 'gameTypes'])
    }
}
</script>

<template>
  <form class="container" @submit.prevent="add_new_game">
      <fieldset>
        <div class="row">
            <div class="column">
                <label for="nameField">Name</label>
                <input type="text" v-model="formData.name" placeholder="Name" id="nameField" />
            </div>
            <div class="column">
                <label for="linkField">Webpage link</label>
                <input type="url" v-model="formData.link" placeholder="Webpage link" id="linkField" />
            </div>
        </div>

        <div class="row">
            <div class="column">
                <label for="descriptionField">Description</label>
                <vue-editor v-model="formData.description"> </vue-editor>
                </div>
        </div>
        
        <div class="row">
            <div class="column">
                <label for="imageURLField"> Server Image URL </label>
                <input type="url" v-model="formData.imageURL" placeholder="URL" id="imageURLField"/>
            </div>
        </div>

        <div class="row">
            <div class="column">
                <label for="categoryField"> Category </label>
                <v-select 
                    :reduce="category => category.id" 
                    v-model="formData.categoryId" 
                    :options="this.categories">
                </v-select>
            </div>

            <div class="column">
                <label for="gamerootField"> Root of game </label>
                <v-select 
                    :reduce="root => root.id" 
                    v-model="formData.gameRootId" 
                    :options="this.gameTypes">
                </v-select>
            </div>
        </div>

        <div class="row">
            <div class="column">
                <input @click="validate" class="button-blue full-button create-game-button" type="submit" value="Create" />
            </div>
        </div>
      </fieldset>
      <v-dialog name="game-create-form-errors" />
  </form>
</template>

<style>
.create-game-button {
    margin-top: 10px;
}
</style>
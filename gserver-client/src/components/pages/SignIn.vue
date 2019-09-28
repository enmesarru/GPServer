<script>
import validationMixin from '../../mixins/validator';

export default {
    name: 'SignIn',
    mixins: [validationMixin],
    props: ['isActive'],
    data() {
        return {
            formData: {
                username: '',
                password: '',
            },
            rules: {
                username: { type: "string", required: true, message: 'Username cannot be empty' },
                password: { type: "string", required: true, message: 'Password cannot be empty' },
            }
        }
    },
    methods: {
        loginValidate() {
            if(!this.validateForm()) {
                const messages = Object.values(this.errorMessages)
                    .map(message => `<li class="modal-error-message"> ${message} </li>`);
                const errorContent = `<ul> ${messages.join('')} </ul>`;
                this.$modal.show('dialog', { title: "Errors", text: errorContent});
            }
        },
        async login() {
            if(!this.isFail) {
                await this.$store
                    .dispatch("login", this.formData)
                    .then(() => this.$router.push("/"))
                    .catch(error => {
                        this.$modal.show('dialog', { 
                            title: "Information", 
                            text: 'Your username or password was wrong.'
                        });
                    });
            }
        },
    },
}
</script>

<template>
  <form v-if="isActive" @submit.prevent="login">
    <fieldset>
      <label for="usernameField">Username</label>
      <input type="text" v-model="formData.username" placeholder="Username" id="userNameField" />

      <label for="passwordField">Password</label>
      <input type="password" v-model="formData.password" placeholder="Password" id="passwordField" />

      <input @click="loginValidate" class="button-primary full-button" type="submit" value="Sign-in" />
    </fieldset>
  </form>
</template>

<style>
</style>
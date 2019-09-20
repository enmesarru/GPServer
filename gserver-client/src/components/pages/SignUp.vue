<script>
import validationMixin from '../../mixins/validator';

export default {
    name: 'SignUp',
    props: ['isActive'],
    mixins: [validationMixin],
    data() {
        return {
            formData: {
                email: '',
                password: '',
                username: ''
            },
            rules: {
                username: { type: "string", required: true, message: 'Username cannot be empty' },
                password: { type: "string", required: true, message: 'Password cannot be empty' },
                email: { type: "string", required: true, message: 'E-mail cannot be empty' },
            }
        };
    },
    methods: {
        signUpValidate() {
            if(!this.validateForm()) {
                const messages = Object.values(this.errorMessages)
                    .map(message => `<li class="modal-error-message"> ${message} </li>`);
                const errorContent = `<ul> ${messages.join('')} </ul>`;
                console.log(errorContent)
                this.$modal.show('dialog', { title: "Errors", text: errorContent});
            }
        },
        register() {
            if(!this.isFail) {
                this.$store
                    .dispatch("register", this.formData)
                    .then(() => this.$router.push("/"))
                    .catch(err => console.log(err));
            }
        }
    }
}
</script>

<template>
    <form v-if="!isActive">
        <fieldset>
            <label for="emailField">E-mail</label>
            <input type="email" v-model="formData.email" placeholder="E-mail" id="emailField">

            <label for="usernameField">Username</label>
            <input type="text" v-model="formData.username" placeholder="Username" id="userNameField">

            <label for="passwordField">Password</label>
            <input type="password" v-model="formData.password" placeholder="Password" id="passwordField">

            <input class="button-primary full-button" @click="signUpValidate" type="button" value="Sign-up">
        </fieldset>
    </form>
</template>

<style>

</style>
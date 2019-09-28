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
                this.$modal.show('dialog', { title: "Errors", text: errorContent});
            }
        },
        async register() {
            if(!this.isFail) {
                await this.$store
                    .dispatch("register", this.formData)
                    .then(() => {
                        this.$modal.show('dialog', { 
                            title: "Information", 
                            text: 'Registration has been done successfully. You will be redirected in seconds.'
                        });

                        setTimeout(() => {
                            this.$router.go({
                                path: '/',
                                force: true
                            });
                        }, 2500);
                    })
                    .catch(err => {
                        this.$modal.show('dialog', { 
                            title: "Error", 
                            text: 'Something went wrong.'
                        });
                    });
            }
        }
    }
}
</script>

<template>
    <form v-if="!isActive" @submit.prevent="register">
        <fieldset>
            <label for="emailField">E-mail</label>
            <input type="email" v-model="formData.email" placeholder="E-mail" id="emailField">

            <label for="usernameField">Username</label>
            <input type="text" v-model="formData.username" placeholder="Username" id="userNameField">

            <label for="passwordField">Password</label>
            <input type="password" v-model="formData.password" placeholder="Password" id="passwordField">

            <input class="button-primary full-button" @click="signUpValidate" type="submit" value="Sign-up">
        </fieldset>
    </form>
</template>

<style>

</style>
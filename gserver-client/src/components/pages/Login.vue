<script>
import SignIn from './SignIn';
import SignUp from './SignUp';

export default {
    name: 'Login',
    data() {
        return {
            signInForm: true,
        };
    },
    components: {
        SignIn,
        SignUp
    }
}
</script>

<template>
    <div class="center-login-container">
        <div class="login-box">
            <div class="login-box-item text-center">
                <h2>{{ signInForm ? 'Sign-in' : 'Sign-up' }}</h2>
            </div>
            <div class="login-box-item">
                <sign-in :isActive = "signInForm"/>
                <sign-up :isActive = "signInForm" />
            </div>
            <div class="login-box-item">
                <input class="button button-black full-button" 
                    @click="signInForm = false" 
                    v-if="signInForm" 
                    type="button" 
                    value="Sign-up">
            </div>

            <div class="login-box-item">
                <v-dialog name="login-form-errors" />
            </div>
        </div>
    </div>
</template>

<style>
.center-login-container {
    display: flex;
    justify-content: center;
    align-items: center;
    height: 100%;
}
.login-box {
    width: 400px;
    height: auto;
    padding: 20px;
    background-color: white;
    border-radius: 4px;
    display: flex;
    flex-direction: column;
}

.login-box .login-box-item {
    width: 100%;
}
</style>
<script>
export default {
    name: 'Login',
    data() {
        return {
            signInForm: true
        }
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
                <form v-if="signInForm">
                    <fieldset>
                        <label for="usernameField">Username</label>
                        <input type="text" placeholder="Username" id="userNameField">

                        <label for="passwordField">Password</label>
                        <input type="password" placeholder="Password" id="passwordField">

                        <input class="button-primary full-button" type="submit" value="Sign-in">
                    </fieldset>
                </form>

                <form v-else>
                    <fieldset>
                        <label for="emailField">E-mail</label>
                        <input type="email" placeholder="E-mail" id="emailField">

                        <label for="usernameField">Username</label>
                        <input type="text" placeholder="Username" id="userNameField">

                        <label for="passwordField">Password</label>
                        <input type="password" placeholder="Password" id="passwordField">

                        <input class="button-primary full-button" type="submit" value="Sign-up">
                    </fieldset>
                </form>
            </div>
            <div class="login-box-item">
                <input class="button button-black full-button" @click="signInForm = false" v-if="signInForm" type="button" value="Sign-up">
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
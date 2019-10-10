<script>
import { mapActions } from 'vuex';
import validationMixin from '../../mixins/validator';
export default {
    name: 'Profile',
    mixins: [validationMixin],
    data() {
      return {
        formData: {
          oldPassword: null,
          newPassword: null,
          reNewPassword: null
        },
        rules: {
          oldPassword: { type: "string", required: true, message: "Old password cannot be null!"},
          reNewPassword: { type: "string", required: true, message: "Re-new password cannot be null!"},
          newPassword: { 
            type: "string",
            required: true,
            validator: (rule, value, callback, source, options) => {
              if(!value) {
                return new Error("New password cannot be null");
              }
              if(value !== source.reNewPassword) {
                return new Error("New password and re-new password must be same value.");
              }
            }
          }
        }
    };
  },
  methods: {
    ...mapActions(['changePassword']),
    validate() {
      if(!this.validateForm()) {
        const messages = Object.values(this.errorMessages)
            .map(message => `<li class="modal-error-message"> ${message} </li>`);
        const errorContent = `<ul> ${messages.join('')} </ul>`;
        this.$modal.show('dialog', { title: "Errors", text: errorContent});
      }
    },
    async sendNewPassword() {
      if(!this.isFail) {
        try {
          const {oldPassword, newPassword} = this.formData;
          await this.changePassword({oldPassword, newPassword});

          this.$modal.show('dialog', { 
            title: "Information", 
            text: 'Changing of password has been done successfully. You will be redirected in seconds.'
          });

          setTimeout(() => {
            this.$router.go({ path: '/', force: true }); 
          }, 2500);
        } catch(error) {
          this.$modal.show('dialog', { 
            title: "Error", 
            text: 'Something went wrong'
          });
        }

      }
    }
  }
}
</script>

<template>
  <div class="container">
    <form @submit.prevent="sendNewPassword">
      <fieldset>
        <div class="row">
          <div class="column">
            <label for="oldPasswordField">Old Password</label>
            <input type="password" v-model="formData.oldPassword" placeholder="Old Password" id="oldPasswordField" />
          </div>
        </div>
        <div class="row">
          <div class="column">
            <label for="newPasswordField">New Password</label>
            <input type="password" v-model="formData.newPassword" placeholder="New Password" id="newPasswordField" />
          </div>
          <div class="column">
            <label for="reNewPasswordField">Re-new Password</label>
            <input 
              type="password" 
              v-model="formData.reNewPassword" 
              placeholder="New Password Again" 
              id="reNewPasswordField" />
          </div>
        </div>
                     
        <input 
          class="button-blue full-button create-game-button" 
          @click="validate"
          type="submit"
          value="Save"/>
      </fieldset>
      <v-dialog name="game-create-form-errors" />
    </form>
  </div>
</template>

<style>

</style>
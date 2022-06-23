<template>
  <v-container class="login_main_container">
    <v-form v-model="valid" class="login_form_container" ref="form">
      <v-container>
        <v-row>
          <v-col cols="12" md="4">
            <v-text-field
              v-model="email"
              :rules="emailRules"
              label="E-mail"
              required
            ></v-text-field>
          </v-col>
          <v-col cols="12" md="4">
            <v-text-field
              v-model="password"
              :rules="passwordRules"
              label="Password"
              type="password"
              required
            ></v-text-field>
          </v-col>
        </v-row>
        <v-btn
          class="login_form_loginbtn"
          @click="handleLoginClick"
          :disabled="!valid"
          >Log in</v-btn
        >
      </v-container>
    </v-form>
  </v-container>
</template>

<script>
import axios from "axios";

export default {
  data: () => ({
    valid: false,
    password: "",
    passwordRules: [
      (v) => !!v || "Password is required",
      (v) => v.length >= 8 || "Password must have a mininum of 8 characters",
    ],
    email: "",
    emailRules: [
      (v) => !!v || "E-mail is required",
      (v) => /.+@.+/.test(v) || "E-mail must be valid",
    ],
  }),
  methods: {
    handleLoginClick() {
      this.validate();
      axios.post(`${process.env.VUE_APP_API_URL}/api/User/authenticate`, {
        email: this.email,
        password: this.password,
      });
    },
    validate() {
      this.$refs.form.validate();
    },
    reset() {
      this.$refs.form.reset();
    },
    resetValidation() {
      this.$refs.form.resetValidation();
    },
  },
};
</script>

<style lang="scss">
.login_main_container {
  height: 100%;
  display: flex;
  align-content: center;
  justify-content: center;
  .login_form_container {
    width: 50%;
    margin: auto;
    .login_form_loginbtn {
      width: 100%;
    }
  }
}
</style>
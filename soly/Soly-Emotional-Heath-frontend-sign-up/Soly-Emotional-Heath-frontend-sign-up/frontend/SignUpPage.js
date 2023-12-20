import React, { useState, useCallback } from 'react';
import { View, TextInput, TouchableOpacity, Text, StyleSheet } from 'react-native';
import * as Yup from 'yup';

export default function SignUpPage() {
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [validEmail, setValidEmail] = useState(false);

  const checkEmail = (email) => {
    const emailValidationSchema = Yup.object().shape({
      email: Yup
        .string()
        .required('Email is required')
        .matches(
          /^[a-zA-Z0-9._-]{6,}@[a-zA-Z.-]+\.[a-zA-Z]{2,}$/,
          'Invalid email format'
        ),
    });

  };
  try {
    emailValidationSchema.validate({ email });
    return true; 
  } catch (validationError) {
    return false; 
  }
};

const checkPassword = (password) => {
  // Define your password validation criteria here
  const minLength = 8;
  const hasUpperCase = /[A-Z]/.test(password);
  const hasLowerCase = /[a-z]/.test(password);
  const hasDigit = /\d/.test(password);

  if (password.length >= minLength && hasUpperCase && hasLowerCase && hasDigit) {
    return true;
  } else {
    alert("change password")
    return false
  }
}

const onSubmitEmail = useCallback(() => {
  if (!checkEmail(email)) {
    return;
  } else {
    // Set the email as valid
    setValidEmail(true);
    console.log(email);
  }
}, [email]);

const onSubmitPassword = useCallback(() => {
  if (!checkPassword(password)) {
    return;
  } else {
    console.log(password);
    //create user and send to server🥳
    const user = {
      email: email,
      password: password
    }
  }
}, [password]);

return (
  <View style={styles.container}>
    <View style={styles.content}>
      <Text style={styles.createAccountTitle}>Create your account</Text>
      <Text style={styles.noteText}>
        Please note that phone verification is required for signup. Your number will only be used to verify your identity for security purposes.
      </Text>
    </View>
    {!validEmail && (
      <TextInput
        style={styles.input}
        autoCompleteType="email"
        keyboardType="email-address"
        textContentType="emailAddress"
        placeholder="Email address"
        placeholderTextColor="#ffffff80" // Light white placeholder text
        value={email}
        onChangeText={setEmail}
      />
    )}

    {validEmail && (
      <TextInput
        style={styles.input}
        secureTextEntry // To hide the password
        autoCompleteType="password"
        keyboardType="default"
        textContentType="newPassword"
        placeholder="Password"
        placeholderTextColor="#ffffff80" // Light white placeholder text
        value={password}
        onChangeText={setPassword}
      />
    )}

    {!validEmail ? (
      <TouchableOpacity style={styles.button} onPress={onSubmitEmail}>
        <Text style={styles.buttonText}>Continue</Text>
      </TouchableOpacity>
    ) : (
      <TouchableOpacity style={styles.button} onPress={onSubmitPassword}>
        <Text style={styles.buttonText}>Sign Up!</Text>
      </TouchableOpacity>
    )}
    <br></br>
    <div style={styles.loginDiv}>
      Already have an account? <a style={styles.loginLink} href="./Login">Log in!</a>
    </div>
    <br></br>

    <div style={styles.loginDiv}>-------------- OR --------------</div>

    {/*Load Authentication component*/}
  </View>
);




//--- Style
const styles = StyleSheet.create({
  loginDiv: { textAlign: 'center', color: 'white' },
  loginLink: { color: 'green' },
  container: {
    backgroundColor: '#333333', // Dark gray background for the page
    flex: 1,
    justifyContent: 'center',
    padding: 16,
  },
  content: {
    alignItems: 'center',
  },
  createAccountTitle: {
    color: 'white',
    fontSize: 18,
    fontWeight: 'bold',
    marginBottom: 8,
  },
  noteText: {
    maxWidth: 300,
    textAlign: 'center',
    color: 'white',
  },
  input: {
    height: 40,
    borderColor: 'white', // White outline for the input
    borderWidth: 1,
    backgroundColor: '#333333', // Dark gray background for the input
    borderRadius: 5,
    paddingHorizontal: 10,
    marginVertical: 8,
    color: 'white', // White text for the input
    width: '100%',
  },
  button: {
    backgroundColor: '#4CAF50', // Green background for the button
    padding: 10,
    borderRadius: 5,
    justifyContent: 'center',
    alignItems: 'center',
    marginTop: 10,
  },
  buttonText: {
    color: 'white', // White text for the button
    fontSize: 16,
  },
});

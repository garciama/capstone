import React, { Component } from 'react';
import { Button, Text, TextInput, View } from 'react-native';

export default class loginView extends Component {
  constructor(props) {
    super(props);
    this.state = {text: ''};
  }

  render() {
    return (
     <View style={{
      backgroundColor: 'black',
      flex: 1,
      flexDirection: 'column',
      justifyContent: 'center',
      padding: '5%',
      
      //alignItems: 'stretch',
    }}>
        <TextInput
          style={{height: 40, fontSize: 20, backgroundColor: 'gray', color:'white'}}
          placeholder="Email"
          placeholderTextColor='white'
          onChangeText={(text) => this.setState({text})}
          emailInput={this.state.text}
          
        />
        <View style={{flex:0.025}}/>
        <TextInput
          style={{height: 40, fontSize: 20, backgroundColor: 'gray', color:'white'}}
          placeholder="Password"
          placeholderTextColor='white'
          onChangeText={(text) => this.setState({text})}
          passwordInput={this.state.text}
          secureTextEntry
        />
        <View style={{flex:0.025}}/>
      
      <Button
      onPress={() => {
        alert('ayy you logged in');
      }}
      title="Login"
      />
      <View style={{flex:0.025}}/>
       <Button
      onPress={() => {
        alert('need to go to sign up page now');
      }}
      title="Signup"
      />
    </View>
    );
  }
}
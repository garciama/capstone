import React, {Component} from 'react';
import { Text, StyleSheet, TextInput, View , TouchableOpacity, KeyboardAvoidingView, Image} from 'react-native';
import RNFetchBlob from 'rn-fetch-blob'

export default class loginView extends Component {
    constructor(props) {
      super(props);
      this.state = {text: ''};
    }
    render() {
      const {navigate} = this.props.navigation;
  
      return (
      <KeyboardAvoidingView keyboardVerticalOffset = '-600' style = {{flex: 1, backgroundColor: '#181818'}} behavior="height" >

        <View style={{padding: '5%', flex: 1, justifyContent: 'flex-end'}}>

          <View style={{flex: .5, alignItems:'center', justifyContent: 'center'}}>
            <Image
              style={{flex: 1, resizeMode: 'stretch', width: '100%'}}   
              source={require('./img/saucer.png')} />    
          </View>
          <View style={{flex:0.025}}/>  
  
            <TextInput
              style={styles.loginBoxes}
              placeholder="Email"
              placeholderTextColor='white'
              onChangeText={(text) => this.setState({text})}
              emailInput={this.state.text}
              keyboardType={"email-address"}
              returnKeyType = { "next" }
              onSubmitEditing={() => { this.Password.focus(); }}
              blurOnSubmit={false}
            />
  
            <View style={{flex:0.025}}/>
            <TextInput
              style={styles.loginBoxes}
              placeholder="Password"
              placeholderTextColor='white'
              onChangeText={(text) => this.setState({text})}
              passwordInput={this.state.text}
              secureTextEntry
              ref={(input) => { this.Password = input; }}
  
              // check to make sure login is valid later before navigating
              onSubmitEditing={() => { navigate('Menu') }} 
            />
        </View>
        
  
        <View style={styles.bottom}>
  
          <TouchableOpacity style={styles.loginButtons} onPress={() => {
            // RNFetchBlob.config({
            //     trusty: true
            // }).fetch( 'POST', 'https:10.0.2.2:5001/Login', 
            //   { 'Content-Type': 'application/json'}, 
            //   JSON.stringify({ 
            //     email: 'yuhh@email.com',
            //     password : "suhh"
            //   }))
            //   .then((response) => response.text()) 
            //   .then((responseJson) => {
            //     alert(responseJson); 
            //   })
            // .catch((error) => {
            //   console.error(error);
            // }),
            navigate('Menu')
          }}>
            <Text style={styles.loginButtonText}>Login</Text>
          </TouchableOpacity>
  
          <TouchableOpacity style={styles.loginButtons} onPress={() => navigate('Signup')}>
            <Text style={styles.loginButtonText}>Signup</Text>
          </TouchableOpacity>         
        </View>

        <View style={{justifyContent:'center', alignItems:'center', alignSelf:'center',position: 'absolute', top: '5%'}}>
          <Text style={{color:'white', fontSize: 30}}>Cunning Coders' Cafe</Text>
        </View>

       </KeyboardAvoidingView>
      );
    }
  }
  
  const styles = StyleSheet.create({
      bottom: {
        flex: 0.27,
        justifyContent: 'flex-end',
        marginBottom: 0,
      },
      loginBoxes: {
        flex: 0.09,
        fontSize: 18,
        backgroundColor: '#303030',
        color:'white',
        borderRadius: 6,
        justifyContent: 'center',
        alignItems: 'center'
      },
      loginButtons: {
        flex: 1,
        justifyContent: "center",
        backgroundColor: '#fbba37',
        alignItems: 'center',
        fontSize: 30,
        borderWidth: 1,
        borderColor: '#404040',
        borderRadius: 6,
      },
      loginButtonText: {
        color: 'white',
        fontSize: 29
      },
  });
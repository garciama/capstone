import React, {Component} from 'react';
import { Button, Text, StyleSheet, TextInput, View , TouchableOpacity, KeyboardAvoidingView, Platform} from 'react-native';
import FooterView from './Footer.js';


class menuView extends Component {
  constructor(props) {
    super(props);
    this.state = {text: ''};
  }
  render() {
    const {navigate} = this.props.navigation;

    return (
        <View style={{flex: 1, backgroundColor: 'blue'}}>
            <View style={{flex: 1, backgroundColor: 'green'}}>
                <Text>Menu</Text>
            </View>

            <View style={{flex: 1, justifyContent: 'flex-end'}}>

            <FooterView/>

            </View>

        </View>

    );
  }
}

const styles = StyleSheet.create({

})

module.exports = menuView
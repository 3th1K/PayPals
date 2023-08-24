import React, { useEffect, useState } from 'react';
import { View, Text, StyleSheet } from 'react-native';

import { CreateLogger } from '../Logger';

const log = CreateLogger("HomeScreen");


const HomeScreen = () => {

  return (
    <View style={styles.container}>
      <Text style={styles.welcome}>Welcome</Text>
    </View>
  );

}

export default HomeScreen

const styles = StyleSheet.create({
  container: {
    flex: 1,
    alignItems: 'center',
    justifyContent: 'center',
  },
  welcome: {
    fontSize: 18,
    fontWeight: 'bold',
    marginBottom: 10,
  },
});
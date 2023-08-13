import { StyleSheet, Text, View } from 'react-native'
import React from 'react'
import { RemoveToken } from '../TokenHandler'
import { CreateLogger, GetToken } from '../Logger'
const log = CreateLogger("ProfileScreen");

const ProfileScreen = () => {
    log.info("Removing Token");
    RemoveToken();
    log.info("Removed Token");
  return (
    <View>
      <Text>ProfileScreen</Text>
    </View>
  )
}

export default ProfileScreen

const styles = StyleSheet.create({})
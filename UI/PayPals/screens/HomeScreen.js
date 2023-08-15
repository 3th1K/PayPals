import { StyleSheet, Text, View } from 'react-native'
import React from 'react'
import { GetUsers, GetUser } from '../services/UserService'
import { CreateLogger } from '../Logger'
import { useState, useEffect } from 'react'

const log = CreateLogger("HomeScreen");

const HomeScreen = () => {
  const [currentUser, setCurrentUser] = useState(null);

  useEffect(() => {
    const fetchUser = async () => {
      try {
        const user = await GetUser();
        setCurrentUser(user);
      } catch (error) {
        log.error(error);
      }
    };

    fetchUser();
  }, []);
  return (
    <View>
      <Text>Welcome {currentUser ? currentUser.username : 'Loading...'}</Text>
    </View>
  )
}

export default HomeScreen

const styles = StyleSheet.create({})
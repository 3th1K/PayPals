import React, { useEffect, useState } from 'react';
import { View, Text, StyleSheet } from 'react-native';
import { useUser } from '../helpers/UserContext';
import { GetUser } from '../services/UserService';
import { CreateLogger } from '../Logger';
const log = CreateLogger("HomeScreen");


const HomeScreen = () => {
  const { userId } = useUser();
  log.info("userid : "+userId);
  const [userData, setUserData] = useState(null);

  useEffect(() => {
    const fetchUserData = async () => {
      try {
        const user = await GetUser(userId);
        setUserData(user);
      } 
      catch (error) {
        console.error('Error fetching user data:', error);
      }
    };

    if (userId) {
      fetchUserData();
    }
  }, []);
  
  if (!userId) {
    return (
      <View style={styles.container}>
        <Text>Loading...</Text>
      </View>
    );
  }

  return (
    <View style={styles.container}>
      <Text style={styles.welcome}>Welcome {userData ? userData.username : 'Loading...'}</Text>
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
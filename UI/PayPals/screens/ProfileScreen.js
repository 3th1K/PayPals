import React, { useEffect, useState } from 'react';
import { View, Text, StyleSheet, Button } from 'react-native';
import { useUser } from '../helpers/UserContext';
import { GetUser } from '../services/UserService';
import { useNavigation } from '@react-navigation/native';
import { CreateLogger } from '../Logger';
import { RemoveToken } from '../TokenHandler';
const log = CreateLogger("ProfileScreen");

const ProfileScreen = () => {
  const navigation = useNavigation();
  const { userId, setUserId } = useUser();
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
  }, [userId]);

  const handleLogout = async () => {
    try {
      // Clear the token and user ID from AsyncStorage
      await RemoveToken();
      
      navigation.navigate("Login");
    } catch (error) {
      log.error('Error logging out:', error);
    }
  };

  return (
    <View style={styles.container}>
      {userData ? (
        <>
          <Text style={styles.username}>{userData.username}</Text>
          <Text>Email: {userData.email}</Text>
          {/* Display other user details */}
          <Button title="Logout" onPress={handleLogout} />
        </>
      ) : (
        <Text>Loading ...</Text>
      )}
    </View>
  );
};

const styles = StyleSheet.create({
  container: {
    flex: 1,
    alignItems: 'center',
    justifyContent: 'center',
  },
  username: {
    fontSize: 18,
    fontWeight: 'bold',
    marginBottom: 10,
  },
});

export default ProfileScreen;

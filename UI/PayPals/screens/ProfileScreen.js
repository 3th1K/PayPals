import React, { useEffect, useState } from 'react';
import { View, Text, StyleSheet, Button } from 'react-native';
import { useNavigation } from '@react-navigation/native';
import { CreateLogger } from '../Logger';
import { RemoveToken } from '../TokenHandler';
import { useSelector } from 'react-redux';
const log = CreateLogger("ProfileScreen");

const ProfileScreen = () => {
  const navigation = useNavigation();
  const [userData, setUserData] = useState(null);
  const userState = useSelector((state) => state.user);



  useEffect(() => {
    const user = userState.user;
    log.warn(user);
    setUserData(user);
    log.success(userData);
  })
  const handleLogout = async () => {
    log.info("Logging out");
    try {
      await RemoveToken();
      navigation.navigate("Login");
      log.success("Logged out");
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
        <>
          <Text>Loading ...</Text>
          <Button title="Logout" onPress={handleLogout} />
        </>

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

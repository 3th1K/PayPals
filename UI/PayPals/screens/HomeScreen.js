import React, { useEffect, useState } from 'react';
import { View, Text, StyleSheet } from 'react-native';
import { useSelector } from 'react-redux';
import { CreateLogger } from '../Logger';
import { SafeAreaView } from 'react-native-safe-area-context';
import LoadingOverlay from '../helpers/LoadingOverlay';

const log = CreateLogger("HomeScreen");


const HomeScreen = () => {
  const [userData, setUserData] = useState('');
  const [isLoading, setIsLoading] = useState(true);
  const userState = useSelector((state) => state.user);
  
  useEffect(() => {
    if(useState.error){

    }
    const user = userState.user;
    log.warn(user);
    setUserData(user);
    log.success(userData);
    setIsLoading(!userData);
  })

  return (
    <SafeAreaView style={styles.container}>
      <View style={styles.headerContainer}>
        {userData && (
          <Text style={styles.welcome}>Welcome {userData.username}</Text>
        )}
      </View>
      <View style={styles.contentContainer}>
        {/* Replace the following with your actual list content */}
        <Text>List of Something</Text>
        {/* Other list content */}
      </View>
      <LoadingOverlay isVisible={isLoading} />
    </SafeAreaView>
    
  );

}

export default HomeScreen

const styles = StyleSheet.create({
  container: {
    flex: 1,
    alignItems: 'center',
    justifyContent: 'center',
    backgroundColor: '#f0f0f0', // Change this to your desired background color
    borderRadius: 20, // Add rounded corners
    margin: 10, // Add margin to make it a little smaller than the screen
    overflow: 'hidden', // Hide overflow content
  },
  headerContainer: {
    alignItems: 'flex-end', // Align to the top-right corner
    padding: 10,
  },
  welcome: {
    fontSize: 18,
    fontWeight: 'bold',
  },
  contentContainer: {
    flex: 1,
    justifyContent: 'center',
    alignItems: 'center',
    padding: 20,
  },
});
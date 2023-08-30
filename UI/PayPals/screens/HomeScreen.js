import React, { useEffect, useState } from 'react';
import { View, Text, StyleSheet, FlatList, TouchableOpacity } from 'react-native';
import { useSelector } from 'react-redux';
import { CreateLogger } from '../Logger';
import { SafeAreaView } from 'react-native-safe-area-context';
import { useNavigation } from '@react-navigation/native';
import LoadingOverlay from '../helpers/LoadingOverlay';

const log = CreateLogger("HomeScreen");


const HomeScreen = () => {
  const [userData, setUserData] = useState('');
  const [isLoading, setIsLoading] = useState(true);
  const userState = useSelector((state) => state.user);
  const navigation = useNavigation();
  
  useEffect(() => {
    if(userState.error){
      log.error(userState)
    }
    const user = userState.user;
    setUserData(user);
    setIsLoading(!userData);
  })

  // const handleGroupPress = (group) =>{
  //   log.info(`${group.groupName} is pressed`);
  //   navigation.navigate("Group");
  // }

  const renderGroupItem = ({ item }) => {
    return (
      <TouchableOpacity onPress={() => navigation.navigate("Group", {groupId: item.groupId})}>
        <View style={styles.groupContainer}>
          <View style={styles.groupHeaderContainer}>
            <Text style={styles.groupHeader}>{item.groupName}</Text>
          </View>
          <View style={styles.groupDetails}>
            <Text style={styles.groupDetailText}>
              Created by: {item.creator.username}
            </Text>
            <Text style={styles.groupDetailText}>
              Total Expenses: {item.totalExpenses}
            </Text>
          </View>
        </View>
      </TouchableOpacity>
    ); // Adjust this based on your group object structure
  };

  return (
    <SafeAreaView style={styles.container}>
      <View style={styles.headerContainer}>
        {userData && (
          <Text style={styles.welcome}>Welcome {userData.username}</Text>
        )}
      </View>
      <View style={styles.contentContainer}>
        {isLoading ? (
          <LoadingOverlay isVisible={isLoading} />
        ) : (
          <FlatList
            data={userData.groups}
            renderItem={renderGroupItem}
            keyExtractor={(item) => item.groupId.toString()} // Assuming each group has a unique id
          />
        )}
      </View>
    </SafeAreaView>
    
  );

}

export default HomeScreen

const styles = StyleSheet.create({
  container: {
    flex: 1,
    //alignItems: 'center',
    justifyContent: 'center',
    padding: 20,
    backgroundColor: 'black', // Change this to your desired background color
    //borderRadius: 20, // Add rounded corners
    //margin: 10, // Add margin to make it a little smaller than the screen
    overflow: 'hidden', // Hide overflow content
    width: '100%'
  },
  headerContainer: {
    alignItems: 'flex-start',
    paddingVertical: 20,
    paddingHorizontal: 30,
  },
  welcome: {
    fontSize: 20,
    fontWeight: 'bold',
    color: 'white',
  },
  contentContainer: {
    flex: 1,
    paddingTop: 20,
    paddingHorizontal: 20,
  },
  groupContainer: {
    backgroundColor: '#f9f9f9',
    borderRadius: 10,
    marginBottom: 15,
    padding: 15,
    shadowColor: '#000',
    shadowOffset: { width: 0, height: 2 },
    shadowOpacity: 0.1,
    shadowRadius: 3,
    elevation: 3,
  },
  groupHeaderContainer: {
    backgroundColor: '#333',
    borderTopLeftRadius: 10,
    borderTopRightRadius: 10,
    borderBottomLeftRadius: 10,
    borderBottomRightRadius: 10,
    paddingHorizontal: 15,
    paddingVertical: 10,
  },
  groupHeader: {
    fontSize: 16,
    fontWeight: 'bold',
    color: '#fff',
  },
  groupDetails: {
    marginTop: 10,
  },
  groupDetailText: {
    fontSize: 14,
    color: '#666',
  },
});
import React, { useEffect, useState } from 'react';
import { View, Text, TouchableOpacity, FlatList, StyleSheet, SafeAreaView } from 'react-native';
import { GetGroup } from '../services/GroupService';
import { useNavigation } from '@react-navigation/native';
import { AntDesign } from '@expo/vector-icons';
import { CreateLogger } from '../Logger';
import { GetError } from '../helpers/ErrorHandler';
const log = CreateLogger("GroupScreen");
const GroupScreen = ({ route }) => {
    const [group, setGroup] = useState(null);
    const [isLoading, setIsLoading] = useState(true);
    const navigator = useNavigation();

    useEffect(() => {
        async function getGroup() {
            try {
                const groupId = route.params.groupId;
                const group = await GetGroup(groupId);
                setGroup(group);
                setIsLoading(false);
            }
            catch (error) {
                log.warn(error.code);
                const errordata = GetError(error);
                log.error(JSON.stringify(errordata));
            }
        }
        getGroup();
    }, [])

    const renderItem = ({ item }) => (
        <View style={styles.userContainer}>
            <View style={styles.userNameContainer}>
                <Text style={styles.userName}>{item.username}</Text>
            </View>
            <View style={styles.userDetails}>
                <Text style={styles.groupDetailText}>
                   Total Expense : {item.totalExpense}
                </Text>
            </View>
        </View>
    );

    if (!isLoading) {
        return (
            <SafeAreaView style={styles.container}>
                <View style={styles.headerContainer}>
                    <TouchableOpacity style={styles.backArrow} onPress={() => navigator.goBack()}>
                        <AntDesign name="arrowleft" size={20} color="black"/>
                    </TouchableOpacity>
                    <Text style={styles.groupName}>{group.groupName}</Text>
                    <Text>Total expense: {group.groupId}</Text>
                </View>
                <View style={styles.contentContainer}>
                    <FlatList
                        data={group.users}
                        renderItem={renderItem}
                        keyExtractor={(item) => item.userId.toString()} // Assuming each user has a unique id
                    />
                </View>

            </SafeAreaView>
            
        );
    } else {
        // Render loading indicator or placeholder here
        return <Text>Loading...</Text>;
    }
};


const styles = StyleSheet.create({
    container: {
        flex: 1,
        //alignItems: 'center',
        //justifyContent: 'center',
        paddingTop: 20,
        backgroundColor: 'red', // Change this to your desired background color
        //borderRadius: 20, // Add rounded corners
        //margin: 10, // Add margin to make it a little smaller than the screen
        overflow: 'hidden', // Hide overflow content
        width: '100%'
    },
    headerContainer: {
        //flex: 1,
        flexDirection: 'row',
        height: 50,
        alignItems: 'center',
        justifyContent: 'space-between',
        //paddingTop: 40,
        margin: 20,
        backgroundColor: 'yellow'
    },
    backArrow: {
        //flex: 1,
        //flexDirection: 'row',
        justifyContent: 'center',
        padding: 10,
        //alignItems: 'start',
        backgroundColor: '#f0f0f0', // Header background color
    },
    groupName: {
        justifyContent: 'space-between',
        fontWeight: 'bold',
        fontSize: 18,
    },
    totalExpense: {
        //justifyContent: 'space-between',
        fontWeight: 'bold',
        fontSize: 18,
    },
    contentContainer: {
        flex: .8,
        paddingTop: 20,
        paddingHorizontal: 20,
        backgroundColor: 'green'
      },
    userContainer: {
        backgroundColor: '#f9f9f9',
        borderRadius: 10,
        margin: 20,
        padding: 15,
        shadowColor: '#000',
        shadowOffset: { width: 0, height: 2 },
        shadowOpacity: 0.1,
        shadowRadius: 3,
        elevation: 3,
      },
    userNameContainer: {
        backgroundColor: '#333',
        borderTopLeftRadius: 10,
        borderTopRightRadius: 10,
        borderBottomLeftRadius: 10,
        borderBottomRightRadius: 10,
        paddingHorizontal: 15,
        paddingVertical: 10,
      },
    userName: {
        fontSize: 16,
        fontWeight: 'bold',
        color: '#fff',
      },
    userDetails: {
        marginTop: 10,
      },
    userDetailText: {
        fontSize: 14,
        color: '#666',
      },
});



export default GroupScreen;

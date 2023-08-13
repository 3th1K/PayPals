import { createBottomTabNavigator } from "@react-navigation/bottom-tabs";
import HomeScreen from "./screens/HomeScreen";
import ProfileScreen from "./screens/ProfileScreen";
import { AntDesign } from '@expo/vector-icons';
import { Entypo } from '@expo/vector-icons'; 
import { Ionicons } from '@expo/vector-icons';
import { createNativeStackNavigator } from "@react-navigation/native-stack";
import { NavigationContainer } from "@react-navigation/native";
import LoginScreen from "./screens/LoginScreen";
import RegisterScreen from "./screens/RegisterScreen";
import { CheckToken, GetToken } from './TokenHandler';
import React, { useEffect, useState } from 'react';
import { CreateLogger } from "./Logger";

const log = CreateLogger("StackNavigator");
const Tab = createBottomTabNavigator();

function BottomTabs() {
  return (
    <Tab.Navigator>
      <Tab.Screen name="Home" component={HomeScreen} options={{
        tabBarLabel:"Home",
        headerShown:false,
        tabBarLabelStyle:{color:'black'},
        tabBarIcon:({focused}) => focused? <Entypo name="home" size={24} color="black" /> : <AntDesign name="home" size={24} color="black" />,
        }}/>
      <Tab.Screen name="Profile" component={ProfileScreen} options={{
        tabBarLabel:"Profile",
        headerShown:false,
        tabBarLabelStyle:{color:'black'},
        tabBarIcon:({focused}) => focused? <Ionicons name="person" size={24} color="black" /> : <Ionicons name="person-outline" size={24} color="black" />,
      }} />
    </Tab.Navigator>
  );
}

const Stack = createNativeStackNavigator();

function Navigation(){
  const [token, setToken] = useState(null);
  useEffect(() => {
    async function x(){
      log.info("Checking if user already logged in");
      const tokenPresent = await CheckToken();
      if(tokenPresent){
        log.success("User is already logged in");
        setToken(await GetToken());
      }
    }
    x();
  }, []);
    return(
        <NavigationContainer>
            <Stack.Navigator>
            {token ? (
          <>
            <Stack.Screen name="Main" component={BottomTabs} options={{ headerShown: false }} />
          </>
        ) : (
          <>
            <Stack.Screen name="Login" component={LoginScreen} options={{ headerShown: false }} />
            <Stack.Screen name="Register" component={RegisterScreen} options={{ headerShown: false }} />
            <Stack.Screen name="Main" component={BottomTabs} options={{ headerShown: false }} />
          </>
        )}
            </Stack.Navigator>
        </NavigationContainer>
    )
}

export default Navigation
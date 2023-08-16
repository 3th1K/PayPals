import React, { createContext, useContext, useState, useEffect } from 'react';
import AsyncStorage from '@react-native-async-storage/async-storage'; // Use AsyncStorage library
import jwtDecode from 'jwt-decode'; // Import jwt-decode library (make sure it's installed)
import { CheckToken, GetToken } from '../TokenHandler';
import { CreateLogger } from '../Logger';
const log = CreateLogger("UserContext");

// Create a context
const UserContext = createContext();

// Create a provider component
export const UserProvider = ({ children }) => {
  log.info("User provider running ...");
  const [userId, setUserId] = useState(null);
  const [isLoading, setIsLoading] = useState(true);

  // Load user ID from AsyncStorage on app startup
  useEffect(() => {
    const loadUserId = async () => {
        if(await CheckToken()){
            log.info("Token is present");
            try {
                const token = await AsyncStorage.getItem('token'); // Replace with your token key
                // Decode JWT token and extract user ID, then set it
                if (token) {
                    const decodedToken = jwtDecode(token);
                    if (decodedToken && decodedToken.userId) {
                        setUserId(decodedToken.userId);
                    }
                }
            } 
            catch (error) {
                log.error('Error loading user ID:', error);
            }
        }
        else{
            log.info("Token is not present, User is not logged in");
            log.info("Please Log in First");
        }
        setIsLoading(false);
    };
    log.info("User provider useeffect running");
    loadUserId();
  }, []);

  // Provide the user ID and setUserId to the components
  return (
    <UserContext.Provider value={{ userId, setUserId }}>
      {!isLoading && children}
    </UserContext.Provider>
  );
};

// Create a custom hook to access the context values
export const useUser = () => useContext(UserContext);

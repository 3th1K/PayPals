import { takeEvery } from 'redux-saga/effects';
import { SET_USER } from './Constants';
import { GetUser } from '../services/UserService';
import { GetToken } from '../TokenHandler';
import { useState } from 'react';
function* SetUser(){
    const [userId, setUserId] = useState(null);
    const token = GetToken;
    if (token) {
        const decodedToken = jwtDecode(token);
        if (decodedToken && decodedToken.userId) {
            setUserId(decodedToken.userId);
        }
    }
    const data = GetUser(userId);
    console.log("data in saga "+data);
}
function* UserSagaData(){
    yield takeEvery(SET_USER, SetUser)
}
export default UserSagaData;
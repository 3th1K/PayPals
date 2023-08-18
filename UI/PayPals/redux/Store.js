import { configureStore } from "@reduxjs/toolkit";
import RootReducer from "./RootReducer";
import createSagaMiddleware from "redux-saga"; 
import UserSagaData from "./UserSaga";
const sagaMiddleware = createSagaMiddleware();
export const Store = configureStore({
    reducer:RootReducer,
    middleware:()=>sagaMiddleware
})

sagaMiddleware.run(UserSagaData);
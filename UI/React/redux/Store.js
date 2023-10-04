import { configureStore } from "@reduxjs/toolkit";
import RootReducer from './RootReducer'
import { CreateLogger } from '../Logger';
import createSagaMiddleware from 'redux-saga'
import SagaData from "./UserSaga";
const sagaMiddleware = createSagaMiddleware();
const middleware = [sagaMiddleware];
const log = CreateLogger("Store");
log.warn("Triggered Store");
export const Store = configureStore({
    reducer: RootReducer,
    middleware: middleware
})

sagaMiddleware.run(SagaData);


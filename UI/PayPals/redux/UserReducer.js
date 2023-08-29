import {
    SET_USER_SUCCESS, 
    SET_USER_FAILURE, 
} from "./Constants";
import { CreateLogger } from '../Logger';
const log = CreateLogger("UserReducer");

const initialState = {
    user: null,
    error: null
};

export const UserReducer = (state = initialState, action) =>{
    log.warn("Triggered Reducer");
    switch(action.type){
        case SET_USER_SUCCESS:
            return { 
                ...state, 
                user: action.payload 
            };
        case SET_USER_FAILURE:
            return {
                ...state,
                user: null,
                userGroups: null,
                error: action.error
            };
        default:
            return state;
    }
}
import { SET_USER } from "./Constants";

const initialState = null;

export const UserReducer = (state = initialState, action) =>{
    switch(action.type){
        case SET_USER:
            return action.data
    }
}
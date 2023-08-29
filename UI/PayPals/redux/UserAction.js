import { SET_USER, SET_USER_GROUPS } from "./Constants";
import { CreateLogger } from '../Logger';
const log = CreateLogger("UserAction");

export const SetUser = (userId)=>{
    return {
        type: SET_USER,
        payload: userId
    }
}

// export const SetUserGroups  = (userId)=>{
//     return {
//         type: SET_USER_GROUPS,
//         payload: userId
//     }
// }
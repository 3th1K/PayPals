import { SET_USER } from "./Constants";
import { CreateLogger } from '../Logger';
const log = CreateLogger("UserAction");

export const SetUser = (userId)=>{
    log.warn("Triggered action");
    return {
        type: SET_USER,
        payload: userId
    }
}
import { 
    SET_USER_SUCCESS, 
    SET_USER, 
    SET_USER_FAILURE, 
    SET_USER_GROUPS,
    SET_USER_GROUPS_SUCCESS,
    SET_USER_GROUPS_FAILURE
} from "./Constants";
import { takeEvery, call, put } from 'redux-saga/effects'
import { CreateLogger } from "../Logger";
import { GetUser, GetUserGroups } from "../services/UserService";
import { GetError } from "../helpers/ErrorHandler";
const log = CreateLogger("UserSaga");

function* SetUser(action){
    const userId = action.payload;
    try{
        const userData = yield call(GetUser, userId);
        const userGroups = yield call(GetUserGroups, userId);
        userData.groups = userGroups;
        yield put({type:SET_USER_SUCCESS, payload: userData});
    }
    catch(error){
        var errorData = GetError(error);
        yield put({ type: SET_USER_FAILURE, error: errorData.ErrorMessage });
    }
}

// function* SetUserGroups(action){
//     const userId = action.payload;
//     try{
//         const userGroups = yield call(GetUserGroups, userId);
//         yield put({type:SET_USER_GROUPS_SUCCESS, payload:userGroups});
//     }
//     catch(error){
//         yield put({type:SET_USER_GROUPS_FAILURE, error: error.message})
//     }
// }

function* SagaData(){
    yield takeEvery(SET_USER, SetUser);
}
export default SagaData;
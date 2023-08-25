import { SET_USER_SUCCESS, SET_USER, SET_USER_FAILURE } from "./Constants";
import { takeEvery, call, put } from 'redux-saga/effects'
import { CreateLogger } from "../Logger";
import { GetUser } from "../services/UserService";
const log = CreateLogger("UserSaga");

function* SetUser(action){
    const userId = action.payload;
    log.warn(`Set User is Called with userId: ${userId}`);
    log.info("Setting User");
    try{
        const userData = yield call(GetUser, userId);
        yield put({type:SET_USER_SUCCESS, payload: userData});
        log.success("User has been set");
    }
    catch(error){
        log.error("User was not set");
        yield put({ type: SET_USER_FAILURE, error: error.message });
    }
}

function* SagaData(){
    yield takeEvery(SET_USER, SetUser);
}
export default SagaData;
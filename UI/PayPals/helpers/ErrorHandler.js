import { CreateLogger } from "../Logger";
var log = CreateLogger("ErrorHandler");
export const GetError = (error) =>{
    log.warn(error.code)
    if(error.code == "ECONNABORTED"){
        return {
            ErrorCode : "ERR_TIMEOUT",
            ErrorMessage : "Unable to reach server, try again later"
        }
    }
    else{
        log.success("came here random");
        return {
            ErrorCode : error.response.data.errorCode,
            ErrorMessage : error.response.data.errorMessage
        } 
    }
}
import axiosInstance from "../helpers/Interceptor";

export const GetGroup = async (id) => {
    const response = await axiosInstance.get(`/groups/${id}`);
    return response.data;
}
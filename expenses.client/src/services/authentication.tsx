import { login } from '../app/authenticationSlice';
import axios, { AxiosInstance } from 'axios';

const axiosInstance: AxiosInstance = axios.create({
    baseURL: `https://localhost:5001/Authentication`,
});

export const SignUp = async (dispatch: any, credentials: any): Promise<void> => {
    try {
        // api call
        const { data } = await axiosInstance.post('/signup', credentials);
        dispatch(login(data));
    } catch (error) {
        console.log('Error:', error);
    }
};

export const SignIn = async (dispatch: any, credentials: any): Promise<void> => {
    try {
        // api call
        const { data } = await axiosInstance.post('/signin', credentials);
        dispatch(login(data));
    } catch (error) {
        console.log('Error:', error);
    }
};

import { createSlice, PayloadAction } from '@reduxjs/toolkit';

interface AuthenticationState {
    token: string | null;
    email: string | null;
    isLoggedIn: boolean;
}

const initialState: AuthenticationState = {
    token: sessionStorage.getItem('token') || null,
    email: sessionStorage.getItem('email') || null,
    isLoggedIn: sessionStorage.getItem('isLoggedIn') === 'true',
};

export const authenticationSlice = createSlice({
    name: 'authentication',
    initialState,
    reducers: {
        login: (state, action: PayloadAction<{ email: string, password: string }>) => {
            state.token = action.payload.password;
            state.isLoggedIn = true;
            state.email = action.payload.email;

            console.log(action.payload);
            console.log('password:' + action.payload.password);
            console.log('email:' + action.payload.email);

            sessionStorage.setItem('token', action.payload.password);
            sessionStorage.setItem('isLoggedIn', 'true');
            sessionStorage.setItem('email', action.payload.email);

            console.log('isLoggedIn:' + sessionStorage.getItem('isLoggedIn'));
        },
        logout: (state) => {
            state.token = null;
            state.isLoggedIn = false;

            sessionStorage.removeItem('token');
            sessionStorage.removeItem('isLoggedIn');
            sessionStorage.removeItem('email');
        },
    },
});

export const { login, logout } = authenticationSlice.actions;

export default authenticationSlice.reducer;
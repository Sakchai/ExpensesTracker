import { configureStore, ThunkAction, Action } from '@reduxjs/toolkit';
import authenticationReducer from './authenticationSlice';
import expensesSlice from './expensesSlice';
import ToastMiddleware from '../middlewares/toastMiddleware';

export const store = configureStore({
    reducer: {
        authentication: authenticationReducer,
        expenses: expensesSlice,
    },
    middleware: (getDefaultMiddleware) => getDefaultMiddleware().concat(ToastMiddleware),
});

export type AppDispatch = typeof store.dispatch;
export type RootState = ReturnType<typeof store.getState>;
export type AppThunk<ReturnType = void> = ThunkAction<
    ReturnType,
    RootState,
    unknown,
    Action<string>
    >;

export default store; 
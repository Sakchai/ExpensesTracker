import { createSlice, PayloadAction } from '@reduxjs/toolkit';

interface ErrorState {
    expensesError: string | null;
    newExpenseError: string | null;
    editExpenseError: string | null;
    deleteExpenseError: string | null;
}

const initialState: ErrorState = {
    expensesError: null,
    newExpenseError: null,
    editExpenseError: null,
    deleteExpenseError: null,
};

export const errorExpensesSlice = createSlice({
    name: 'error',
    initialState,
    reducers: {
        setExpensesError: (state, action: PayloadAction<string | null>) => {
            state.expensesError = action.payload;
        },
        setNewExpenseError: (state, action: PayloadAction<string | null>) => {
            state.newExpenseError = action.payload;
        },
        setEditExpenseError: (state, action: PayloadAction<string | null>) => {
            state.editExpenseError = action.payload;
        },
        setDeleteExpenseError: (state, action: PayloadAction<string | null>) => {
            state.deleteExpenseError = action.payload;
        },
    },
});

export const { setExpensesError, setNewExpenseError, setEditExpenseError, setDeleteExpenseError } = errorExpensesSlice.actions;
export default errorExpensesSlice.reducer;

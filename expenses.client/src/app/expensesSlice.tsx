import { createSlice, PayloadAction } from '@reduxjs/toolkit';

interface Expense {
    id: number;
    description: string;
    amount: number;
}

interface ExpensesState {
    expenses: Expense[];
}

const initialState: ExpensesState = {
    expenses: [],
};

export const expensesSlice = createSlice({
    name: 'expenses',
    initialState,
    reducers: {
        setExpenses: (state, action: PayloadAction<Expense[]>) => {
            state.expenses = [...action.payload];
        },
        newExpense: (state, action: PayloadAction<Expense>) => {
            state.expenses.unshift(action.payload);
        },
        editExpense: (state, action: PayloadAction<Expense>) => {
            const index = state.expenses.findIndex((expense) => expense.id === action.payload.id);
            if (index !== -1) {
                state.expenses[index] = action.payload;
            }
        },
        deleteExpense: (state, action: PayloadAction<{ id: number }>) => {
            state.expenses = state.expenses.filter((expense) => expense.id !== action.payload.id);
        },
    },
});

export const { setExpenses, newExpense, editExpense, deleteExpense } = expensesSlice.actions;

export default expensesSlice.reducer;
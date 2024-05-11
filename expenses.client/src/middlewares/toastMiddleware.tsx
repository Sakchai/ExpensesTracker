import { Middleware } from 'redux';
import { newExpense, editExpense, deleteExpense } from '../app/expensesSlice';
import { setDeleteExpenseError, setExpensesError, setNewExpenseError, setEditExpenseError } from '../app/errorExpensesSlice';
import { toast } from 'react-toastify';

const ToastMiddleware: Middleware = ({ dispatch }) => next => action => {
    switch (action.type) {
        case newExpense.type:
            toast.success('New expense added successfully');
            break;
        case editExpense.type:
            toast.success('Expense edited successfully');
            break;
        case deleteExpense.type:
            toast.success('Expense deleted successfully');
            break;
        case setExpensesError.type:
            toast.error('Error loading expenses');
            break;
        case setNewExpenseError.type:
            toast.error('Error adding new expense');
            break;
        case setEditExpenseError.type:
            toast.error('Error editing expense');
            break;
        case setDeleteExpenseError.type:
            toast.error('Error deleting expense');
            break;
        default:
            break;
    }
    return next(action);
};

export default ToastMiddleware;

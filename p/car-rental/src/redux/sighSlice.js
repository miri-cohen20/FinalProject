import { createSlice } from "@reduxjs/toolkit";
import { fetchSighUpAsyncAction } from "./Thunk";

const sighSlice = createSlice({
    name: "sigh",
    initialState: {
        loading: false,
        error:null
    },
reducers:{
    
}, extraReducers: (builder) => {
    builder
        .addCase(fetchSighUpAsyncAction.pending, (state) => {
            state.loading = true;
            state.error = null; // לא לאתחל שגיאה ב-pending
        })
        .addCase(fetchSighUpAsyncAction.fulfilled, (state, action) => {
            state.loading = false;
            state.error = null; // לא לאתחל שגיאה על הצלחה
            // כאן תוכל להוסיף את הנתונים שהתקבלו מהצלחה, אם יש צורך
        })
        .addCase(fetchSighUpAsyncAction.rejected, (state, action) => {
            state.loading = false;
            state.error = action.error.message; // השתמש במשתנה action.error
        });
}
});
export const {} = sighSlice.actions;
export default sighSlice.reducer;
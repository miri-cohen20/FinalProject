
import { createSlice } from "@reduxjs/toolkit";
import { fetchActivityRentals, fetchActivityAndFutureRentals } from "./Thunk";

const initialState = {
    activityRentals: [],
    activityAndFutueRentals: [],
    loading: false,
    error: null,   
    
};

const activitySlice = createSlice({
    name: "activity",
    initialState,
    reducers: {

    },
    extraReducers: (builder) => {
        builder
            .addCase(fetchActivityRentals.pending, (state) => {
                state.loading = true;
                state.error = null;
            })
            .addCase(fetchActivityRentals.fulfilled, (state, action) => {
                state.loading = false;
                state.activityRentals = action.payload;
            })
            .addCase(fetchActivityRentals.rejected, (state, action) => {
                state.loading = false;
                state.error = action.error.message;
            })
            .addCase(fetchActivityAndFutureRentals.pending, (state) => {
                state.loading = true;
                state.error = null;
                
            })
            .addCase(fetchActivityAndFutureRentals.fulfilled, (state, action) => {
                state.loading = false;
                state.activityAndFutueRentals = action.payload;
                
            })
            .addCase(fetchActivityAndFutureRentals.rejected, (state, action) => {
                state.loading = false;
                state.error = action.error.message;
                
            })

    }
});

export const {  } = activitySlice.actions;
export default activitySlice.reducer;
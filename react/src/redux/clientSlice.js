
import { createSlice } from "@reduxjs/toolkit";
import { updateCustomerDetails, fetchRentalHistory } from "./Thunk";

const initialState = {
    customer: null,
    rentalHistory: [],
    loading: false,
    error: null,
    updateSuccess: false,
};

const clientSlice = createSlice({
    name: "client",
    initialState,
    reducers: {
        setCustomer: (state, action) => {
            state.customer = action.payload;
        },
        clearClientError: (state) => {
            state.error = null;
            state.updateSuccess = false;
        }
    },
    extraReducers: (builder) => {
        builder
            .addCase(fetchRentalHistory.pending, (state) => {
                state.loading = true;
                state.error = null;
            })
            .addCase(fetchRentalHistory.fulfilled, (state, action) => {
                state.loading = false;
                state.rentalHistory = action.payload;
            })
            .addCase(fetchRentalHistory.rejected, (state, action) => {
                state.loading = false;
                state.error = action.error.message;
            })
            .addCase(updateCustomerDetails.pending, (state) => {
                state.loading = true;
                state.error = null;
                state.updateSuccess = false;
            })
            .addCase(updateCustomerDetails.fulfilled, (state, action) => {
                state.loading = false;
                state.customer = { ...state.customer, ...action.payload };
                state.updateSuccess = true;
            })
            .addCase(updateCustomerDetails.rejected, (state, action) => {
                state.loading = false;
                state.error = action.error.message;
                state.updateSuccess = false;
            });
    }
});

export const { setCustomer, clearClientError } = clientSlice.actions;
export default clientSlice.reducer;
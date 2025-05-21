import { createSlice } from "@reduxjs/toolkit";
import { fetchSighUpAsyncAction, fetchLoginAsyncAction } from "./Thunk";

const localUser = JSON.parse(localStorage.getItem("user") || "null");
const initialState = {
    loading: false,
    error: null,
    user: localUser,
    message: ""
};

const sighSlice = createSlice({
    name: "sigh",
    initialState,
    reducers: {
        clearError: (state) => { state.error = null; },
        clearMessage: (state) => { state.message = ""; },
        logout: (state) => {
            state.user = null;
            localStorage.removeItem("user");
            state.message = "";
        }
    },
    extraReducers: (builder) => {
        builder
            .addCase(fetchSighUpAsyncAction.pending, (state) => {
                state.loading = true;
                state.error = null;
                state.message = "";
            })
            .addCase(fetchSighUpAsyncAction.fulfilled, (state, action) => {
                state.loading = false;
                state.error = null;
                state.user = {
                    id: action.payload.id,
                    firstName: action.payload.firstName
                };
                localStorage.setItem("user", JSON.stringify(state.user));
                state.message = "Registration successful!";
            })
            .addCase(fetchSighUpAsyncAction.rejected, (state, action) => {
                state.loading = false;
                state.error = action.error.message || "Registration failed";
                state.message = "";
            })
            .addCase(fetchLoginAsyncAction.pending, (state) => {
                state.loading = true;
                state.error = null;
                state.message = "";
            })
            .addCase(fetchLoginAsyncAction.fulfilled, (state, action) => {
                state.loading = false;
                state.error = null;
                state.user = {
                    id: action.payload.id,
                    firstName: action.payload.firstName
                };
                localStorage.setItem("user", JSON.stringify(state.user));
                state.message = "Login successful!";
            })
            .addCase(fetchLoginAsyncAction.rejected, (state, action) => {
                state.loading = false;
                state.error = action.error.message || "Login failed";
                state.message = "";
            });
    }
});

export const { clearError, clearMessage, logout } = sighSlice.actions;
export default sighSlice.reducer;
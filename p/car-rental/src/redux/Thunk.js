

import { createAsyncThunk } from "@reduxjs/toolkit";
import { fetchSighUp } from "../api";

export const fetchSighUpAsyncAction = createAsyncThunk("sigh/sighUp", async (customerData) => {
    const response = await fetchSighUp(customerData);
    
    if (!response.ok) {
        const errorData = await response.json();
        throw new Error(errorData.message);
    }
    
    return await response.json(); 
});

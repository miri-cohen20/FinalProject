
import { createAsyncThunk } from "@reduxjs/toolkit";
import { fetchSighUp, fetchLogin,updateCustomerDetailsApi, fetchRentalHistoryApi ,fetchAllRenting,fetchAllCar } from "../api";

export const fetchSighUpAsyncAction = createAsyncThunk(
    "sigh/sighUp",
    async (customerData) => {
      const response = await fetchSighUp(customerData);
      // מיפוי לתוצאה שהסלייס שלך מצפה לה
      return {
        id: response.customerId,
        firstName: customerData.firstName // או מה ששלחת מהטופס
      };
    }
  );

export const fetchLoginAsyncAction = createAsyncThunk(
    "sigh/login",
    async (loginData) => {
        return await fetchLogin(loginData);
    }
);

// עדכון פרטי לקוח
export const updateCustomerDetails = createAsyncThunk(
    "client/updateCustomerDetails",
    async ({ id, data }) => {
        return await updateCustomerDetailsApi(id, data);
    }
);

// שליפת היסטוריית השכרות
export const fetchRentalHistory = createAsyncThunk(
    "client/fetchRentalHistory",
    async (customerId) => {
        return await fetchRentalHistoryApi(customerId);
    }
);


export const fetchGetAllCarAsyncAction = createAsyncThunk("getAllCar", async () => {
    const response = await fetchAllCar();

    if (!response.ok) {
        const errorData = await response.json();
        throw new Error(errorData.message);
    }

    return await response.json(); 
});

export const fetchGetAllRentingAsyncAction = createAsyncThunk("getAllRenting", async () => {
    const response = await fetchAllRenting();

    if (!response.ok) {
        const errorData = await response.json();
        throw new Error(errorData.message);
    }

    return await response.json(); 
});
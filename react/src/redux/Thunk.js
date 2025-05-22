
import { createAsyncThunk } from "@reduxjs/toolkit";
import { fetchSighUp, fetchLogin,updateCustomerDetailsApi, fetchRentalHistoryApi  } from "../api";

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



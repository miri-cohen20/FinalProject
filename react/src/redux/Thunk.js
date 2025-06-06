
import { createAsyncThunk } from "@reduxjs/toolkit";
import { fetchSighUp, fetchLogin,updateCustomerDetailsApi, fetchRentalHistoryApi ,fetchAllRenting,fetchAllCar, fetchAllActiveAndFutureRentals,fetchAllActivityRentals,
     fetchEndRental, fetchComlaintMal, fetchComlaintCleaning, fetchGetUntilExtendRental, fetchGetPriceForExtendRental, fetchExtendRental} from "../api";

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




export const fetchGetAllRentingAsyncAction = createAsyncThunk("getAllRenting", async () => {
    const response = await fetchAllRenting();

    if (!response.ok) {
        const errorData = await response.json();
        throw new Error(errorData.message);
    }

    return await response.json(); 
});

export const fetchActivityRentals = createAsyncThunk(
    "activity/fetchActivityRentals",
    async (customerId) => {
        return await fetchAllActivityRentals(customerId);
    }
);
export const fetchActivityAndFutureRentals = createAsyncThunk(
    "activity/fetchActivityAndFutureRentals",
    async (customerId) => {
        return await fetchAllActiveAndFutureRentals(customerId);
    }
);
export const fetchGetAllCarAsyncAction = createAsyncThunk(
    "getAllCar",
    async () => {
        return await fetchAllCar();
    }
);
export const fetcheEndReantlasyncAction = createAsyncThunk(
    "activity/endRental", 
    async ({customerId, rentalId})=>{
        return await fetchEndRental({customerId, rentalId});
         
    }
);
export const fetchComlaintMalasyncAction = createAsyncThunk(
    "activity/complaintMal", 
    async ({rentalId, descraption})=>{
        return await fetchComlaintMal({rentalId, descraption});
         
    }
);

export const fetchComlaintCleaningasyncAction = createAsyncThunk(
    "activity/complaintMal", 
    async ({rentalId, descraption})=>{
        return await fetchComlaintCleaning({rentalId, descraption});
         
    }
);
export const fetchUntilExtendRental = createAsyncThunk(
    "activity/extendRental", 
    async ({idCustomer, idRenting})=>{
        return await fetchGetUntilExtendRental({idCustomer, idRenting});
         
    }
);

export const fetchGetPriceExtendRental = createAsyncThunk(
    "activity/priceForExtendRental", 
    async ({rentalId,customerId, untilTime })=>{
        return await fetchGetPriceForExtendRental({rentalId,customerId, untilTime });
         
    }
);
export const fetchExtendRentalAsinc = createAsyncThunk(
    "activity/ExtendRental", 
    async ({rentalId,customerId, untilTime })=>{
        return await fetchExtendRental({rentalId,customerId, untilTime });
         
    }
);










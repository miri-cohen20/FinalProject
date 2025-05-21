import { createSlice } from "@reduxjs/toolkit";
import { fetchGetAllCarAsyncAction, fetchGetAllRentingAsyncAction } from "./Thunk";

const getAllCarSlice = createSlice({
    name: "car",
    initialState: {
        cars: [],
        rentals: [],
        loading: false,
        error: null
    },
    reducers: {
        selectAvailableCars: (state, action) => {
            const { startTime, endTime } = action.payload;
            return state.cars.filter(car => {
                return !state.rentals.some(rental => {
                    const rentalStart = new Date(rental.startTime);
                    const rentalEnd = new Date(rental.endTime);

                    // אם יש רק startTime, נבדוק אם הרכב פנוי באותה השעה
                    if (startTime && !endTime) {
                        return rentalStart <= startTime && rentalEnd >= startTime;
                    }

                    // אם יש גם startTime וגם endTime, נבדוק את טווח הזמן
                    if (startTime && endTime) {
                        return (rentalStart < endTime && rentalEnd > startTime);
                    }

                    // אם אין startTime, נחשב את כל הרכבים כזמינים
                    return false;
                });
            });
        },
        selectCarsBySeats: (state, action) => {
            const numberOfSeats = action.payload;
            return state.cars.filter(car => car.seats === numberOfSeats);
        },
        selectCarsByCity: (state, action) => {
            const city = action.payload;
            return state.cars.filter(car => car.city === city);
        },
        // Function to filter cars by city and neighborhood
        selectCarsByCityAndStreet: (state, action) => {
            const { city, street } = action.payload;
            return state.cars.filter(car => car.city === city && car.street === street);
        },
        filterTowList: (state, action) => {
            const { list1, list2 } = action.payload;
            return list1.filter(item => list2.includes(item));
        },
        filterThreeList: (state, action) => {
            const { list1, list2, list3 } = action.payload;
            return list1.filter(item => list2.includes(item) && list3.includes(item));
        }
    },
    extraReducers: (builder) => {
        builder
            // Fetch Cars
            .addCase(fetchGetAllCarAsyncAction.pending, (state) => {
                state.loading = true;
            })
            .addCase(fetchGetAllCarAsyncAction.fulfilled, (state, action) => {
                state.cars = action.payload;
                state.loading = false;
            })
            .addCase(fetchGetAllCarAsyncAction.rejected, (state, action) => {
                state.loading = false;
                state.error = action.error.message;
            })
            // Fetch Rentals
            .addCase(fetchGetAllRentingAsyncAction.pending, (state) => {
                state.loading = true;
            })
            .addCase(fetchGetAllRentingAsyncAction.fulfilled, (state, action) => {
                state.rentals = action.payload;
                state.loading = false;
            })
            .addCase(fetchGetAllRentingAsyncAction.rejected, (state, action) => {
                state.loading = false;
                state.error = action.error.message;
            });
    },
});

export const initializeData = () => async (dispatch) => {
  await dispatch(fetchGetAllCarAsyncAction());
  await dispatch(fetchGetAllRentingAsyncAction());
};

export const { 
    selectAvailableCars, 
    selectCarsBySeats, 
    selectCarsByCityAndStreet, 
    selectCarsByCity, 
    filterTowList, 
    filterThreeList 
} = getAllCarSlice.actions;

export default getAllCarSlice.reducer;

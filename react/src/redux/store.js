import { configureStore } from "@reduxjs/toolkit";
import sighReducer from "./sighSlice";
import clientReducer from "./clientSlice";
import carReducer from "./getAllCarSlice"

const store = configureStore({
  reducer: {
    sigh: sighReducer,
    client: clientReducer,
    car: carReducer
  }
});

export default store;
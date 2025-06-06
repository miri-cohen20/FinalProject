import { configureStore } from "@reduxjs/toolkit";
import sighReducer from "./sighSlice";
import clientReducer from "./clientSlice";
import carReducer from "./getAllCarSlice"
import activityReducer from "./activitySlice"

const store = configureStore({
  reducer: {
    sigh: sighReducer,
    client: clientReducer,
    car: carReducer,
    activity: activityReducer
  }
});

export default store;
import { configureStore } from "@reduxjs/toolkit";
import sighReducer from "./sighSlice";
import clientReducer from "./clientSlice";

const store = configureStore({
  reducer: {
    sigh: sighReducer,
    client: clientReducer
  }
});

export default store;
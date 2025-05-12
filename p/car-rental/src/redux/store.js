import { configureStore } from "@reduxjs/toolkit";
import sighReducer from "./SighSlice";

const store = configureStore({
    reducer: {

        sigh:sighReducer
    }
});
export default store;

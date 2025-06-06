import { StrictMode } from 'react';
import { createRoot } from 'react-dom/client';
import './index.css';
import App from './App.jsx';
import { Provider } from 'react-redux'; // אל תשכח לייבא את זה
import store from './redux/store'; // ייבוא ה-store לפי הנתיב הנכון
import "./components/AppTheme.css";

createRoot(document.getElementById('root')).render(
  <StrictMode>
    <Provider store={store}>
      <App />
    </Provider>
  </StrictMode>
);

// import React from "react";
// import ReactDOM from "react-dom";
// import { Provider } from "react-redux";
// import store from "./redux/store";
// import SignUpLoginComponent from "./components/SignUpLoginComponent";

// ReactDOM.render(
//     <Provider store={store}>
//         <SignUpLoginComponent />
//     </Provider>,
//     document.getElementById("root")
// );

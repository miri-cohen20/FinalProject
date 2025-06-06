import React from "react";
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import SignUpLoginComponent from "./components/SignUpComponent";
import Client from "./components/Client";
import RentCar from "./components/RentCar";
import ActivityRental from "./components/ActivityRental";
import ActivityNow from "./components/ActivityNow";
import ActivityFuture from "./components/ActivityFuture";
import EndRental from "./components/EndRental";
import SuccessedEnd from "./components/SuccessedEnd";
import ComlaintMal from "./components/ComlaintMal";
import SuccessedComlaint from "./components/SuccessedComlaint"
import ComlaintCleaning from "./components/ComlaintCleaning"

import Payment from "./components/Payment";
import ExtendRental from "./components/ExtendRental";
import ExtendSuccess from "./components/ExtendSuccess";
function App() {
  return (
    <Router>
      <Routes>
        <Route path="/" element={<SignUpLoginComponent />} />
        <Route path="/client" element={<Client />} />
        <Route path="/RentCar" element={<RentCar />} />
        <Route path="/ActivityRentals" element={<ActivityRental />} />
        <Route path="/ActivityNow" element={<ActivityNow />} />
        <Route path="/ActivityFuture" element={<ActivityFuture />} />
        <Route path="/EndRental" element={<EndRental />} />
        <Route path="/SuccessedEnd" element={<SuccessedEnd />} />
        <Route path="/ComlaintMal" element={<ComlaintMal />} />
        <Route path="/ComlaintCleaning" element={<ComlaintCleaning />} />
        <Route path="/ExtendRental" element={<ExtendRental />} />
        <Route path="/SuccessedComlaint" element={<SuccessedComlaint />} />

        <Route path="/extend-rental" element={<ExtendRental />} />
        <Route path="/payment" element={<Payment />} />
       <Route path="/extend-success" element={<ExtendSuccess />} />
        
      </Routes>
    </Router>
  );
}

export default App;


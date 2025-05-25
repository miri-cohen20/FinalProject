import React from "react";
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import SignUpLoginComponent from "./components/SignUpComponent";
import Client from "./components/Client";
import RentCar from "./components/RentCar";
import ActivityRental from "./components/ActivityRental";
import ActivityNow from "./components/ActivityNow";
import ActivityFuture from "./components/ActivityFuture";

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
      </Routes>
    </Router>
  );
}

export default App;


import React from "react";
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import SignUpLoginComponent from "./components/SignUpComponent";
import Client from "./components/Client";
import RentCar from "./components/RentCar";

function App() {
  return (
    <Router>
      <Routes>
        <Route path="/" element={<SignUpLoginComponent />} />
        <Route path="/client" element={<Client />} />
        <Route path="/RentCar" element={<RentCar />} />
      </Routes>
    </Router>
  );
}

export default App;


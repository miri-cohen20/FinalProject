import React from "react";
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import SignUpLoginComponent from "./components/SignUpComponent";
import Client from "./components/Client";

function App() {
  return (
    <Router>
      <Routes>
        <Route path="/" element={<SignUpLoginComponent />} />
        <Route path="/client" element={<Client />} />
      </Routes>
    </Router>
  );
}

export default App;


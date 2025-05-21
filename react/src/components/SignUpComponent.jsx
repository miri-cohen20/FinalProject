import React, { useState, useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import { useNavigate } from "react-router-dom";
import { fetchSighUpAsyncAction, fetchLoginAsyncAction } from "../redux/Thunk";
import { clearError, clearMessage } from "../redux/sighSlice";
import "./SignUpComponent.css";

const carImgUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/3/3e/Toyota_Yaris_II_1.3_VVT-i_Facelift_front_20100801.jpg/800px-Toyota_Yaris_II_1.3_VVT-i_Facelift_front_20100801.jpg";

const SignUpLoginComponent = () => {
  const dispatch = useDispatch();
  const navigate = useNavigate();

  const loading = useSelector((state) => state.sigh.loading);
  const error = useSelector((state) => state.sigh.error);
  const message = useSelector((state) => state.sigh.message);
  const user = useSelector((state) => state.sigh.user);

  const [mode, setMode] = useState(""); // "", "signup", "login"
  const [successMsg, setSuccessMsg] = useState("");

  const [customerData, setCustomerData] = useState({
    id: "",
    firstName: "",
    lastName: "",
    phoneNumber: "",
    email: "",
    city: "",
    address: "",
    buildingNumber: "",
    password: ""
  });

  const [loginData, setLoginData] = useState({
    id: "",
    password: ""
  });

  const handleChange = (e) => {
    const { name, value } = e.target;
    if (mode === "signup") {
      setCustomerData({ ...customerData, [name]: value });
    } else if (mode === "login") {
      setLoginData({ ...loginData, [name]: value });
    }
  };

  const handleSignUp = async (e) => {
    e.preventDefault();
    dispatch(clearError());
    dispatch(clearMessage());
    await dispatch(fetchSighUpAsyncAction(customerData));
  };

  const handleLogin = async (e) => {
    e.preventDefault();
    dispatch(clearError());
    dispatch(clearMessage());
    await dispatch(fetchLoginAsyncAction(loginData));
  };

  const handleBack = () => {
    setMode("");
    setCustomerData({
      id: "",
      firstName: "",
      lastName: "",
      phoneNumber: "",
      email: "",
      city: "",
      address: "",
      buildingNumber: "",
      password: ""
    });
    setLoginData({
      id: "",
      password: ""
    });
    dispatch(clearError());
    dispatch(clearMessage());
    setSuccessMsg("");
  };

  // ניווט אוטומטי ל-Client אחרי הצלחה, רק אם יש user!
  useEffect(() => {
    if (
      (message === "Registration successful!" || message === "Login successful!") &&
      user && user.firstName
    ) {
      setSuccessMsg(message === "Registration successful!" ? "נרשמת בהצלחה" : "התחברת בהצלחה");
      setTimeout(() => {
        dispatch(clearMessage());
        navigate("/client");
      }, 1200);
    }
  }, [message, user, navigate, dispatch]);

  return (
    <div className="car-bg-container">
      <h1 style={{
        textAlign: "center",
        color: "#fff",
        marginBottom: "2rem",
        fontSize: "2rem",
        letterSpacing: "1px",
        textShadow: "0 2px 12px #333"
      }}>
        ברוכה הבאה להשכרת רכבים הגדולה בעולם
      </h1>
      <img src={carImgUrl} alt="רכב יפה" className="car-image" />

      <div className="signup-login-box">
        {mode === "" && (
          <div style={{ textAlign: "center" }}>
            <button style={{ width: "80%", margin: "1rem 0", padding: 10 }} onClick={() => setMode("signup")}>
              הרשמה
            </button>
            <button style={{ width: "80%", padding: 10 }} onClick={() => setMode("login")}>
              התחברות
            </button>
          </div>
        )}

        {mode === "signup" && (
          <form onSubmit={handleSignUp} style={{ display: "flex", flexDirection: "column", gap: 10 }}>
            <h2 style={{ textAlign: "center" }}>הרשמה</h2>
            <input name="id" placeholder="תעודת זהות" onChange={handleChange} required />
            <input name="firstName" placeholder="שם פרטי" onChange={handleChange} required />
            <input name="lastName" placeholder="שם משפחה" onChange={handleChange} />
            <input name="phoneNumber" placeholder="מספר טלפון" onChange={handleChange} required />
            <input name="email" placeholder="אימייל" type="email" onChange={handleChange} required />
            <input name="city" placeholder="עיר" onChange={handleChange} required />
            <input name="address" placeholder="רחוב" onChange={handleChange} required />
            <input name="buildingNumber" placeholder="מספר בית" onChange={handleChange} />
            <input name="password" placeholder="סיסמה" type="password" onChange={handleChange} required />
            <button type="submit" disabled={loading}>
              {loading ? "נרשם..." : "הרשמה"}
            </button>
            <button type="button" onClick={handleBack} style={{ background: "#eee", color: "#222" }}>
              חזרה
            </button>
            {successMsg && <p style={{ color: "green", textAlign: "center" }}>{successMsg}</p>}
            {message && !successMsg && <p style={{ color: "green" }}>{message}</p>}
            {error && <p style={{ color: "red" }}>{error}</p>}
          </form>
        )}

        {mode === "login" && (
          <form onSubmit={handleLogin} style={{ display: "flex", flexDirection: "column", gap: 10 }}>
            <h2 style={{ textAlign: "center" }}>התחברות</h2>
            <input name="id" placeholder="תעודת זהות" onChange={handleChange} required />
            <input name="password" placeholder="סיסמה" type="password" onChange={handleChange} required />
            <button type="submit" disabled={loading}>
              {loading ? "מתחבר..." : "התחברות"}
            </button>
            <button type="button" onClick={handleBack} style={{ background: "#eee", color: "#222" }}>
              חזרה
            </button>
            {successMsg && <p style={{ color: "green", textAlign: "center" }}>{successMsg}</p>}
            {message && !successMsg && <p style={{ color: "green" }}>{message}</p>}
            {error && <p style={{ color: "red" }}>{error}</p>}
          </form>
        )}
      </div>
    </div>
  );
};

export default SignUpLoginComponent;
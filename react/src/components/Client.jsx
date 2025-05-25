// src/componenes/Client.jsx
import React, { useEffect, useState } from "react";
import { useSelector, useDispatch } from "react-redux";
import { useNavigate } from "react-router-dom";
import { clearClientError, setCustomer } from "../redux/clientSlice";
import { fetchRentalHistory, updateCustomerDetails } from "../redux/Thunk";
import "./Client.css";

const Popup = ({ onClose, children }) => (
  <div className="popup-overlay">
    <div className="popup-content">
      <button className="popup-close" onClick={onClose}>×</button>
      {children}
    </div>
  </div>
);

const Client = () => {
  const sighUser = useSelector((state) => state.sigh.user);
  const client = useSelector((state) => state.client.customer);
  const rentalHistory = useSelector((state) => state.client.rentalHistory);
  const loading = useSelector((state) => state.client.loading);
  const error = useSelector((state) => state.client.error);
  const updateSuccess = useSelector((state) => state.client.updateSuccess);

  const dispatch = useDispatch();
  const navigate = useNavigate();

  const [showHistory, setShowHistory] = useState(false);
  const [showEdit, setShowEdit] = useState(false);
  const [editData, setEditData] = useState({
    firstName: "",
    lastName: "",
    phoneNumber: "",
    email: "",
    city: "",
    street: "",
    buildingNumber: "",
    password: ""
  });

  useEffect(() => {
    if (sighUser && !client) {
      dispatch(setCustomer(sighUser));
    }
    if (client) {
      setEditData({
        firstName: client.firstName || "",
        lastName: client.lastName || "",
        phoneNumber: client.phoneNumber || "",
        email: client.email || "",
        city: client.city || "",
        street: client.street || "",
        buildingNumber: client.buildingNumber || "",
        password: ""
      });
    }
  }, [sighUser, client, dispatch]);

  const handleLogout = () => {
    navigate("/");
  };
  const handleRental = () => navigate("/RentCar");
  const handleActivity = () => navigate("/ActivityRentals");

  const openHistory = () => {
    if (client?.id) dispatch(fetchRentalHistory(client.id));
    setShowHistory(true);
  };

  const openEdit = () => {
    setShowEdit(true);
    dispatch(clearClientError());
  };
  const handleEditChange = (e) => {
    const { name, value } = e.target;
    setEditData((prev) => ({ ...prev, [name]: value }));
  };
  const handleEditSubmit = (e) => {
    e.preventDefault();
    if (client?.id) {
      const dataToSend = {
        ...editData,
        id: client.id, // הוספת id!
        buildingNumber: editData.buildingNumber === "" ? null : Number(editData.buildingNumber)
      };
      dispatch(updateCustomerDetails({ id: client.id, data: dataToSend }));
    }
  };

  return (
    <div className="client-bg">
    <div className="client-title-box">
      <h1 className="client-title">
        {client && client.firstName ? `שלום ל: ${client.firstName}` : "שלום אורח"}
      </h1>
      <p className="client-subtitle">
        אנחנו כאן תמיד לשירותך
      </p>
    </div>

      <div className="client-btns">
        <button onClick={handleRental} className="client-btn">השכרת רכב</button>
        <button onClick={openHistory} className="client-btn">היסטורית השכרות</button>
        <button onClick={handleActivity} className="client-btn">השכרת פעילות</button>
        {/* הכפתור של הפופאפ הצמוד */}
        <div className="client-btn-popup-wrapper">
          <button onClick={() => setShowEdit(true)} className="client-btn">
            שינוי פרטים אישיים
          </button>
          {showEdit && (
            <>
              <div className="popup-overlay" onClick={() => setShowEdit(false)} />
              <div className="popup-content popup-below-btn">
                <button className="popup-close" onClick={() => setShowEdit(false)}>×</button>
                <h2 className="popup-title">שינוי פרטים אישיים</h2>
                <form onSubmit={handleEditSubmit}>
                  <input name="firstName" placeholder="שם פרטי" value={editData.firstName} onChange={handleEditChange} className="client-input" />
                  <input name="lastName" placeholder="שם משפחה" value={editData.lastName} onChange={handleEditChange} className="client-input" />
                  <input name="phoneNumber" placeholder="טלפון" value={editData.phoneNumber} onChange={handleEditChange} className="client-input" />
                  <input name="email" placeholder="אימייל" value={editData.email} onChange={handleEditChange} className="client-input" />
                  <input name="city" placeholder="עיר" value={editData.city} onChange={handleEditChange} className="client-input" />
                  <input name="street" placeholder="רחוב" value={editData.street} onChange={handleEditChange} className="client-input" />
                  <input name="buildingNumber" placeholder="מספר בית" type="number" value={editData.buildingNumber} onChange={handleEditChange} className="client-input" />
                  <input name="password" placeholder="סיסמה חדשה" type="password" value={editData.password} onChange={handleEditChange} className="client-input" />
                  <button type="submit" disabled={loading} className="client-btn btn-block">
                    {loading ? "שומר..." : "עדכן"}
                  </button>
                  {error && <div className="popup-error">{error}</div>}
                  {updateSuccess && <div className="popup-success">הפרטים עודכנו!</div>}
                </form>
              </div>
            </>
          )}
        </div>
      </div>

      {/* פופאפ היסטוריה (אם יש קומפוננטה Popup, תעדכני, אחרת תעשי בשיטה כמו כאן) */}
      {showHistory && (
        <div className="popup-overlay">
          <div className="popup-content popup-center">
            <button className="popup-close" onClick={() => setShowHistory(false)}>×</button>
            <h2 className="popup-title">היסטורית השכרות</h2>
            {loading && <div>טוען...</div>}
            {error && <div className="popup-error">{error}</div>}
            <ul>
              {rentalHistory && rentalHistory.length > 0 ? (
                rentalHistory.map((r, i) => (
                  <li key={i}>{JSON.stringify(r)}</li>
                ))
              ) : (
                <li>לא נמצאו השכרות</li>
              )}
            </ul>
          </div>
        </div>
      )}
    </div>
  );
};

export default Client;